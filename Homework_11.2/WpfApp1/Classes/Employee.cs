using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace WpfApp1
{
    abstract public class Employee
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get;}
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get;  }
        /// <summary>
        /// Кем является сотрудник
        /// </summary>
        public string Status { get; }
        /// <summary>
        /// ЗП сотрудника
        /// </summary>
        public abstract uint Salary { get;}
        /// <summary>
        /// Департамент сотрудника
        /// </summary>
        public string Department { get; }
        public Employee(JObject employee)
        {
            Name = employee["Name"].ToString();
            Position = employee["Position"].ToString();
            Status = employee["Status"].ToString();
            Department = employee["Department"].ToString();
        }
    }
}
