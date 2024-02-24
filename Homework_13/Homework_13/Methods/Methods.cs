using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Homework_13
{
    public class Methods
    {
        /// <summary>
        /// Объявление всех клиентов
        /// </summary>
        /// <param name="departments"></param>
        /// <returns></returns>
        public ObservableCollection<Client> AllClients(ObservableCollection<Department<Client>> departments)
        {
            ObservableCollection<Client> allClients = new ObservableCollection<Client>();

            foreach(var e in departments)
            {
                foreach(var i in e.ClientList)
                {
                    allClients.Add(i);
                }
            }

            return allClients;
        }
        /// <summary>
        /// Генерация случайного номера с учетом типа клиента
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public int GetNum(AddClient window)
        {
            int spec;
            if (window.ComboBox_Type.Text == "Юр. лицо") spec = 1;
            else if (window.ComboBox_Type.Text == "Физ. лицо") spec = 2;
            else spec = 3;
            Random random = new Random();
            return Convert.ToInt32(spec * 10000000 + random.Next(0,9999999));
        }
    }
}
