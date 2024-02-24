using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Windows.Controls;

namespace MainApp
{
    class Logic
    {
        private TelegramBotClient bot;

        private MainWindow w;

        public ObservableCollection<MessageItem> messageItems { get; set; }
        private void MessegeListner(object sender, MessageEventArgs e)
        {
            // Обработка добавления в коллекцию сообщений каждого типа
            switch (e.Message.Type.ToString())
            {
                case "Sticker":
                    w.Dispatcher.Invoke(() =>
                    {
                        messageItems.Add(new MessageItem(DateTime.Now.ToShortTimeString(),
                            e.Message.Chat.Id,
                            e.Message.Sticker.SetName.ToString(), 
                            e.Message.Chat.FirstName, 
                            e.Message.Type.ToString(),
                            e.Message.Sticker.FileId,
                            e.Message.Sticker.FileId));
                    });
                    break;
                case "Text":
                    w.Dispatcher.Invoke(() =>
                    {
                        messageItems.Add(new MessageItem(DateTime.Now.ToLongTimeString(), 
                            e.Message.Chat.Id, 
                            e.Message.Text, 
                            e.Message.Chat.FirstName, 
                            e.Message.Type.ToString(),
                            e.Message.Text.ToString(),
                            e.Message.Text.ToString()));
                    });
                    break;
                case "Photo":
                    w.Dispatcher.Invoke(() =>
                    {
                        messageItems.Add(new MessageItem(DateTime.Now.ToLongTimeString(),
                            e.Message.Chat.Id,
                            e.Message.Type.ToString(),
                            e.Message.Chat.FirstName,
                            e.Message.Type.ToString(),
                            e.Message.Photo[2].FileId,
                            "jpeg"));
                    });
                    break;
                case "Voice":
                    w.Dispatcher.Invoke(() =>
                    {
                        messageItems.Add(new MessageItem(DateTime.Now.ToLongTimeString(),
                            e.Message.Chat.Id,
                            e.Message.Type.ToString(),
                            e.Message.Chat.FirstName,
                            e.Message.Type.ToString(),
                            e.Message.Voice.FileId,
                            e.Message.Voice.MimeType));
                    });
                    break;
                case "Document":
                    w.Dispatcher.Invoke(() =>
                    {
                        messageItems.Add(new MessageItem(DateTime.Now.ToLongTimeString(),
                            e.Message.Chat.Id,
                            e.Message.Type.ToString(),
                            e.Message.Chat.FirstName,
                            e.Message.Type.ToString(),
                            e.Message.Document.FileId,
                            e.Message.Document.MimeType));
                    });
                    break;
                case "Video":
                    w.Dispatcher.Invoke(() =>
                    {
                        messageItems.Add(new MessageItem(DateTime.Now.ToLongTimeString(),
                            e.Message.Chat.Id,
                            e.Message.Type.ToString(),
                            e.Message.Chat.FirstName,
                            e.Message.Type.ToString(),
                            e.Message.Video.FileId,
                            e.Message.Video.MimeType));
                    });
                    break;
                case "VideoNote":
                    w.Dispatcher.Invoke(() =>
                    {
                        messageItems.Add(new MessageItem(DateTime.Now.ToLongTimeString(),
                            e.Message.Chat.Id,
                            e.Message.Type.ToString(),
                            e.Message.Chat.FirstName,
                            e.Message.Type.ToString(),
                            e.Message.VideoNote.FileId,
                            "mp4"));
                    });
                    break;
            }
        }
        /// <summary>
        /// Скачивание файла
        /// </summary>
        /// <param name="fileid">ID файла в телеграмме</param>
        /// <param name="path">путь для сохранения файла</param>
        internal async void DownLoad(string fileid,string path)
        {
            var file = await bot.GetFileAsync(fileid);
            FileStream stream = new FileStream("_" + path,FileMode.Create);
            await bot.DownloadFileAsync(file.FilePath, stream);
            stream.Close();
            stream.Dispose();
        }
        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <param name="txtMassageSend">текст сообщения в ячейке</param>
        /// <param name="id_of_sender">id адресата</param>
        internal void SandMassage(TextBox txtMassageSend, TextBlock id_of_sender)
        {
            long id = Convert.ToInt32(id_of_sender.Text);
            bot.SendTextMessageAsync(id, txtMassageSend.Text);
        }
        public Logic(MainWindow W)
        {
            this.messageItems = new ObservableCollection<MessageItem>();

            this.w = W;

            bot = new TelegramBotClient("1277764582:AAHmxKTZwdWyaAD0jZUXFvDyC8hIUGcQ6v4");

            bot.OnMessage += MessegeListner;

            bot.StartReceiving();
        }

    }
}
