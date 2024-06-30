using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ClickUpBot.States
{
    internal class EmptyState : State
    {
        public EmptyState(ITelegramBotClient _bot, long _userId) : base(_bot, _userId)
        {
        }

        public override async Task Next(Update update)
        {
            finished = true;
        }
    }
}
