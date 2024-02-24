using System;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Unateus_bot
{
    class Program
    {
        static TelegramBotClient bot;
        static void Main(string[] args)
        {
            Methods methods = new Methods();

            Commands EventsList = methods.Events(@"commands.json");

            string API = "1667562706:AAG4-8KkWx077IcIuWgKbg-2NIUPJ6llVLA";

            bot = new TelegramBotClient(API);
            bot.SendTextMessageAsync((long)-1001122231003, "Have a good evening, team!");
            Thread.Sleep(100);
            bot.SendTextMessageAsync((long)-1001180084329, "Have a good evening, team!");
            Timer timer = new Timer(Sender, EventsList, 0, 60000);

            bot.StartReceiving();
            Console.ReadKey();
        }

        static void Sender(object obj)
        {
            Methods methods = new Methods();
            Console.Clear();
            DateTime dateTime = DateTime.Now;
            bool HasMessage = false;
            Chats chatsNigeria = methods.ChatList(@"chatsNigeria.json");
            Chats chatsKenya = methods.ChatList(@"chatsKenya.json");
            Commands EventsList = (Commands)obj;
            foreach (var e in EventsList.dailyEvents)
            {
                if (e.Hour == dateTime.Hour && e.Minute == dateTime.Minute) // for Kenya
                {
                    foreach (var i in chatsKenya.IdList)
                    {
                        bot.SendTextMessageAsync(i, e.Message, ParseMode.Html);
                        Thread.Sleep(100);
                    }
                    //bot.SendTextMessageAsync((long)-507314104, $"{e.Message}",ParseMode.Html);
                    Console.WriteLine($"It is now {e.Hour}:{e.Minute} (Moscow UTC+3).\nMessage sended.");
                    HasMessage = true;
                    break;
                }
                else if (e.Hour + 2 == dateTime.Hour && e.Minute == dateTime.Minute) // for Nigeria
                {
                    foreach (var i in chatsNigeria.IdList)
                    {
                        bot.SendTextMessageAsync(i, e.Message, ParseMode.Html);
                        Thread.Sleep(100);
                    }
                }
            }
            if(HasMessage == false)
            {
                Console.WriteLine($"It is now {dateTime.Hour}:{dateTime.Minute} (Moscow UTC+3). \nBot is active now. There is no messages yet.");
            }
        }

    }
}
