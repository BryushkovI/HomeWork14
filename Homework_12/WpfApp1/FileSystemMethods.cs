using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.IO;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public class FileSystemMethods
    {
        Methods methods = new Methods();
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
                    Manager["Salary"] = e.Manager.Salary;
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
                    ["Salary"] = methods.CEO_Salary(Organization.Organization, Organization.CEO),
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
