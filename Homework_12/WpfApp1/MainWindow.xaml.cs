using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Methods methods = new Methods();

        public FileSystemMethods FileSystemMethods = new FileSystemMethods();

        public Org organization;

        public MainWindow()
        {
            InitializeComponent();

            organization = FileSystemMethods.DeserializeOrgJSON(@"Org.json"); //Инициализаця организации

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
                Manager_salary.Text = department.Manager.Salary.ToString();
                if (department.Employees != null)
                {
                    Manager_Employees.Text = department.Employees.Count.ToString();
                }
                else Manager_Employees.Text = "0";
            }
            Employee_Creation_btn.Visibility = Visibility.Visible;
            Department_Creation_btn.Visibility = Visibility.Visible;
            Department_Delite_btn.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// Закрытие окна
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FileSystemMethods.SerializeOrgJSON(organization, @"Org.json"); // Сохраняем организацию
            Close();
        }
        /// <summary>
        /// Вывод выбранного сотрудника на экран для редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Employees_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            WorkerProperties.Visibility = Visibility.Visible; //если выделен сотрудник, показываем его описание
            if (Employee_list.SelectedItem is Intern)
            {
                Textbox_workhours.Text = "-";
            }
            Employee_delite_btn.Visibility = Visibility.Visible;
            if (Employee_list.SelectedItem == null)
            {
                Employee_delite_btn.Visibility = Visibility.Hidden; // если не выделен, то не показываем
            }
        }
        /// <summary>
        /// Сортировка по выбранному столбцу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeaderSorter(object sender, RoutedEventArgs e)
        {
            Department department = (Department)OrgTree.SelectedItem; //выбранный департамент
            List<Employee> employees = department.Employees.ToList(); //сотрудинки выбранного департамента
            GridViewColumnHeader gridViewColumnHeader = sender as GridViewColumnHeader; // заголовок
            Employee_list.ItemsSource = methods.Sorter(gridViewColumnHeader, employees);// сортированный по выбранному полю
        }
        /// <summary>
        /// Переход к созданию нового сотрудника
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EmployeeCreation employeeCreation = new EmployeeCreation(); //окно создания сотрудника
            employeeCreation.Show();
            this.Hide();
        }
        /// <summary>
        /// Удаляет выбранного сотрудинка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Employee_delite_btn_Click(object sender, RoutedEventArgs e)
        {
            methods.DeliteEmployee((Employee)Employee_list.SelectedItem, organization.Organization);
        }
        /// <summary>
        /// Переход к созданию нового отдела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Department_Creation_btn_Click(object sender, RoutedEventArgs e)
        {
            DepartmentCreation departmentCreation = new DepartmentCreation(); //окно создания ного депаратмента
            departmentCreation.Show();
            Hide();
        }
        /// <summary>
        /// Удаление выбранного отдела
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Department_Delite_btn_Click(object sender, RoutedEventArgs e)
        {
            Department department = OrgTree.SelectedItem as Department;
            if(department.Departments.Count != 0 || department.Employees.Count != 0) //проверяем наличие сотрудинков и отелов в отделе
            {
                DeliteDepDenied deliteDepDenied = new DeliteDepDenied(); //если есть, то запрещаем удалять отдел
                deliteDepDenied.Show();
                Hide();
            }
            else methods.DeliteDepartment((Department)OrgTree.SelectedItem, organization.Organization); 
            // если нет, то удаляем
        }
    }
}
