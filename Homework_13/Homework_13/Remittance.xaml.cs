using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Homework_13
{
    /// <summary>
    /// Логика взаимодействия для Remittance.xaml
    /// </summary>
    public partial class Remittance : Window
    {
        private readonly Function function = new Function();
        private readonly MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        readonly public Methods methods = new Methods();
        readonly private FrontEndMethods frontEndMethods = new FrontEndMethods();
        public Remittance()
        {
            InitializeComponent();

            TxtBlockName.Text = mainWindow.Name.Text;
            ObservableCollection<Client> clients = methods.AllClients(mainWindow.departments);
            clients.Remove(SelfRemittance(clients));
            List.ItemsSource = clients;
            SelfRemittance(clients);
            TxtBlockNumber.Text = mainWindow.Number.Text;
            TxtBlockBank_account.Text = mainWindow.Bank_account.Text;
        }
        /// <summary>
        /// Убирает адресата из списка адресантов
        /// </summary>
        /// <param name="clients"></param>
        /// <returns></returns>
        private Client SelfRemittance(ObservableCollection<Client> clients)
        {
            foreach(var e in clients)
            {
                if (e is Client client && client.Name == TxtBlockName.Text)
                {
                    return e;
                }
            }
            return null;
        }
        /// <summary>
        /// Проверяет вводимый тескт на наличие чисел
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txtbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = frontEndMethods.InputTextCheck(e);
        }
        /// <summary>
        /// Отмена
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }
        /// <summary>
        /// Получение фокуса на текстовом поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txtbox_Sum_GotFocus(object sender, RoutedEventArgs e)
        {
            Txtbox_Sum.Foreground = new SolidColorBrush(Colors.Black);
            Txtbox_Sum.Text = "";
        }
        /// <summary>
        /// Снятие фокуса с текстового поля
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txtbox_Sum_LostFocus(object sender, RoutedEventArgs e)
        {
            Txtbox_Sum.Foreground = new SolidColorBrush(Colors.Gray);
        }
        /// <summary>
        /// Досупность/недоступность кнопки перевода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            frontEndMethods.Remittance_Selection_Change(this);
        }
        /// <summary>
        /// Осуществление перевода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Remittance_Click(object sender, RoutedEventArgs e)
        {
            Client client = (Client)List.SelectedItem;
            function.Remit(int.Parse(TxtBlockNumber.Text), client.Number, int.Parse(Txtbox_Sum.Text));
            mainWindow.Show();
            Close();
        }
        /// <summary>
        /// Подтверждение введенной суммы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Txtbox_Sum_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                List.IsEnabled = true;
            }
        }
    }
}
