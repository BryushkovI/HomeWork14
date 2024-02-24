using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace WpfApp1
{
    public class Manager:Employee
    {
        /// <summary>
        /// ЗП (нефиксированная)
        /// </summary>
        public override uint Salary { get; set; }

        /// <summary>
        /// Процент от ЗП сотрудников
        /// </summary>
        public uint Percent { get; set; }

        public Manager(JObject manager, uint depsalary)
            :base(manager)
        {
            Percent = uint.Parse(manager["Percent"].ToString());

            Salary = depsalary * Percent / 100;
            if (Salary < 1300) Salary = 1300;
        }
        public Manager(string status)
            :base(status)
        {

        }
    }
}