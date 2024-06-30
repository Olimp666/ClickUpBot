using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chinchilla.ClickUp;
using ClickUpBot.Helpers;
using Chinchilla.ClickUp.Params;
using Telegram.Bot.Types.Enums;

namespace ClickUpBot.States
{
    internal class SetProjectState : State
    {
        Dictionary<string, string> teams = [];
        Dictionary<string, string> spaces = [];
        Dictionary<string, string> lists = [];

        string? teamId;
        string? spaceId;
        string? listId;

        ClickUpApi clickUpApi;
        public SetProjectState(ITelegramBotClient _bot, long _userId) : base(_bot, _userId)
        {
            clickUpApi = new ClickUpApi(db.GetUserToken(userId));
        }

        public override async Task Next(Update update)
        {
            if (update.Message!.Text == null)
                return;

            string responseMessage = "";
            if (step == 0)
            {
                responseMessage += "Выберите команду:\n";
                var response = await clickUpApi.GetAuthorizedTeamsAsync();
                foreach (var team in response.ResponseSuccess.Teams)
                {
                    responseMessage += $"`{team.Name}`\n";
                    teams.Add(team.Name, team.Id);
                }
            }
            if (step == 1)
            {
                responseMessage += "Выберите спейс:\n";
                teamId = teams[update.Message!.Text!];
                var response = await clickUpApi.GetTeamSpacesAsync(new ParamsGetTeamSpaces(teamId));
                foreach (var space in response.ResponseSuccess.Spaces)
                {
                    responseMessage += $"`{space.Name}`\n";
                    spaces.Add(space.Name, space.Id);
                }
            }
            if (step == 2)
            {
                responseMessage += "Выберите лист:\n";
                spaceId = spaces[update.Message!.Text!];
                var response = await clickUpApi.GetFolderlessListsAsync(new ParamsGetFolderlessLists(spaceId));
                foreach (var list in response.ResponseSuccess.Lists)
                {
                    responseMessage += $"`{list.Name}`\n";
                    lists.Add(list.Name, list.Id);
                }
            }
            if (step == 3)
            {
                responseMessage += "Проект по умолчанию установлен";
                listId = lists[update.Message!.Text!];
                db.SetDefaultProject(update.Message!.From!.Id, teamId, spaceId, listId);
                finished = true;
            }
            await bot.SendTextMessageAsync(update.Message!.From!.Id, responseMessage, parseMode: ParseMode.MarkdownV2);
            step++;
        }
    }
}
