﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HomeWork15.Services
{
    class Parser : IParser
    {
        public ObservableCollection<T> DeserializeClients<T>(string Path)
        {
            
            var ClientsList = System.Text.Json.JsonSerializer.Deserialize<List<T>>(File.ReadAllText(Path));
            ObservableCollection<T> ClientsCollection = new(ClientsList);
            return ClientsCollection;
        }

        public async Task<ObservableCollection<T>> DeserializeClientsAsync<T>(string Path)
        {
            FileStream fs = new(Path, FileMode.OpenOrCreate, FileAccess.Read);
            var ClientsList = await System.Text.Json.JsonSerializer.DeserializeAsync<List<T>>(fs).ConfigureAwait(false);
            ObservableCollection<T> ClientsCollection = new(ClientsList);
            return ClientsCollection;
        }

        public async Task<ObservableCollection<T>> DeserializeClientsLinqAsync<T>(string Path, int AccountType, int Skip, int Take)
        {
            FileStream fs = new(Path, FileMode.OpenOrCreate, FileAccess.Read);

            var parsed = await JArray.LoadAsync(new JsonTextReader(new StreamReader(fs)));
            fs.Close();
            var clientsJToken = parsed.SelectTokens($"[ ?( @.AccountType == {AccountType} ) ]").Skip(Skip).Take(Take).ToList();
            ObservableCollection<T> ClientsCollection = new ObservableCollection<T>();
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
    }

    internal interface IParser
    {
        public ObservableCollection<T> DeserializeClients<T>(string Path);

        public Task<ObservableCollection<T>> DeserializeClientsAsync<T>(string Path);

        public Task<ObservableCollection<T>> DeserializeClientsLinqAsync<T>(string Path, int AccountType, int Skip, int Take);

        public Task<T> DeserializeClientLinqAsync<T>(string Path, int AccountNumber);
    }
}