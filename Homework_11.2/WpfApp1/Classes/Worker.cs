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
        public override uint Salary => HourSalary*WorkHours;
        /// <summary>
        /// Количество отработанных часов
        /// </summary>
        public uint WorkHours { get; }

        private uint HourSalary { get; }

        public Worker(JObject worker)
            :base(worker)
        {
            WorkHours = uint.Parse(worker["WorkHours"].ToString());
            HourSalary = uint.Parse(worker["Salary"].ToString());
        }
    }
}
