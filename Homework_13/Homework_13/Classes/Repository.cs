using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Homework_13
{
    class Repository<T>
        where T : ObservableCollection<Client>
    {
        public ObservableCollection<T> Departments { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Repository()
        {
            Departments = new ObservableCollection<T>();
        }
    }
}
