using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Collections.ObjectModel;

namespace Homework_13
{
    public class FileMethods
    {
        /// <summary>
        /// Десериализует список отделов с клиентами
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public ObservableCollection<Department<Client>> DeserializeClients(string Path)
        {
            ObservableCollection<Department<Client>> departments = new ObservableCollection<Department<Client>>();
            string JSON = File.ReadAllText(Path);
            JToken jToken = JToken.Parse(JSON);
            JArray jArray = JArray.Parse(jToken["Clients"].ToString()); //парсит по всех клинетов 
            foreach(var e in jArray)
            {
                departments.Add(DeserializeSelectedType(e)); //добавляем новый отдел
            }
            return departments;
        }
        /// <summary>
        /// Десериализует выбранный отдел
        /// </summary>
        /// <param name="Type">Токен отдела</param>
        /// <returns></returns>
        public Department<Client> DeserializeSelectedType(JToken Type)
        {
            Department<Client> clients = new Department<Client>();
            JArray array;
            if (Type.ToString().Contains("Entity")) //проверка на тип отдела
            {
                array = JArray.Parse(Type[$"Entity"].ToString());
                foreach(var e in array)
                {
                    clients.Add(new Entity((JObject)e)); //новый экземпляр юр лица
                }
            }
            else if (Type.ToString().Contains("Individual_regular"))
            {
                array = JArray.Parse(Type[$"Individual_regular"].ToString());
                foreach (var e in array)
                {
                    clients.Add(new Individual_regular((JObject)e)); //новый экземпляр обычного физлица
                }
            }
            else
            {
                array = JArray.Parse(Type[$"Individual_VIP"].ToString());
                foreach (var e in array)
                {
                    clients.Add(new Individual_VIP((JObject)e)); // новый экземпляр VIP физ лица
                }
            }
            
            return clients;
        }
        /// <summary>
        /// Создает сериализованный список клиентов отдела
        /// </summary>
        /// <param name="clients">Список клиентов в отделе</param>
        /// <returns></returns>
        public JArray Clients(Department<Client> clients)
        {
            JArray department = new JArray();
            foreach(var e in clients.ClientList)
            {
                JObject client = e.SerializeClient(e); // сериализуем каждого клиента в отделе
                department.Add(client); // добавляем в отдел
            }
            return department;
        }
        /// <summary>
        /// Сериализует всех клиентов
        /// </summary>
        /// <param name="departments">Отделы</param>
        /// <param name="Path"></param>
        public void SerializeClients(ObservableCollection<Department<Client>> departments,string Path)
        {
            JObject Entities = new JObject
            {
                ["Entity"] = Clients(departments[0])
            };
            JObject Individual_regular = new JObject
            {
                ["Individual_regular"] = Clients(departments[1])
            };
            JObject Individual_VIP = new JObject
            {
                ["Individual_VIP"] = Clients(departments[2])
            };
            JObject jObject = new JObject
            {
                ["Clients"] = new JArray
                {
                    Entities,
                    Individual_regular,
                    Individual_VIP
                }
            };
            string JSON = jObject.ToString();
            File.WriteAllText(Path, JSON);
        }
    }
}
