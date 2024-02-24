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
    /// Логика взаимодействия для NewDeposite.xaml
    /// </summary>
    public partial class NewDeposite : Window
    {
        FrontEndMethods methods = new FrontEndMethods();
        MainWindow window = (MainWindow)App.Current.MainWindow;
        public NewDeposite()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Проверка вводимой суммы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = methods.InputTextCheck(e);
        }
        /// <summary>
        /// отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            window.Show();
        }
        /// <summary>
        /// Получение фокуса на поле для суммы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sum_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            textBox.Foreground = new SolidColorBrush(Colors.Black);
            textBox.Text = "";
        }
        /// <summary>
        /// Доступность \ недоступность кнопки подстверждения
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sum_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text != "Введите сумму вклада") Confirm_Btn.IsEnabled = true;
            if (string.IsNullOrEmpty(textBox.Text)) Confirm_Btn.IsEnabled = false;
        }
        /// <summary>
        /// Подтверждение открытия депозита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Btn_Click(object sender, RoutedEventArgs e)
        {
            Function function = new Function();
            function.OpenNewDeposite(window, this);
            Close();
            window.Show();
        }
    }
}
