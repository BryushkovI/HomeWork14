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
    /// Логика взаимодействия для DepartmentCreation.xaml
    /// </summary>
    public partial class DepartmentCreation : Window
    {
        Methods methods = new Methods();
        public MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
        public DepartmentCreation()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Добавляет созданный департамент внутрь выбранного одела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddInside(object sender, RoutedEventArgs e)
        {
            Department department = methods.NewDepartment(this); //создаем департамент
            methods.AddDepartmentInside(department, mainWindow.organization.Organization); //добавляем внутрь выбранного отдела
            Close();
            mainWindow.Show();
        }
        /// <summary>
        /// Закрывает окно созданя отдела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
            mainWindow.Show();
        }
        /// <summary>
        /// Добавляет департамент рядом с выделенныи отделом
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOutside(object sender, RoutedEventArgs e)
        {
            Department department = methods.NewDepartment(this);
            methods.AddDepartmentOutside(department, mainWindow.organization.Organization);
            Close();
            mainWindow.Show();
        }
        /// <summary>
        /// Проверяет является ли текущий ввод цифрой
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Percent_box_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (!(int.TryParse(e.Text, out int result) || e.Text == ","))
            {
                e.Handled = true;
            }
        }
        private void Nameing_box_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Nameing_box.Text) ||
                string.IsNullOrEmpty(Manager_box.Text) ||
                string.IsNullOrEmpty(Percent_box.Text))
            {

            }
            else
            {
                btnCreateOutside.Visibility = Visibility.Visible;
                btnCreateinside.Visibility = Visibility.Visible;
            }
        }
    }
}
