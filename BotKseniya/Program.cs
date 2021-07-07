using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using System.IO;

namespace BotKseniya
{
    class Program
    {
        static List<string> love = new List<string> { };

        static void Main(string[] args)
        {

            #region Код без библиатеки

            string token = "1793664423:AAEht_3gBVX7H8hyFACErB1MjxjIpg6AzO4"; //тоекен MyLOveKseniya_Bot
            //string token = "1815629190:AAF7uqO106uo1DNsXxqNlY_XeMfKKZN03Og"; //токен(адрес) бота

            WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };

            int update_id = 0;


            string startUrl = $@"https://api.telegram.org/bot{token}/"; // создает полный адрес бота
            bool bl = true;


            while (true)
            {

                string url = $"{startUrl}getUpdates?offset={update_id}"; //прочитать сообщение
                var r = wc.DownloadString(url); // скачать строку

                var msgs = JObject.Parse(r)["result"].ToArray(); // я хуй его знает что оно делает

                //string userId = "866008227"; // это адрес моего акк в телеге
                string userId = "840306162"; // это адрес ксюши акк в телеге


                DateTime dt = DateTime.Now.ToLocalTime(); // узнаем сколько сейчас времени

                Random random = new Random();


                if (dt.Hour == 7 && bl)
                {
                    #region считывает из файла и записывает в Love

                    string json = File.ReadAllText(@"C:\Users\Ксения\source\repos\BotTelegram\BotTelegram\bin\Debug\netcoreapp3.1\LoveString.json");
                    love = JsonConvert.DeserializeObject<List<string>>(json);

                    #endregion

                    string message = love[random.Next(0, love.Count)];

                    url = $"{startUrl}sendMessage?chat_id={userId}&text={message}";
                    Console.WriteLine(wc.DownloadString(url));
                    bl = false;
                }
                if (dt.Hour >= 8 && dt.Hour <= 13)
                {
                    bl = true;
                }

                if (dt.Hour == 14 && bl)
                {
                    #region считывает из файла и записывает в Love

                    string json = File.ReadAllText(@"C:\Users\Ксения\source\repos\BotTelegram\BotTelegram\bin\Debug\netcoreapp3.1\LoveString.json");
                    love = JsonConvert.DeserializeObject<List<string>>(json);

                    #endregion

                    string message = love[random.Next(0, love.Count)];

                    url = $"{startUrl}sendMessage?chat_id={userId}&text={message}";
                    Console.WriteLine(wc.DownloadString(url));
                    bl = false;
                }
                if (dt.Hour >= 15 && dt.Hour <= 20)
                {
                    bl = true;
                }

                if (dt.Hour == 21 && bl)
                {
                    #region считывает из файла и записывает в Love

                    string json = File.ReadAllText(@"C:\Users\Ксения\source\repos\BotTelegram\BotTelegram\bin\Debug\netcoreapp3.1\LoveString.json");
                    love = JsonConvert.DeserializeObject<List<string>>(json);

                    #endregion

                    string message = love[random.Next(0, love.Count)];

                    url = $"{startUrl}sendMessage?chat_id={userId}&text={message}";
                    Console.WriteLine(wc.DownloadString(url));
                    bl = false;
                }
                if (dt.Hour >= 23 && dt.Hour >= 0 && dt.Hour >= 6)
                {
                    bl = true;
                }

                Thread.Sleep(1800000);

            }

            #endregion
        }
    }
}
