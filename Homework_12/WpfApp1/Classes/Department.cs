using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    
    public class Department
    {
        /// <summary>
        /// Наименование департамента
        /// </summary>
        public string Nameing { get; set; }
        /// <summary>
        /// Список сотрудников
        /// </summary>
        public ObservableCollection<Employee> Employees { get; set; }
        /// <summary>
        /// Руководитель отдела
        /// </summary>
        public Manager Manager { get; set; }
        /// <summary>
        /// ЗП руководителя
        /// </summary>
        public uint Total_Salary
        {
            get
            {
                uint Sum = 0;
                if (Employees != null)
                {
                    foreach (var e in Employees)
                    {
                        Sum += e.Salary;
                    }
                }
                return Sum;
            }
        }
        /// <summary>
        /// Список входящих департаментов
        /// </summary>
        public ObservableCollection<Department> Departments { get; set; }
        public Department(JObject dep)
        {
            Employees = new ObservableCollection<Employee>();
            Departments = new ObservableCollection<Department>();
            JObject e = (JObject)dep;
            Nameing = e["Nameing"].ToString();
            JArray employees = JArray.Parse(e["Employees"].ToString());
            JArray deps = JArray.Parse(e["Departments"].ToString());
            foreach(var i in employees)
            {
                string set_position = i["Status"].ToString();
                switch (set_position)
                {
                    case "Worker":
                        Employees.Add(new Worker((JObject)i));
                        break;
                    case "Intern":
                        Employees.Add(new Intern((JObject)i));
                        break;
                }
            }
            Manager = new Manager((JObject)e["Manager"],Total_Salary);
            foreach(var i in deps)
            {
                Departments.Add(new Department((JObject)i));
            }
        }

        public Department()
        {

        }

    }
}