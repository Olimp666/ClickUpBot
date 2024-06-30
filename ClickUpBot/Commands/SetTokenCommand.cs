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
using Newtonsoft.Json.Linq;

namespace ClickUpBot.Commands
{
    internal class SetTokenCommand(ITelegramBotClient _bot, long _userId) : Command(_bot, _userId)
    {
        public async override Task Next(Update update)
        {
            if (update.Message!.Text == null)
                return;

            if (step == 0)
            {
                await bot.SendTextMessageAsync(update.Message!.Chat.Id, CfgHelper.authUrl);
            }
            if (step == 1)
            {
                string endpoint = "https://api.clickup.com/api/v2/oauth/token";
                string url = endpoint + $"?client_id={CfgHelper.clickupId}&client_secret={CfgHelper.clickupSecret}&code={update.Message!.Text}";

                HttpClient client = new();
                using HttpResponseMessage request = await client.PostAsync(url, null);
                if (!request.IsSuccessStatusCode)
                {
                    await bot.SendTextMessageAsync(update.Message!.From!.Id, "Не удалось получить access token\nВозможно, вы ввели неверный код авторизации");
                    return;
                }
                string response = await request.Content.ReadAsStringAsync();

                string accessToken = JObject.Parse(response)["access_token"]!.ToString();
                db.SetToken(update.Message.From!.Id, accessToken);
                finished = true;
                await bot.SendTextMessageAsync(update.Message!.From!.Id, "Access token установлен");
            }
            step++;
        }
    }
}
