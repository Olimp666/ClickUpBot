using Chinchilla.ClickUp;
using Chinchilla.ClickUp.Params;
using ClickUpBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ClickUpBot.Commands
{
    internal class GetForTodayCommand : Command
    {
        ClickUpApi clickUpApi;
        BotUser currentUser;
        public GetForTodayCommand(ITelegramBotClient _bot, long _userId) : base(_bot, _userId)
        {
            currentUser = db.GetUser(userId);
            clickUpApi = new ClickUpApi(db.GetUserToken(userId));
        }

        public override async Task Next(Update update)
        {
            var response = await clickUpApi.GetTasksAsync(new ParamsGetTasks(currentUser.DefaultTeamId));
            if (response.ResponseError != null)
            {
                await SendMessage(userId, "Не удалось получить задачи. Возможно, вы не установили проект по умолчанию /project");
                finished = true;
                return;
            }
            var tasks = response.ResponseSuccess.Tasks.Where(x => x.DueDate != null).Where(x => x.DueDate > DateTime.Today);
            string responseMessage = "Список задач на сегодня:\n";
            foreach (var task in tasks)
            {
                responseMessage += $"{task.Name} - {task.Url}\n";
            }
            await SendMessage(userId, responseMessage);
            finished = true;
        }
    }
}
