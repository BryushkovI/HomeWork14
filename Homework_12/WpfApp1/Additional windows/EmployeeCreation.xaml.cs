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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для EmployeeCreation.xaml
    /// </summary>
    public partial class EmployeeCreation : Window
    {
        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        Methods methods = new Methods();
        public EmployeeCreation()
        {
            InitializeComponent();
            List<string> Statuses = new List<string>
            {
                "Intern",
                "Worker"
            }; //список статусов сотрудника для выбора
            EmployeeStatus.ItemsSource = Statuses;
        }
        /// <summary>
        /// Создание нового работника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Box_WorkHours.Text)) Box_WorkHours.Text = "0";
            if (string.IsNullOrEmpty(Box_Salary.Text)) Box_Salary.Text = "0";
            Employee employee = methods.NewEmployee(this); //создаем сотрудника
            methods.AddEmployee(employee, mainWindow.organization.Organization); //добавяем его в отдел
            mainWindow.Show();
            Close();
        }
        /// <summary>
        /// Выбор типа добавляемого работника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmployeeStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
             Если выбранный статус Worker, то появляются дополнительные поля
             */
            if (EmployeeStatus.SelectedItem.ToString() == "Worker")
            {
                Box_WorkHours.IsEnabled = true;
                Block_salary.Text = "Зарплата в час";
                btnCreate.IsEnabled = true;
            }
            else
            {
                Block_salary.Text = "Зарплата";
                btnCreate.IsEnabled = true;
            }
        }
        /// <summary>
        /// Отмена ввода новго сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            mainWindow.Show();
            Close();
        }

        private void Box_Salary_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(int.TryParse(e.Text, out int result) || e.Text == ","))
            {
                e.Handled = true;
            }
        }

    }
}
