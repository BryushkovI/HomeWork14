using HomeWork15.Command;
using HomeWork15.Command.Base;
using HomeWork15.DataProvider;
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
        Logger _logger;
        public Client _client;
        public Client Client
        {
            get => _client;
            set => Set(ref _client, value);
        }
        public Client _clientRecipient;
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
            IDataProvider dataProvider = new DataProvider.DataProvider();

            try
            {
                _clientRecipient = await dataProvider.GetClientAsync(int.Parse(_titleClient.AccountNumber));

                _clientRecipient.BankAccount += _transferSum;
                _client.BankAccount -= _transferSum;
                OnSaving();
                OnLog("{0} перевел(а) {1} {2} {3}", new[]
                {
                _client.Name,
                _clientRecipient.Name,
                _transferSum.ToString(),
                DateTime.Now.ToString()
                });
            }
            catch (Exception)
            {

            }
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
            _logger = new();
            Log += _logger.FileLog;
        }
    }
}
