using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Методы упрвления организацией
    /// </summary>
    public class Methods
    {
        MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
        /// <summary>
        /// Назначение ЗП менеджеру
        /// </summary>
        /// <param name="department">Депвртамент менеджера</param>
        /// <returns></returns>
        public uint Salary_of_Manager(Department department)
        {
            uint Sum = 0;
            if (department.Employees != null)
            {
                foreach (var e in department.Employees)
                {
                    Sum += e.Salary;
                }
            }
            if (Sum < 1300)
            {
                return 1300;
            }
            return Convert.ToUInt32(Math.Round(Sum * Convert.ToDouble(department.Manager.Percent)/100));
        }
        /// <summary>
        /// ЗП руководителя считается как процент от суммы ЗП всех менеджеров
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public uint CEO_Salary(ObservableCollection<Department> departments, Manager CEO)
        {
            uint Salary = 0;
            foreach(var e in departments)
            {
                if (e.Departments != null)
                {
                    Salary += CEO_Salary(e.Departments, CEO);
                }

                Salary += Salary_of_Manager(e);
            }
            Salary = Convert.ToUInt32(Math.Round(Salary * Convert.ToDouble(CEO.Percent)/100));
            if (Salary < 1300)
            {
                return 1300;
            }
            return Salary;
        }
        /// <summary>
        /// Сортирует спиоск сотрудников в выбранном департаменте по выделенной колонке
        /// </summary>
        /// <param name="gridViewColumnHeader">Экземпляр выделенной колонки</param>
        /// <param name="employees">Начальный список сотрудников</param>
        /// <returns>Отсортированный список сотрудников</returns>
        public List<Employee> Sorter(GridViewColumnHeader gridViewColumnHeader, List<Employee> employees)
        {
            var newEmployees = employees.ToList(); //новый список сотрудников
            switch (gridViewColumnHeader.Content.ToString())
            {
                case "Зарплата":
                    newEmployees.Sort(Employee.SortedBy(SortCriterion.Salary)); // сортировка по ЗП
                    return newEmployees;
                case "Имя":
                    newEmployees.Sort(Employee.SortedBy(SortCriterion.Name)); // Сортировка по Имени
                    return newEmployees;
                case "Должность":
                    newEmployees.Sort(Employee.SortedBy(SortCriterion.Position)); //Сортировка по названию должности
                    return newEmployees;
                default:
                    return newEmployees;
            }
        }
        /// <summary>
        /// Создание новго сотрудника
        /// </summary>
        /// <param name="window">Ссылка на текущее окно</param>
        /// <returns></returns>
        public Employee NewEmployee(EmployeeCreation window)
        {
            if (window.EmployeeStatus.SelectedItem.ToString() == "Intern")
            {
                Employee employee = new Intern(window.EmployeeStatus.SelectedItem.ToString())
                {
                    Status = window.EmployeeStatus.SelectedItem.ToString(),
                    Department = mainWindow.SelectedDepartment.Text,
                    Salary = uint.Parse(window.Box_Salary.Text)
                };
                if (window.Box_Name.Text != null) employee.Name = window.Box_Name.Text;
                if (window.Box_Position.Text != null) employee.Position = window.Box_Position.Text;
                return employee;
            }
            else
            {
                Employee employee = new Worker(window.EmployeeStatus.SelectedItem.ToString())
                {
                    WorkHours = uint.Parse(window.Box_WorkHours.Text),
                    HourSalary = uint.Parse(window.Box_Salary.Text),
                    Status = window.EmployeeStatus.SelectedItem.ToString(),
                    Department = mainWindow.SelectedDepartment.Text
                };
                if (window.Box_Name.Text != null) employee.Name = window.Box_Name.Text;
                if (window.Box_Position.Text != null) employee.Position = window.Box_Position.Text;
                return employee;
            }
        }
        /// <summary>
        /// Поиск департамента для сотрудника и добавление сотрудника в департамент
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="departments"></param>
        public void AddEmployee(Employee employee, ObservableCollection<Department> departments)
        {
            foreach(var e in departments)
            {
                if (e.Nameing == employee.Department) //если в данном вложении есть отдел с таким названием
                {
                    e.Employees.Add(employee); //то добавляем в него
                }
                else if (e.Departments != null) AddEmployee(employee, e.Departments);//если нет, то ищем глубже
            }
        }
        /// <summary>
        /// Увольняет выбранного из списка сотрудника
        /// </summary>
        /// <param name="employee"></param>
        /// <param name="departments"></param>
        public void DeliteEmployee(Employee employee, ObservableCollection<Department> departments)
        {
            foreach(var e in departments)
            {
                if (e.Employees.Contains(employee)) // если содержит такого сотрудника
                {
                    e.Employees.Remove(employee); //то удаляем его
                }
                else if (e.Departments != null)
                {
                    DeliteEmployee(employee, e.Departments);// если нет, то ищем глубже
                }
            }
        }
        /// <summary>
        /// Создание департамента
        /// </summary>
        /// <param name="window">Ссылка на окно создания департамента</param>
        /// <returns></returns>
        public Department NewDepartment(DepartmentCreation window)
        {
            /*
             * Для отела будет создан менеджер автоматически
             */
            Department department = new Department()
            {
                Nameing = window.Nameing_box.Text,
                Manager = new Manager("Manager")
                {
                    Name = window.Manager_box.Text,
                    Salary = 1300,
                    Percent = uint.Parse(window.Percent_box.Text),
                    Department = window.Nameing_box.Text,
                    Position = $"Руководитель {window.Nameing_box.Text}"
                },
                Employees = new ObservableCollection<Employee>(),
                Departments = new ObservableCollection<Department>()
            };
            return department;
        }
        /// <summary>
        /// Добавление нового департамента внутрь указанного
        /// </summary>
        /// <param name="department"></param>
        /// <param name="departments"></param>
        public void AddDepartmentInside(Department department,ObservableCollection<Department> departments)
        {
            foreach(var e in departments)
            {
                if (e.Nameing == mainWindow.SelectedDepartment.Text)
                {
                    e.Departments.Add(department);
                }
                else if (e.Departments != null) AddDepartmentInside(department, e.Departments);
            }
        }
        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="department">Удаляемй департамент</param>
        /// <param name="departments">Список департаментов</param>
        public void DeliteDepartment(Department department, ObservableCollection<Department> departments)
        {
            if (departments.Contains(department))
            {
                departments.Remove(department);
            }
            else
            {
                foreach(var e in departments)
                {
                    DeliteDepartment(department, e.Departments);
                }
            }
        }
        /// <summary>
        /// Добавление департамента рядом с исходным
        /// </summary>
        /// <param name="department">новый департамент</param>
        /// <param name="departments">корневой список департаментов</param>
        public void AddDepartmentOutside(Department department,ObservableCollection<Department> departments)
        {
            if (departments.Contains((Department)mainWindow.OrgTree.SelectedItem))
            {
                departments.Add(department);
            }
            else
            {
                foreach(var e in departments)
                {
                    AddDepartmentOutside(department, e.Departments);
                }
            }
        }
    }
}
