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
    /// Логика взаимодействия для NewCredit.xaml
    /// </summary>
    public partial class NewCredit : Window
    {
        FrontEndMethods methods = new FrontEndMethods();
        MainWindow window = (MainWindow)App.Current.MainWindow;
        public NewCredit()
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
        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            Close();
            window.Show();
        }
        /// <summary>
        /// Фокус на поле суммы кредита
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
        /// Доступность\ недоступность кнопки подтвержденя кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sum_TextChanged(object sender, TextChangedEventArgs e)
        {
            methods.EnabledButton(sender, this);
        }
        /// <summary>
        /// Подтверждение выдачи кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_btn_Click(object sender, RoutedEventArgs e)
        {
            Function function = new Function();
            function.OpenNewCredit(window, this);
            Close();
            window.Show();
        }
    }
}
