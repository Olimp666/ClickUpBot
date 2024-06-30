using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ClickUpBot.Commands
{
    internal class EmptyCommand : Command
    {
        public EmptyCommand(ITelegramBotClient _bot, long _userId) : base(_bot, _userId)
        {
        }

        public override async Task Next(Update update)
        {
            finished = true;
        }
    }
}
