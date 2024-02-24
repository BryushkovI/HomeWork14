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
using System.Windows.Shapes;

namespace Homework_13
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        public MainWindow window = (MainWindow)App.Current.MainWindow;
        Function function = new Function();
        public AddClient()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            window.Show();
        }
        /// <summary>
        /// Создание нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Client newClient = function.AddClient(this);
            if (newClient.Type == "Entity") window.departments[0].Add(newClient);
            else if (newClient.Type == "Individual_regular") window.departments[1].Add(newClient);
            else window.departments[2].Add(newClient);
            Close();
            window.Show();
        }
        /// <summary>
        /// Получение фокуса на ячейке имени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Name_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Text = string.Empty;
            textBox.Foreground = new SolidColorBrush(Colors.Black);
        }
        /// <summary>
        /// Потеря фокуса с ячеки имени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Name_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox_Name.Foreground = new SolidColorBrush(Colors.Gray);
        }
        /// <summary>
        /// Проверка имени на пустоту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(TextBox_Name.Text) && TextBox_Name.Text!="Введите имя") Btn_Create.IsEnabled = true;
            if (string.IsNullOrEmpty(TextBox_Name.Text)) Btn_Create.IsEnabled = false;
        }
    }
}
