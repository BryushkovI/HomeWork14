using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Intern : Employee
    {
        public override uint Salary { get; set; }

        public Intern(string status)
            :base(status)
        {

        }

        public Intern(JObject intern)
            :base(intern)
        {
            Salary = uint.Parse(intern["Salary"].ToString());
        }
    }
}
