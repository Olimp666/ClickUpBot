﻿using ClickUpBot.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using Telegram.Bot;
using Telegram.Bot.Types;
using Chinchilla.ClickUp;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using ClickUpBot.Models;
using Chinchilla.ClickUp.Params;
using Chinchilla.ClickUp.Requests;
using Chinchilla.ClickUp.Responses.Model;

namespace ClickUpBot.States
{
    internal class CreateTaskState : State
    {
        Dictionary<string, string> teams = [];
        Dictionary<string, string> spaces = [];
        Dictionary<string, string> lists = [];

        MemoryStream docStream = new MemoryStream();
        string? taskName;
        string? taskDescription;
        string? docName;
        string? docMimeType;
        string? taskTeam;
        string? taskSpace;
        string? taskList;

        string? listId;

        ResponseModelTask? createdTask;

        ClickUpApi clickUpApi;
        BotUser currentUser;
        public CreateTaskState(ITelegramBotClient _bot, long _userId) : base(_bot, _userId)
        {
            currentUser = db.GetUser(userId);
            clickUpApi = new ClickUpApi(db.GetUserToken(userId));
        }

        public async override Task Next(Telegram.Bot.Types.Update update)
        {
            long userId = update.Message!.From!.Id;
            if (step == 0)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                await bot.SendTextMessageAsync(userId, "Введите название таски");
            }
            if (step == 1)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                taskName = update.Message!.Text;
                await bot.SendTextMessageAsync(userId, "Введите описание таски");
            }
            if (step == 2)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                KeyboardButton[] buttons = { "Пропустить" };
                var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };
                taskDescription = update.Message!.Text;
                await bot.SendTextMessageAsync(userId, "Приложите документ (опционально)", replyMarkup: markup);
            }
            if (step == 3)
            {
                bool skipDocument = false;
                if (update.Message!.Text == "Пропустить")
                {
                    skipDocument = true;
                }
                if (!skipDocument)
                {
                    if (update.Message!.Document == null)
                    {
                        await NoDocumentError(userId);
                        return;
                    }

                    var doc = update.Message!.Document;
                    await bot.GetInfoAndDownloadFileAsync(doc.FileId, docStream);
                    docName = doc.FileName!;
                    docMimeType = doc.MimeType!;
                }

                string responseMessage = "Выберите команду\n";
                var response = await clickUpApi.GetAuthorizedTeamsAsync();
                foreach (var team in response.ResponseSuccess.Teams)
                {
                    responseMessage += $"{team.Name}\n";
                    teams.Add(team.Name, team.Id);
                }
                string? curTeamId = currentUser.DefaultTeamId;
                bool isTeamIdNull = curTeamId == null;
                KeyboardButton[] buttons = { isTeamIdNull ? "" : teams.First(x => x.Value == curTeamId).Key };
                var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };

                await bot.SendTextMessageAsync(userId, responseMessage, replyMarkup: isTeamIdNull ? new ReplyKeyboardRemove() : markup);

            }
            if (step == 4)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                if (!teams.ContainsKey(update.Message!.Text))
                {
                    await SendMessage(userId, "Тима не найдена");
                    return;
                }

                string responseMessage = "Выберите спейс\n";
                var teamId = teams[update.Message!.Text];
                var response = await clickUpApi.GetTeamSpacesAsync(new ParamsGetTeamSpaces(teamId));
                foreach (var space in response.ResponseSuccess.Spaces)
                {
                    responseMessage += $"{space.Name}\n";
                    spaces.Add(space.Name, space.Id);
                }
                string? curSpaceId = currentUser.DefaultSpaceId;
                bool isSpaceIdNull = curSpaceId == null;
                KeyboardButton[] buttons = { isSpaceIdNull ? "" : spaces.First(x => x.Value == curSpaceId).Key };
                var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };

                await bot.SendTextMessageAsync(userId, responseMessage, replyMarkup: isSpaceIdNull ? new ReplyKeyboardRemove() : markup);

            }
            if (step == 5)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                if (!spaces.ContainsKey(update.Message!.Text))
                {
                    await SendMessage(userId, "Спейс не найден");
                    return;
                }

                string responseMessage = "Выберите лист\n";
                var spaceId = spaces[update.Message!.Text];
                var response = await clickUpApi.GetFolderlessListsAsync(new ParamsGetFolderlessLists(spaceId));
                foreach (var list in response.ResponseSuccess.Lists)
                {
                    responseMessage += $"{list.Name}\n";
                    lists.Add(list.Name, list.Id);
                }
                listId = currentUser.DefaultListId;
                bool isListIdNull = listId == null;
                KeyboardButton[] buttons = { isListIdNull ? "" : lists.First(x => x.Value == listId).Key };
                var markup = new ReplyKeyboardMarkup(buttons) { ResizeKeyboard = true };

                await bot.SendTextMessageAsync(userId, responseMessage, replyMarkup: isListIdNull ? new ReplyKeyboardRemove() : markup);

            }
            if (step == 6)
            {
                if (update.Message!.Text == null)
                {
                    await NoTextError(userId);
                    return;
                }
                if (!lists.ContainsKey(update.Message!.Text))
                {
                    await SendMessage(userId, "Лист не найден");
                    return;
                }

                var response = await clickUpApi.CreateTaskInListAsync(new ParamsCreateTaskInList(listId),
                    new RequestCreateTaskInList(taskName, taskDescription));

                if (response.ResponseError != null)
                {
                    await SendMessage(userId, "Не удалось создать таску");
                    return;
                }
                createdTask = response.ResponseSuccess;
                await SendMessage(userId, createdTask.Url);

            }
            step++;
        }
    }
}