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

        //string access_token = "68479552_2745f2bef927358e26affd3f17a2e0b114d80094e41372a3190d7c7cb2ff107e";
        //string list_id = "901505866725";
        //string file_id = "BQACAgIAAxkBAAIBJWaBX8Er9EaP9sQV7_AMHa9XEkqAAALaTQACcDsISHfrhLmreFGtNQQ";
        //string task_id = "86bzdqmn8";

        //ClickUpApi api = new ClickUpApi(access_token);

        var botClient = new TelegramBotClient(CfgHelper.tgToken);

        var botService = new BotService(botClient);

        var me = await botClient.GetMeAsync();

        Console.WriteLine($"Start listening for @{me.Username}");
        Console.ReadLine();
    }
}
