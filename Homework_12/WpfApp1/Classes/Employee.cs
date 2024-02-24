using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace WpfApp1
{
    public enum SortCriterion
    {
        Name,
        Salary,
        Status,
        Position
    }
    abstract public class Employee
    {
        /// <summary>
        /// Имя сотрудника
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Должность
        /// </summary>
        public string Position { get; set; }
        /// <summary>
        /// Кем является сотрудник
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// ЗП сотрудника
        /// </summary>
        public abstract uint Salary { get; set; }
        /// <summary>
        /// Департамент сотрудника
        /// </summary>
        public string Department { get; set; }
        public Employee(JObject employee)
        {
            Name = employee["Name"].ToString();
            Position = employee["Position"].ToString();
            Status = employee["Status"].ToString();
            Department = employee["Department"].ToString();
        }
        public Employee(string status)
        {
            Status = status;
            Name = "Новый Сотрудник";
            Salary = 0;
            Position = "Новая должность";
        }
        private class SortByName : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = x;
                Employee Y = y;

                return String.Compare(X.Name, Y.Name, StringComparison.Ordinal);
            }
        }
        private class SortBySalary : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = x;
                Employee Y = y;

                if (X.Salary > Y.Salary) return -1;
                else if (X.Salary < Y.Salary) return 1;
                else return 0;
            }
        }

        private class SortByPosition : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                Employee X = x;
                Employee Y = y;
                return String.Compare(X.Position, Y.Position, StringComparison.Ordinal);
            }
        }

        public static IComparer<Employee> SortedBy(SortCriterion criterion)
        {
            if (criterion == SortCriterion.Name) return new SortByName();
            else if (criterion == SortCriterion.Salary) return new SortBySalary();
            else if (criterion == SortCriterion.Position) return new SortByPosition();
            else return null;
        }
    }
}
