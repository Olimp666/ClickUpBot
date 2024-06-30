using ClickUpBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chinchilla.ClickUp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ClickUpBot.Models;
using Chinchilla.ClickUp.Params;
using Chinchilla.ClickUp.Requests;
using Chinchilla.ClickUp.Responses.Model;
using Telegram.Bot.Types.Enums;

namespace ClickUpBot.Commands
{
    internal class CreateTaskCommand : Command
    {
        Dictionary<string, string> teams = [];
        Dictionary<string, string> spaces = [];
        Dictionary<string, string> lists = [];
        Dictionary<string, long> members = [];

        MemoryStream docStream = new();
        bool skipDocument = false;
        long assigneeId;
        string? taskName;
        string? taskDescription;
        string? docName;
        DateTime dueTime;

        string? listId;


        ResponseModelTask? createdTask;

        ClickUpApi clickUpApi;
        BotUser currentUser;
        public CreateTaskCommand(ITelegramBotClient _bot, long _userId) : base(_bot, _userId)
        {
            currentUser = db.GetUser(userId);
            clickUpApi = new ClickUpApi(db.GetUserToken(userId));
        }

        public async override Task Next(Telegram.Bot.Types.Update update)
        {
            long userId = update.Message!.From!.Id;
            if (step == 0)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                await bot.SendTextMessageAsync(userId, "Введите название таски");
            }
            if (step == 1)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                taskName = update.Message!.Text;
                await bot.SendTextMessageAsync(userId, "Введите описание таски");
            }
            if (step == 2)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                KeyboardButton[] buttons = ["Пропустить"];
                var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };
                taskDescription = update.Message!.Text;
                await bot.SendTextMessageAsync(userId, "Приложите документ (опционально)", replyMarkup: markup);
            }
            if (step == 3)
            {
                if (update.Message!.Text == "Пропустить")
                {
                    skipDocument = true;
                }
                if (!skipDocument)
                {
                    if(!await ParseAttachment(update))
                    {
                        return;
                    }
                }

                string responseMessage = "Выберите команду\n";
                var response = await clickUpApi.GetAuthorizedTeamsAsync();
                foreach (var team in response.ResponseSuccess.Teams)
                {
                    responseMessage += $"`{team.Name}`\n";
                    teams.Add(team.Name, team.Id);
                }
                string? curTeamId = currentUser.DefaultTeamId;
                bool isTeamIdNull = curTeamId == null;
                KeyboardButton[] buttons = [isTeamIdNull ? "" : teams.First(x => x.Value == curTeamId).Key];
                var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };

                await bot.SendTextMessageAsync(userId, responseMessage, parseMode: ParseMode.MarkdownV2, replyMarkup: isTeamIdNull ? new ReplyKeyboardRemove() : markup);

            }
            if (step == 4)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                if (!teams.TryGetValue(update.Message!.Text, out string? value))
                {
                    await SendMessage(userId, "Тима не найдена");
                    return;
                }

                string responseMessage = "Выберите спейс\n";
                var teamId = value;
                var response = await clickUpApi.GetTeamSpacesAsync(new ParamsGetTeamSpaces(teamId));
                foreach (var space in response.ResponseSuccess.Spaces)
                {
                    responseMessage += $"`{space.Name}`\n";
                    spaces.Add(space.Name, space.Id);
                }
                string? curSpaceId = currentUser.DefaultSpaceId;
                bool isSpaceIdNull = curSpaceId == null;
                KeyboardButton[] buttons = [isSpaceIdNull ? "" : spaces.First(x => x.Value == curSpaceId).Key];
                var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };

                await bot.SendTextMessageAsync(userId, responseMessage, parseMode: ParseMode.MarkdownV2, replyMarkup: isSpaceIdNull ? new ReplyKeyboardRemove() : markup);

            }
            if (step == 5)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                if (!spaces.TryGetValue(update.Message!.Text, out string? value))
                {
                    await SendMessage(userId, "Спейс не найден");
                    return;
                }

                string responseMessage = "Выберите лист\n";
                var spaceId = value;
                var response = await clickUpApi.GetFolderlessListsAsync(new ParamsGetFolderlessLists(spaceId));
                foreach (var list in response.ResponseSuccess.Lists)
                {
                    responseMessage += $"`{list.Name}`\n";
                    lists.Add(list.Name, list.Id);
                }
                listId = currentUser.DefaultListId;
                bool isListIdNull = listId == null;
                KeyboardButton[] buttons = [isListIdNull ? "" : lists.First(x => x.Value == listId).Key];
                var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };

                await bot.SendTextMessageAsync(userId, responseMessage, parseMode: ParseMode.MarkdownV2, replyMarkup: isListIdNull ? new ReplyKeyboardRemove() : markup);

            }
            if (step == 6)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                if (!lists.TryGetValue(update.Message!.Text, out string? _listId))
                {
                    await SendMessage(userId, "Лист не найден");
                    return;
                }

                listId = _listId;
                string responseMessage = "Кому назначить таску?\n";
                var response = await clickUpApi.GetListMembersAsync(new ParamsGetListMembers(listId));
                foreach (var member in response.ResponseSuccess.Members)
                {
                    responseMessage += $"`{member.Username}`\n";
                    members.Add(member.Username, member.Id);
                }

                await bot.SendTextMessageAsync(userId, responseMessage, parseMode: ParseMode.MarkdownV2, replyMarkup: new ReplyKeyboardRemove());
            }
            if (step == 7)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                if (!members.TryGetValue(update.Message!.Text, out long value))
                {
                    await SendMessage(userId, "Пользователь не найден");
                    return;
                }
                assigneeId = value;
                KeyboardButton[] buttons = ["Сегодня", "Завтра"];
                var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };
                await bot.SendTextMessageAsync(userId, "Введите дедлайн для задачи", replyMarkup: markup);

            }
            if (step == 8)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                if (update.Message!.Text == "Сегодня")
                {
                    dueTime = DateTime.Today;
                }
                else if (update.Message!.Text == "Завтра")
                {
                    dueTime = DateTime.Today.AddDays(1);
                }
                else if (!DateTime.TryParse(update.Message!.Text, out dueTime))
                {
                    await SendMessage(userId, "Не удалось распарсить время");
                    return;
                }

                var response = await clickUpApi.CreateTaskInListAsync(new ParamsCreateTaskInList(listId),
                    new RequestCreateTaskInList(taskName, taskDescription, assigneeId, dueTime));

                if (response.ResponseError != null)
                {
                    await SendMessage(userId, "Не удалось создать таску");
                    return;
                }

                string createdTaskId = response.ResponseSuccess.Id;
                if (!skipDocument)
                    await clickUpApi.CreateTaskAttachmentAsync(new ParamsCreateTaskAttachment(createdTaskId), 
                        docStream, docName);

                createdTask = response.ResponseSuccess;
                await bot.SendTextMessageAsync(userId, createdTask.Url, replyMarkup: new ReplyKeyboardRemove());
                finished = true;
            }
            step++;
        }
        async Task<bool> ParseAttachment(Telegram.Bot.Types.Update update)
        {
            string fileId;
            switch (update.Message!.Type)
            {
                case MessageType.Text:
                    await NoDocumentError(userId);
                    return false;
                case MessageType.Photo:
                    fileId = update.Message!.Photo!.Last().FileId;
                    docName = $"attachment_{update.Message!.Photo!.Last().FileUniqueId}.jpg";
                    break;
                case MessageType.Audio:
                    fileId = update.Message!.Audio!.FileId;
                    docName = update.Message!.Audio!.FileName;
                    break;
                case MessageType.Video:
                    fileId = update.Message!.Video!.FileId;
                    docName = update.Message!.Video!.FileName;
                    break;
                case MessageType.Voice:
                    fileId = update.Message!.Voice!.FileId;
                    docName = $"attachment_{update.Message!.Voice.FileUniqueId}.mp3";
                    break;
                case MessageType.Document:
                    fileId = update.Message!.Document!.FileId;
                    docName = update.Message!.Document!.FileName;
                    break;
                default:
                    return true;
            }

            await bot.GetInfoAndDownloadFileAsync(fileId, docStream);
            return true;
        }
    }
}
