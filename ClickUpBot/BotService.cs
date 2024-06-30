using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using ClickUpBot.Models;
using System.Threading;
using ClickUpBot.Helpers;
using ClickUpBot.States;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ClickUpBot
{
    internal class BotService
    {
        DbHelper db = DbHelper.Instance;
        Dictionary<long, State> fsm = [];
        CancellationTokenSource cts = new();
        ITelegramBotClient botClient;
        public BotService(ITelegramBotClient _botClient)
        {
            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
            };
            botClient = _botClient;
            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message)
                return;

            if (message.Text != null)
                Console.WriteLine(message.Text);

            long userId = message.From!.Id;


            if (fsm.ContainsKey(userId))
            {
                if (message.Text != null && message.Text!.StartsWith("/cancel"))
                {
                    fsm.Remove(userId);
                    return;
                }

                await fsm[userId].Next(update);

                if (fsm[userId].IsFinished())
                    fsm.Remove(userId);

                return;
            }

            if (message.Text == null)
                return;

            if (!db.CheckIfUserExists(userId))
            {
                if (message.Text!.StartsWith("/start"))
                {
                    db.CreateUser(userId);
                    fsm.Add(userId, new SetTokenState(botClient, userId));
                    await fsm[userId].Next(update);
                }
                else
                {
                    await botClient.SendTextMessageAsync(userId, "Начните с команды /start");
                }
                return;
            }

            fsm.Add(userId, ParseCommand());
            await fsm[userId].Next(update);
            if (fsm[userId].IsFinished())
                fsm.Remove(userId);

            State ParseCommand()
            {
                if (message.Text!.StartsWith("/token"))
                {
                    return new SetTokenState(botClient, userId);
                }
                if (message.Text!.StartsWith("/project"))
                {
                    return new SetProjectState(botClient, userId);
                }
                if (message.Text!.StartsWith("/time"))
                {
                    return new SetTimeState(botClient, userId);
                }
                if (message.Text!.StartsWith("/task"))
                {
                    return new CreateTaskState(botClient, userId);
                }
                if (message.Text!.StartsWith("/today"))
                {
                    return new GetForTodayState(botClient, userId);
                }

                return new EmptyState(botClient, userId);
            }

        }

        public Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var ErrorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(ErrorMessage);
            return Task.CompletedTask;
        }
    }
}
