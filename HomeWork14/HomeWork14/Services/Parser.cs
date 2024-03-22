using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HomeWork15.Services
{
    class Parser : IParser
    {
        public ObservableCollection<T> DeserializeClients<T>(string Path)
        {
            
            var ClientsList = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(Path));
            ObservableCollection<T> ClientsCollection = new ObservableCollection<T>(ClientsList);
            return ClientsCollection;
        }

    }

    internal interface IParser
    {
        public ObservableCollection<T> DeserializeClients<T>(string Path);
    }
}
