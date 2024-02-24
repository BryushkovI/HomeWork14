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
    public class Org
    {
        /// <summary>
        /// Список департамнтов
        /// </summary>
        public ObservableCollection<Department> Organization { get; }
        
        public Manager CEO { get; }

        private uint Total_Salary
        {
            get
            {
                uint Sum = 0;
                foreach (var e in Organization)
                {
                    Sum += e.Manager.Salary;
                }
                return Sum;
            }
        }

        public Org (JToken org)
        {
            JObject e = (JObject)org;
            JArray departments = JArray.Parse(e["Organization"].ToString());
            Organization = new ObservableCollection<Department>();
            foreach(var i in departments)
            {
                Organization.Add(new Department((JObject)i));
            }
            CEO = new Manager((JObject)org["CEO"], Total_Salary);
        }
    }
}
