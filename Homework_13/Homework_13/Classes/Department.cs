using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Homework_13
{
    public class Department<T>
        where T : Client
    {
        public ObservableCollection<T> ClientList { get; set; }
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Department()
        {
            ClientList = new ObservableCollection<T>();
        }
        /// <summary>
        /// Добавление в список
        /// </summary>
        /// <param name="Item"></param>
        public void Add(T Item)
        {
            ClientList.Add(Item);
        }
    }
}
