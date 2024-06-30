using ClickUpBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ClickUpBot.States
{
    abstract class State(ITelegramBotClient _bot, long _userId)
    {
        protected bool finished = false;
        protected ITelegramBotClient bot = _bot;
        protected long userId = _userId;
        protected int step = 0;
        protected DbHelper db = DbHelper.Instance;

        public bool IsFinished()
        {
            return finished;
        }
        protected async Task NoTextError(long id)
        {
            await bot.SendTextMessageAsync(id, "Введите текст.");
        }
        protected async Task NoDocumentError(long id)
        {
            await bot.SendTextMessageAsync(id, "Отправьте документ.");
        }
        protected async Task SendMessage(long id, string message)
        {
            await bot.SendTextMessageAsync(id, message);
        }
        public abstract Task Next(Update update);
    }
}
