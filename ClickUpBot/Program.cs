using Telegram.Bot;
using System.Configuration;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using ClickUpBot;
using Chinchilla.ClickUp.Params;
using Chinchilla.ClickUp.Requests;
using ClickUpBot.Models;
using ClickUpBot.Helpers;
using Chinchilla.ClickUp;

class Program
{
    static async Task Main(string[] args)
    {
        //ClickUpApi api = new ClickUpApi("68479552_2745f2bef927358e26affd3f17a2e0b114d80094e41372a3190d7c7cb2ff107e");
        //var @params= new ParamsCreateTaskInList("listid");
        //var request = new RequestCreateTaskInList("name","desc");

        var botClient = new TelegramBotClient(CfgHelper.tgToken);

        var botService = new BotService(botClient);

        var me = await botClient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
    }
}
