using HomeWork15.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HomeWork15.Services
{
    class Parser : IParser
    {
        public async Task<ObservableCollection<T>> DeserializeAllClientsAsync<T>(string path)
        {
            ObservableCollection<T> clientsCollection = new();
            FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.Read);
            var parsed = await JArray.LoadAsync(new JsonTextReader(new StreamReader(fs)));
            fs.Close();
            var clientsJToken = parsed.ToList();

            foreach (JToken item in clientsJToken)
            {
                clientsCollection.Add(item.ToObject<T>());
            }
            return clientsCollection;
        }

        public async Task<ObservableCollection<T>> DeserializeClientsLinqAsync<T>(string Path, int AccountType, int Skip = 0, int Take = int.MaxValue)
        {
            FileStream fs = new(Path, FileMode.OpenOrCreate, FileAccess.Read);

            var parsed = await JArray.LoadAsync(new JsonTextReader(new StreamReader(fs)));
            fs.Close();
            var clientsJToken = parsed.SelectTokens($"[ ?( @.AccountType == {AccountType} ) ]").Skip(Skip).Take(Take).ToList();
            ObservableCollection<T> ClientsCollection = new();
            foreach (JToken clientToken in clientsJToken)
            {
                ClientsCollection.Add(clientToken.ToObject<T>());
            }
            return ClientsCollection;
        }
        public async Task<T> DeserializeClientLinqAsync<T>(string Path, int AccountNumber)
        {
            FileStream fs = new(Path, FileMode.OpenOrCreate, FileAccess.Read);
            var parsed = await JArray.LoadAsync(new JsonTextReader(new StreamReader(fs))).ConfigureAwait(false);
            fs.Close();
            var item = parsed.SelectToken($"[ ?( @.AccountNumber == {AccountNumber} ) ]").ToObject<T>();
            return item;
        }
        public async Task CreateSerializeClientAsync(string path, Client client)
        {
            ObservableCollection<Client> clients = await DeserializeAllClientsAsync<Client>(path);
            FileStream fs = new(path, FileMode.Create);
            clients.Add(client);
            await System.Text.Json.JsonSerializer.SerializeAsync(fs, clients);
            fs.Close();

        }
        public async Task EditSerializeClientasync(string path, Client client)
        {
            ObservableCollection<Client> clients = await DeserializeAllClientsAsync<Client>(path);
            //clients.ToList().RemoveAll(e => e.AccountNumber == client.AccountNumber);
            clients.Remove(clients.Where(e => e.AccountNumber == client.AccountNumber).Single());
            FileStream fs = new(path, FileMode.Create);
            clients.Add(client);
            await System.Text.Json.JsonSerializer.SerializeAsync(fs, clients);
            fs.Close();
        }
    }

    internal interface IParser
    {
        public Task<ObservableCollection<T>> DeserializeAllClientsAsync<T>(string path);

        public Task<ObservableCollection<T>> DeserializeClientsLinqAsync<T>(string Path, int AccountType, int Skip, int Take);

        public Task<T> DeserializeClientLinqAsync<T>(string Path, int AccountNumber);

        public Task CreateSerializeClientAsync(string path, Client client);

        public Task EditSerializeClientasync(string path, Client client);
    }
}