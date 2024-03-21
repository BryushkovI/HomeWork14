using HomeWork15.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeWork15.Services
{
    class Parser : IParser
    {
        public ObservableCollection<T> DeserializeClients<T>(string StringData)
        {
            ObservableCollection<T> departments = new();
            JToken jToken = JToken.Parse(StringData);
            JArray jArray = JArray.Parse(jToken["Clients"].ToString()); //парсит все отделы 


            return departments;
        }

    }

    internal interface IParser
    {
        public ObservableCollection<T> DeserializeClients<T>(string StringData);
    }
}
