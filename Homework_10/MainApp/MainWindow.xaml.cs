using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Telegram.Bot.Args;
using Telegram.Bot;
using System.Collections.ObjectModel;
using MainApp;
using System.IO;
using Newtonsoft.Json.Linq;

namespace MainApp
{

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Logic client;  // экземепляр бота

        public MainWindow()
        {
            InitializeComponent();

            client = new Logic(this);

            logList.ItemsSource = client.messageItems;
        }

        private void btnMessageSend_Click(object sender, RoutedEventArgs e)
        {
            client.SandMassage(txtMassageSend, Id_of_sender); // Отправляет сообщение, если нажать на кнопку "отправить"
            txtMassageSend.Text = "Введите сообщение";
        }

        private void txtMassageSend_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (txtMassageSend.Text == null | txtMassageSend.Text == "Введите сообщение") // двойной клик убирает введенный базовый текст
            {
                txtMassageSend.Text = "";
            }
        }

        private void txtMassageSend_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) // при отправке сообщения базовый текст возвращается
            {
                client.SandMassage(txtMassageSend, Id_of_sender);
                txtMassageSend.Text = txtMassageSend.Text = "Введите сообщение";
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (client.messageItems[logList.SelectedIndex].Type != "Text" & client.messageItems[logList.SelectedIndex].Type != "Sticker")
            {
                download_btn.Visibility = Visibility.Visible; // если файл можно скачать, то будет выведена кнопка для скачки
            }
            else
            {
                download_btn.Visibility = Visibility.Hidden;
            }
        }

        private void download_btn_Click(object sender, RoutedEventArgs e)
        {
            bool flag = false;
            string mime = client.messageItems[logList.SelectedIndex].Mime; // по умолчанию расширение файла есть в объекте сообщения
            foreach(var i in client.messageItems[logList.SelectedIndex].Mime) // цикл для определения расширения файла
            {
                if (i == '/')
                {
                    flag = true;
                    mime = "";
                    continue;
                }
                if (flag == true)
                {
                    mime += i;
                }
            }

            client.DownLoad(client.messageItems[logList.SelectedIndex].FileID, 
                $"{client.messageItems[logList.SelectedIndex].FirstName}_" +
                $"{logList.SelectedIndex}." +
                $"{mime}"); // скачивание файла
        }

        private void saveHistory_Click(object sender, RoutedEventArgs e)
        {
            JArray Messages = new JArray();
            foreach (var i in client.messageItems)
            {
                JObject message = new JObject()
                {
                    ["Time"] = i.Time,
                    ["Id"]=i.Id,
                    ["Message"] = i.Message,
                    ["FirstName"] = i.FirstName,
                    ["Type"] = i.Type,
                    ["FileID"] = i.FileID,
                    ["Mime"] = i.Mime
                };
                Messages.Add(message);
            }
            string JSON = Messages.ToString();
            File.WriteAllText("History.json", JSON);
        }
    }
}
