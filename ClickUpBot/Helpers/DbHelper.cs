using ClickUpBot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClickUpBot.Helpers
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<BotUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(CfgHelper.connectionString);
        }
    }
    class DbHelper
    {
        private ApplicationDbContext ctx = new();
        private static DbHelper db;
        private static object syncRoot = new();
        private DbHelper()
        {
        }
        public static DbHelper Instance
        {
            get
            {
                if (db == null)
                {
                    lock (syncRoot)
                    {
                        db ??= new();
                    }
                }
                return db;
            }
        }


        public void CreateUser(long id)
        {
            if (CheckIfUserExists(id))
                return;
            var newUser = new BotUser(id);
            ctx.Users.Add(newUser);
            ctx.SaveChanges();
            Console.WriteLine($"Register user {id}");
        }
        public bool CheckIfUserExists(long id)
        {
            if (ctx.Users.Find(id) == null)
            {
                return false;
            }
            return true;
        }
        public BotUser GetUser(long id)
        {
            return ctx.Users.Find(id)!;
        }
        public void SetToken(long id, string accessToken)
        {
            var user = ctx.Users.Find(id);
            if (user != null)
            {
                user.Token = accessToken;
                ctx.SaveChanges();
            }
        }
        public void SetTime(long id, TimeSpan notifTime)
        {
            var user = ctx.Users.Find(id);
            if (user != null)
            {
                user.NotifTime = notifTime;
                ctx.SaveChanges();
            }
        }
        public void SetDefaultProject(long id, string teamId, string spaceId, string listId)
        {
            var user = ctx.Users.Find(id);
            if (user != null)
            {
                user.DefaultTeamId = teamId;
                user.DefaultSpaceId = spaceId;
                user.DefaultListId = listId;
                ctx.SaveChanges();
            }
        }

        public string GetUserToken(long id)
        {
            var user = ctx.Users.Find(id);
            return user!.Token!;
        }

        public List<long> GetUsersIdsToNotify()
        {
            var now = DateTime.Now;
            var nowTimeSpan = new TimeSpan(now.Hour, now.Minute, 0);
            var userIds = ctx.Users.Where(x => x.NotifTime != null).
                Where(x => x.NotifTime!.Value == nowTimeSpan).Select(x=>x.Id).ToList();
            return userIds;
        }

    }
}
