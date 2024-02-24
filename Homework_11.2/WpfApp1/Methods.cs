using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    /// <summary>
    /// Методы упрвления организацией
    /// </summary>
    public class Methods
    {
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
        /// Рекурсивный метод для сериализации организации
        /// </summary>
        /// <param name="departments">Список департментов</param>
        /// <returns>Сериализованный список департаментов</returns>
        public JArray List_Deps(ObservableCollection<Department> departments)
        {
            JArray ListDeps = new JArray();
            foreach (var e in departments) // разбираем каждый объект списка департаментов
            {
                JObject dep = new JObject();
                if (e.Departments != null) //если в департаменте есть департаменты, 
                {                          // то проводим то же самое в этом списке
                    dep["Departments"] = List_Deps(e.Departments);
                }
                else dep["Departments"] = null;
                dep["Nameing"] = e.Nameing;

                JObject Manager = new JObject();
                if (Manager != null) // находим руководителя отдела
                {
                    Manager["Name"] = e.Manager.Name;
                    Manager["Position"] = e.Manager.Position;
                    Manager["Salary"] = Salary_of_Manager(e);
                    Manager["Department"] = e.Manager.Department;
                    Manager["Status"] = e.Manager.Status;
                    Manager["Percent"] = e.Manager.Percent;
                } 
                JArray Employees = new JArray();
                if (e.Employees != null) //находим всех работников
                {
                    foreach (var i in e.Employees)
                    {
                        if (i is Worker worker)
                        {

                            JObject employee = new JObject()
                            {
                                ["Name"] = i.Name,
                                ["Salary"] = i.Salary / worker.WorkHours,
                                ["Position"] = i.Position,
                                ["Department"] = e.Nameing,
                                ["Status"] = i.Status,
                                ["WorkHours"] = worker.WorkHours
                            };

                            Employees.Add(employee);
                        }
                        else
                        {
                            JObject employee = new JObject()
                            {
                                ["Name"] = i.Name,
                                ["Salary"] = i.Salary,
                                ["Position"] = i.Position,
                                ["Department"] = e.Nameing,
                                ["Status"] = i.Status
                            };

                            Employees.Add(employee);
                        }
                    }
                }
                dep["Manager"] = Manager;
                dep["Employees"] = Employees;
                ListDeps.Add(dep);
            }
            return ListDeps;
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
        /// Сериализация в JSON файл
        /// </summary>
        /// <param name="departments">Коренной список департаментов организации (1 уровень)</param>
        /// <param name="Path">Ссылка на файл</param>
        public void SerializeOrgJSON(Org Organization, string Path)
        {
            JObject org = new JObject
            {
                ["Organization"] = List_Deps(Organization.Organization), //добавляем список департаментов в организацию
                ["CEO"] = new JObject
                {
                    ["Name"] = Organization.CEO.Name,
                    ["Salary"] = CEO_Salary(Organization.Organization, Organization.CEO),
                    ["Position"] = "CEO",
                    ["Department"] = "Organization",
                    ["Status"] = "Manager",
                    ["Percent"] = Organization.CEO.Percent
                }
            };
            string JSON = org.ToString();
            File.WriteAllText(Path, JSON);
        }
        /// <summary>
        /// Получаем десериализованную организацию
        /// </summary>
        /// <param name="Path">Ссылка на файл</param>
        /// <returns>Коренной список департаментов (1 уровень вложенности)</returns>
        public Org DeserializeOrgJSON(string Path)
        {
            ObservableCollection<Department> List_departments = new ObservableCollection<Department>();
            string JSON = File.ReadAllText(Path);
            var data = JToken.Parse(JSON);
            Org org = new Org(data);
            return org;
        }
    }
}
