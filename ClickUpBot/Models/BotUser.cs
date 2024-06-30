using ClickUpBot.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace ClickUpBot.Models
{
    public class BotUser
    {
        public long Id { get; set; }
        public string? DefaultTeamId { get; set; }
        public string? DefaultSpaceId { get; set; }
        public string? DefaultListId { get; set; }
        public string? Token { get; set; }
        public TimeSpan? NotifTime { get; set; }
        public BotUser(long id)
        {
            Id = id;
            DefaultTeamId = null;
            DefaultSpaceId = null;
            DefaultListId = null;
            Token = null;
            NotifTime = null;
        }
    }
}
