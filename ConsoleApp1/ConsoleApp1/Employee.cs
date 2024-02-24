using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public abstract class Employee
    {
        uint Salary { get; set; }
        string Name { get; set; }
        public Employee(string name,uint salary)
        {
            this.Salary = salary;
            this.Name = name;
        }
        
    }
}
