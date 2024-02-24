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
using System.Collections.ObjectModel;

namespace Homework_13
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly FileMethods fileMethods = new FileMethods();
        readonly Methods methods = new Methods();
        readonly FrontEndMethods frontEndMethods = new FrontEndMethods();
        public ObservableCollection<Department<Client>> departments;
        public MainWindow()
        {
            InitializeComponent();

            departments = fileMethods.DeserializeClients(@"Clients.json");

            Entity_List.ItemsSource = departments[0].ClientList;
            Individual_regular_List.ItemsSource = departments[1].ClientList;
            Individual_VIP_List.ItemsSource = departments[2].ClientList;
        }
        /// <summary>
        /// Открытие окна перевода
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Remittance_Click(object sender, RoutedEventArgs e)
        {
            Remittance remittance = new Remittance();
            Hide();
            remittance.Show();
        }
        /// <summary>
        /// Закрытие программы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            fileMethods.SerializeClients(departments, @"Clients.json");
            Close();
        }
        /// <summary>
        /// Переход по вкладкам (отделам)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Entity_List.SelectedItem = null;
            Individual_regular_List.SelectedItem = null;
            Individual_VIP_List.SelectedItem = null;
            Remittance1.IsEnabled = false;
        }
        /// <summary>
        /// Действия при выборе любого клиента (вывод информации о нем)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Individual_VIP_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            frontEndMethods.List_Selection_Changed(sender, this);
        }
        /// <summary>
        /// Создание нового клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Hide();
            AddClient addClient = new AddClient();
            addClient.Show();
        }
        /// <summary>
        /// Изменение названия или суммы вклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            frontEndMethods.Edit_Client(sender, this);
        }
        /// <summary>
        /// Открытие кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenCreditBtn_Click(object sender, RoutedEventArgs e)
        {
            NewCredit newCredit = new NewCredit();
            newCredit.Show();
            Hide();
        }
        /// <summary>
        /// Открытие вклада
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenDepositeBtn_Click(object sender, RoutedEventArgs e)
        {
            NewDeposite newDeposite = new NewDeposite();
            newDeposite.Show();
            Hide();
        }
        /// <summary>
        /// Окно удаления клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_deliteClient_Click(object sender, RoutedEventArgs e)
        {
            DeliteClient deliteClient = new DeliteClient();
            deliteClient.Show();
            Hide();
        }
        /// <summary>
        /// Перемотка времени
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Predict_Click(object sender, RoutedEventArgs e)
        {
            frontEndMethods.Predict(this);
        }
        /// <summary>
        /// Проверка вводимых лет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Predict_years_text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = frontEndMethods.InputTextCheck(e);
        }
        /// <summary>
        /// Проверка вводимых месяцев
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Predict_month_text_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = frontEndMethods.InputTextCheck(e);
        }
    }
}
