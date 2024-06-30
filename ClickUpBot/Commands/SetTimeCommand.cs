using ClickUpBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Newtonsoft.Json;
using System.Security.Principal;
using Telegram.Bot.Types.ReplyMarkups;

namespace ClickUpBot.Commands
{
    internal class SetTimeCommand(ITelegramBotClient _bot, long _userId) : Command(_bot, _userId)
    {
        KeyboardButton[] buttons = { "10:00", "12:00", "16:00", "18:00" };

        public async override Task Next(Update update)
        {
            if (update.Message!.Text == null)
                return;

            var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };
            if (step == 0)
            {
                await bot.SendTextMessageAsync(update.Message!.From!.Id, "В какое время напоминать о задачах?", replyMarkup: markup);
            }
            if (step == 1)
            {
                if (!TimeSpan.TryParse(update.Message!.Text, out TimeSpan span))
                {
                    await bot.SendTextMessageAsync(update.Message!.From!.Id, "Не удалось распарсить время", replyMarkup: markup);
                    return;
                }
                db.SetTime(update.Message!.From!.Id, span);
                await bot.SendTextMessageAsync(update.Message!.From!.Id, "Время установлено", replyMarkup: new ReplyKeyboardRemove());
                finished = true;
            }
            step++;
        }
    }
}
