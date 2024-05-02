using HomeWork15.Command;
using HomeWork15.Command.Base;
using HomeWork15.Models;
using HomeWork15.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HomeWork15.ViewModels.MainWindowViewModel;

namespace HomeWork15.ViewModels
{
    class TransferViewModel : ViewModel
    {
        Client _client;
        public Client Client
        {
            get => _client;
            set => Set(ref _client, value);
        }

        private int _transferSum;
        public int TransferSum
        {
            get => _transferSum;
            set => Set(ref _transferSum, value);
        }

        private ObservableCollection<TitleClient> _clients;
        /// <summary>
        /// Список заголовков клиентов
        /// </summary>
        public ObservableCollection<TitleClient> Clients
        {
            get => _clients;
        }

        private TitleClient _titleClient;

        public TitleClient TitleClient
        {
            get => _titleClient;
            set => Set(ref _titleClient, value);
        }

        #region Transfer
        public IAsyncCommand TransferAsync { get; }

        async Task OnTransferAsyncExecute(object p)
        {
            IParser parser = new Parser();
            Client clientRecipient;
            if (parser.DeserializeClientLinqAsync<Client>(@"Clients.json", int.Parse(_titleClient.AccountNumber)).Result.AccountType == 0)
            {
                clientRecipient = await parser.DeserializeClientLinqAsync<Regular>(@"Clients.json", int.Parse(_titleClient.AccountNumber));
            }
            else if(parser.DeserializeClientLinqAsync<Client>(@"Clients.json", int.Parse(_titleClient.AccountNumber)).Result.AccountType == 1)
            {
                clientRecipient = await parser.DeserializeClientLinqAsync<VIP>(@"Clients.json", int.Parse(_titleClient.AccountNumber));
            }
            else
            {
                clientRecipient = await parser.DeserializeClientLinqAsync<Entity>(@"Clients.json", int.Parse(_titleClient.AccountNumber));
            }
            clientRecipient.BankAccount += _transferSum;
            _client.BankAccount -= _transferSum;
            await parser.EditSerializeClientasync(@"Clients.json", clientRecipient);
            await parser.EditSerializeClientasync(@"Clients.json", _client);

        }

        bool CanTransferAsyncExecuded(object p)
        {
            if (_client.BankAccount >= _transferSum && _transferSum != 0 && _titleClient.AccountNumber != null)
            {
                return true;
            }
            return false;
        } 
        #endregion

        public TransferViewModel(Client client, ObservableCollection<TitleClient> titleClients)
        {
            Client = client;
            _clients = titleClients;
            TransferAsync = new LambdaCommandAsync(OnTransferAsyncExecute, CanTransferAsyncExecuded);
        }
    }
}
