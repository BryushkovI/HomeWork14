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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Methods methods = new Methods();

        public Org organization;


        public MainWindow()
        {
            InitializeComponent();

            organization = methods.DeserializeOrgJSON(@"Org.json"); //Инициализаця организации

            OrgTree.ItemsSource = organization.Organization; //назначение источника данных из огранизации
            CEO_name.Text = organization.CEO.Name;
            CEO_Salary.Text = organization.CEO.Salary.ToString();
        }

        /// <summary>
        /// Событие при выделении узла
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (OrgTree.SelectedItem is Department department)
            {
                SelectedDepartment.Text = department.Nameing; //Изменение названия выделенного депрартамента
                Employee_list.ItemsSource = department.Employees;
                Manager_Name.Text = department.Manager.Name;
                Manager_salary.Text = department.Total_Salary.ToString();
                Manager_Employees.Text = department.Employees.Count.ToString();
            }
        }
        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            methods.SerializeOrgJSON(organization, @"Org.json"); // Сохраняем организацию
            Close();
        }
    }
}
