using System;
using Telegram.Bot;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Threading.Tasks;


namespace MainApp
{
    class Program
    {
        static TelegramBotClient bot;
        static void Main(string[] args)
        {
            string token = "1277764582:AAHmxKTZwdWyaAD0jZUXFvDyC8hIUGcQ6v4";

            bot = new TelegramBotClient(token);

            bot.OnMessage += MessageListener;

            bot.StartReceiving();

            Console.ReadKey();
            
        }
        private static void MessageListener(object sender,Telegram.Bot.Args.MessageEventArgs e)
        {
            string text = $"{DateTime.Now.ToLongDateString()}: {e.Message.Chat.FirstName} {e.Message.Chat.Id} {e.Message.Text}";

            Console.WriteLine(text);

            Console.WriteLine(e.Message.Type.ToString());
            switch (e.Message.Type.ToString())
            {
                case "Text":
                    Console.WriteLine(e.Message.Text);
                    break;
                case "Photo":
                    Download(e.Message.Photo[2].FileId, $"Photo_by_{e.Message.From.FirstName}_{e.Message.MessageId}.jpg");
                break;
                case "Voice":
                    Download(e.Message.Voice.FileId, $"Voice_by_{e.Message.From.FirstName}_{e.Message.MessageId}.ogg");
                    break;
                case "Document":
                    Download(e.Message.Document.FileId, $"Vioce_by_{e.Message.From.FirstName}_{e.Message.MessageId}_{e.Message.Document.FileName}");
                    break;
                case "Sticker":
                    Console.WriteLine(e.Message.Sticker.SetName);
                    break;
                case "Audio":
                    Download(e.Message.Audio.FileId, $"Audio_by_{e.Message.From.FirstName}_{e.Message.MessageId}.{e.Message.Audio.MimeType.Substring(6)}");
                    break;
                case "Video":
                    Download(e.Message.Video.FileId, $"Video_by_{e.Message.From.FirstName}_{e.Message.MessageId}.{e.Message.Video.MimeType.Substring(6)}");
                    break;
                case "VideoNote":
                    Download(e.Message.VideoNote.FileId, $"VideoNote_by_{e.Message.From.FirstName}_{e.Message.MessageId}.mp4");
                    break;
                default:

                    break;
            }

            bot.SendTextMessageAsync(e.Message.Chat.Id, "Данные сохранены");
        }
        static async void Download(string fileid, string path)
        {
            var file = await bot.GetFileAsync(fileid);
            FileStream stream = new FileStream("_" + path, FileMode.Create);
            await bot.DownloadFileAsync(file.FilePath, stream);
            stream.Close();
            stream.Dispose();
        }
    }
}
