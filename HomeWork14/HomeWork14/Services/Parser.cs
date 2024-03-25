using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace HomeWork15.Services
{
    class Parser : IParser
    {
        public ObservableCollection<T> DeserializeClients<T>(string Path)
        {
            
            var ClientsList = JsonSerializer.Deserialize<List<T>>(File.ReadAllText(Path));
            ObservableCollection<T> ClientsCollection = new(ClientsList);
            return ClientsCollection;
        }

        public async Task<ObservableCollection<T>> DeserializeClientsAsync<T>(string Path)
        {
            FileStream fs = new(Path, FileMode.OpenOrCreate, FileAccess.Read);
            var ClientsList = await JsonSerializer.DeserializeAsync<List<T>>(fs).ConfigureAwait(false);
            ObservableCollection<T> ClientsCollection = new(ClientsList);
            return ClientsCollection;
        }

    }

    internal interface IParser
    {
        public ObservableCollection<T> DeserializeClients<T>(string Path);

        public Task<ObservableCollection<T>> DeserializeClientsAsync<T>(string Path);
    }
}