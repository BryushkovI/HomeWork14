using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Worker : Employee
    {
        /// <summary>
        /// ЗП (фиксированная)
        /// </summary>
        public override uint Salary { get => HourSalary * WorkHours; set { } }
        /// <summary>
        /// Количество отработанных часов
        /// </summary>
        public uint WorkHours { get; set; }

        public uint HourSalary { get; set; }

        public Worker(JObject worker)
            :base(worker)
        {
            WorkHours = uint.Parse(worker["WorkHours"].ToString());
            HourSalary = uint.Parse(worker["Salary"].ToString());
        }

        public Worker(string status)
            : base(status)
        {
            WorkHours = 0;
        }
    }
}
