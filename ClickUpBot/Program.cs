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

        var botClient = new TelegramBotClient(CfgHelper.tgToken);

        var botService = new BotService(botClient);


        Console.WriteLine($"Bot is running...");
        Console.ReadLine();
    }

}
