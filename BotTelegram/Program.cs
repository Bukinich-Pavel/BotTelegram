using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
//using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using System.IO;

namespace BotTelegram
{
    class Program
    {
        static ITelegramBotClient bot;
        static List<string> love = new List<string> { };
        

        static void Main(string[] args)
        {
            #region считывает из файла и записывает в Love

            string json = File.ReadAllText("LoveString.json");
            love = JsonConvert.DeserializeObject<List<string>>(json);

            #endregion

            #region Создание бота и реализация события 
            bot = new TelegramBotClient("1815629190:AAF7uqO106uo1DNsXxqNlY_XeMfKKZN03Og"); // BoolaBot
            //bot = new TelegramBotClient("1793664423:AAEht_3gBVX7H8hyFACErB1MjxjIpg6AzO4"); // MyLOveKseniya_Bot

            //bot.OnMessage += Bot_OnMessage;
            bot.OnMessage += AddStringLove;
            bot.StartReceiving();

            Console.WriteLine("Нажмите любую кнопку чтобы выйти");
            Console.ReadKey();

            bot.StopReceiving();
            #endregion

            //#region Код без библиатеки

            //string token = "1793664423:AAEht_3gBVX7H8hyFACErB1MjxjIpg6AzO4"; //тоекен MyLOveKseniya_Bot
            ////string token = "1815629190:AAF7uqO106uo1DNsXxqNlY_XeMfKKZN03Og"; //токен(адрес) бота

            //WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };

            //int update_id = 0;

             
            //string startUrl = $@"https://api.telegram.org/bot{token}/"; // создает полный адрес бота
            //bool bl = true;


            //while (true)
            //{
            //    string url = $"{startUrl}getUpdates?offset={update_id}"; //прочитать сообщение
            //    var r = wc.DownloadString(url); // скачать строку

            //    var msgs = JObject.Parse(r)["result"].ToArray(); // я хуй его знает что оно делает

            //    string userId = "866008227"; // это адрес моего акк в телеге


            //    DateTime dt = DateTime.Now.ToLocalTime(); // узнаем сколько сейчас времени

            //    Random random = new Random();
                


            //    if (dt.Hour == 7 && bl) 
            //    {
            //        string message = love[random.Next(0, love.Count)];
                    
            //        url = $"{startUrl}sendMessage?chat_id={userId}&text={message}";
            //        Console.WriteLine(wc.DownloadString(url));
            //        bl = false;
            //    }
            //    if (dt.Hour == 9)
            //    {
            //        bl = true;
            //    }

            //    if (dt.Hour == 21 && bl) 
            //    {
            //        string message = love[random.Next(0, love.Count)];
                    
            //        url = $"{startUrl}sendMessage?chat_id={userId}&text={message}";
            //        Console.WriteLine(wc.DownloadString(url));
            //        bl = false;
            //    }
            //    if (dt.Hour == 23)
            //    {
            //        bl = true;
            //    }

            //    Thread.Sleep(500);

            //}

            //#endregion
        }

        public async static void AddStringLove(object sender, MessageEventArgs e)
        {
            // выводит список признаний
            #region выводит список признаний
            Console.WriteLine(e.Message.Chat.Id);
            if (e.Message.Text == "/список")
            {
                for (int i = 0; i < love.Count; i++)
                {
                    await bot.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: $"{i}. {love[i]}");
                }

                foreach (var item in love)
                {
                    Console.WriteLine(item);
                }

                return;
            }
            #endregion

            // Удалить
            #region Удаляет выбранное по индексу пожелание
            string str = e.Message.Text;
            string number;
            if (str.Contains("/удалить"))
            {
                number = str.Substring(8);
                number = number.Trim();
                int num;
                try
                {
                    num = Convert.ToInt32(number);
                    love.RemoveAt(num);
                    string json = JsonConvert.SerializeObject(love);
                    File.WriteAllText("LoveString.json", json);

                }
                catch (Exception)
                {
                    await bot.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: "Ошибка удаления!");
                    return;
                }

                return;
            }
            #endregion

            // Принимает сообщение только от меня и сохраняет в список признаний
            #region Добавление признания
            if (e.Message.Text != null)
            {
                love.Add(e.Message.Text);   
            }

            if (e.Message.Chat.Id.ToString() == "866008227")
            {
                string json = JsonConvert.SerializeObject(love);
                File.WriteAllText("LoveString.json", json);

                await bot.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: $"Добавлено: {e.Message.Text}");
                Console.WriteLine();
                Console.WriteLine($"Добавлено: {e.Message.Text}\n");

            }
            #endregion

        }

        /// <summary>
        /// Пример
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async static void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Message.Text != null)
            {
                Console.WriteLine($"\nПолучение текстового сообщения в чате {e.Message.Chat.Id}.\n");

                await bot.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Вы написали:\n" + e.Message.Text);

                await bot.SendStickerAsync(
                    chatId: e.Message.Chat,
                    sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp");


                await bot.SendTextMessageAsync(
                    chatId: e.Message.Chat,
                    text: "Пробуем *все параметры*  метода` sendMessage` ```ghjkl``` ",
                    parseMode: ParseMode.MarkdownV2,
                    disableNotification: true,
                    replyToMessageId: e.Message.MessageId,
                    replyMarkup: new InlineKeyboardMarkup(InlineKeyboardButton.WithUrl(
                        "Проверьте метод",
                        "https://core.telegram.org/bots/api#sendmessage"))
                    );

            }
        }


    }
}
