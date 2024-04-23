﻿using HomeWork15.Command;
using HomeWork15.Command.Base;
using HomeWork15.DataProvider;
using HomeWork15.Models;
using HomeWork15.Services;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace HomeWork15.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        #region Заголовок
        private string _Title = "Транжирбанк";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Статус
        enum ReadyStatus
        {
            Ready,
            Busy
        }
        static readonly Dictionary<ReadyStatus, string> statusPairs = new()
        {
            { ReadyStatus.Ready, "Готово"},
            { ReadyStatus.Busy, "Занят" }
        };
        private string _Status = statusPairs[ReadyStatus.Ready];
        /// <summary>
        /// Статус
        /// </summary>
        public string Status
        {
            get => _Status;
            set
            {
                if (value == _Status) return;
                _Status = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Прогресс
        private int _ProgressBar = 100;
        /// <summary>
        /// Прогресс
        /// </summary>
        public int ProgressBar
        {
            get => _ProgressBar;
            set => _ProgressBar = value;
        }

        private Visibility _ProgressBarVisibility = Visibility.Hidden;
        /// <summary>
        /// Видимость прогресса
        /// </summary>
        public Visibility ProgressBarVisibility
        {
            get => _ProgressBarVisibility;
            set
            {
                if (value == _ProgressBarVisibility) return;
                _ProgressBarVisibility = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Карточки с инфо о клиенте

        ViewModel _clientInfo;
        public ViewModel ClientInfo
        {
            get => _clientInfo;
            set => _clientInfo = value;
        }

        #endregion

        #region Выбранный тип клиентов
        public enum ClientTypes
        {
            Regular,
            VIP,
            Entity
        }
        public bool IsRegular
        {
            get { return ClientType == ClientTypes.Regular; }
            set
            {
                if (value)
                {
                    ClientType = ClientTypes.Regular;
                    _skip = 0;
                    //OnOpenDBExecuted(null).ConfigureAwait(false);
                }
            }
        }
        public bool IsVIP
        {
            get { return ClientType == ClientTypes.VIP; }
            set
            {
                if (value)
                {
                    ClientType = ClientTypes.VIP;
                    _skip = 0;
                    //OnOpenDBExecuted(null).ConfigureAwait(false);
                }
            }
        }
        public bool IsEntity
        {
            get { return ClientType == ClientTypes.Entity; }
            set
            {
                if (value)
                {
                    ClientType = ClientTypes.Entity;
                    _skip = 0;
                    //OnOpenDBExecuted(null).ConfigureAwait(false);
                }
            }
        }

        private ClientTypes _client_type;
        public ClientTypes ClientType
        {
            get => _client_type;
            set
            {
                _client_type = value;
                _clients?.Clear();
                
            } 
        }
        #endregion

        #region Поток
        private Thread _thread;
        private Task<ObservableCollection<TitleClient>> _task; 
        #endregion

        #region Поиск клиента
        private string _clientSearch;
        public string ClientSearch
        {
            get => _clientSearch;
            set => _clientSearch = value;
        }
        #endregion

        #region Список клиентов

        public int ListRowSpan
        {
            get
            {
                if (_clients != null)
                {
                    return _clients.Count % 10 == 0 ? 1 : 2;
                }
                return 2;
            }
        }

        public Visibility AddClientsButtonVisibility
        {
            get
            {
                if (_clients != null)
                {
                    return _clients.Count % 10 == 0 ? Visibility.Visible : Visibility.Collapsed;
                }
                return Visibility.Collapsed;
            }
        }

        private int _skip = 0;
        private ObservableCollection<TitleClient> _clients;
        public ObservableCollection<TitleClient> Clients
        {
            get
            {
                return _clients;
            }
            set
            {
                if (value == _clients) return;
                _clients = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Выбранный клиент
        private TitleClient _selectedTitleClient;
        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public TitleClient SelectedTitleClient
        {
            get => _selectedTitleClient;
            set
            {

                _selectedTitleClient = value;
                
                
            }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => Set(ref _selectedClient, value );
        }
        private async Task<Client> OnOpenSelectedClientAsync()
        {
            IParser parser = new Parser();
            return _client_type switch
            {
                ClientTypes.Regular => await parser.DeserializeClientLinqAsync<Regular>(@"Clients.json", int.Parse(_selectedTitleClient.AccountNumber)).ConfigureAwait(false),
                ClientTypes.VIP => await parser.DeserializeClientLinqAsync<VIP>(@"Clients.json", int.Parse(_selectedTitleClient.AccountNumber)).ConfigureAwait(false),
                ClientTypes.Entity => await parser.DeserializeClientLinqAsync<Entity>(@"Clients.json", int.Parse(_selectedTitleClient.AccountNumber)).ConfigureAwait(false),
                _ => null,
            };
        }

        #endregion

        #region Видимость "График"
        public Visibility MenuItemPlots
        {
            get => _selectedClient != null ? Visibility.Visible : Visibility.Collapsed;
        } 
        #endregion

        #region Комманды
        #region CloseAppCommand
        public ICommand CloseAppCommand { get; }
        private void OnCloseAppCommandExecuted(object p)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private bool CanCloseAppCommandExecute(object p) => true;
        #endregion

        #region Save
        public IAsyncCommand Save { get; }

        async Task OnSaveAsyncExecuted(object p)
        {
            EditClientViewModel editClientVM = (EditClientViewModel)_clientInfo;
            _selectedClient = editClientVM.SelectedClient;
            IParser parser = new Parser();
            await parser.EditSerializeClientasync(@"Clients.json", _selectedClient);
            _clientInfo = new ClientInfoViewModel(_selectedClient);
            OnPropertyChanged("SelectedClient");
            OnPropertyChanged("ClientInfo");
            await OnOpenDBExecuted(p);
            await OnSelectClientExecuted(p);
        }
        bool CanSaveAsyncExecute(object p) => _selectedClient != null && _clientInfo?.GetType() == typeof(EditClientViewModel);  
        #endregion

        #region SelectClient
        public ICommand SelectClient { get; }
        private async Task OnSelectClientExecuted(object p)
        {
            if (_selectedClient != null)
            {
                if (_selectedTitleClient.AccountNumber != _selectedClient.AccountNumber.ToString())
                {
                    _selectedClient = await OnOpenSelectedClientAsync();
                }
            }
            else
            {
                _selectedClient = await OnOpenSelectedClientAsync();
            }
            _clientInfo = new ClientInfoViewModel(_selectedClient);
            OnPropertyChanged("SelectedClient");
            OnPropertyChanged("ClientInfo");
            OnPropertyChanged("MenuItemPlots");
        }
        private bool CanSelectClient(object p) => _selectedTitleClient.AccountNumber != null; 
        #endregion

        #region OpenDB
        public ICommand OpenDB { get; }
        private async Task OnOpenDBExecuted(object p)
        {
            ProgressBarVisibility = Visibility.Visible;
            Status = statusPairs[ReadyStatus.Busy];
            IParser parser = new Parser();
            if (_clients?.Count % 10 != 0)
            {
                _skip = 0;
            }
            Clients = await parser.DeserializeClientsLinqAsync<TitleClient>(@"Clients.json", (int)ClientType, _skip, 10);
            OnPropertyChanged("ListRowSpan");
            OnPropertyChanged("AddClientsButtonVisibility");
            _skip += 10;
            Status = statusPairs[ReadyStatus.Ready];
            ProgressBarVisibility = Visibility.Hidden;
        }

        private bool CanOpenDBExecute(object p) => true;
        #endregion

        #region AddClients
        public IAsyncCommand AddClientsAsync { get; }
        async Task OnAddClientsAsyncExecuted(object p)
        {
            ProgressBarVisibility = Visibility.Visible;
            Status = statusPairs[ReadyStatus.Busy];
            IParser parser = new Parser();
            ObservableCollection<TitleClient> newTitleClient = await parser.DeserializeClientsLinqAsync<TitleClient>(@"Clients.json", (int)ClientType, _skip, 10);
            foreach (var item in newTitleClient)
            {
                Clients.Add(item);
            }

            //if (Clients.Count % 10 == 0)
            //{
            //    Clients.Add(new TitleClient("", "далее"));
            //}
            _skip += 10;
            OnPropertyChanged("ListRowSpan");
            OnPropertyChanged("AddClientsButtonVisibility");
            Status = statusPairs[ReadyStatus.Ready];
            ProgressBarVisibility = Visibility.Hidden;
        }
        bool CanAddClientsAsyncExecute(object p) => _clients?.Count % 10 == 0;
        #endregion

        #region CreateNewClient
        public ICommand AddClient { get; }
        void OnCreateNewClientExecuted(object p)
        {
            _clientInfo = new AddClientViewModel();
            OnPropertyChanged("ClientInfo");
        }
        bool CanCreateNewClientExecute(object p) => true;
        #endregion

        #region DeleteSelectedClient
        public IAsyncCommand DeleteClient { get; }
        async Task OnDeleteSelectedClientAsyncExecuted(object p)
        {
            IParser parser = new Parser();
            await parser.DeleteSerializeClientAsync(@"Clients.json", _selectedClient);
            
            _clientInfo = null;
            _clients.Remove(_clients.Where(e => e.AccountNumber == _selectedClient.AccountNumber.ToString()).Single());
            _selectedClient = null;
            OnPropertyChanged("SelectedClient");
            OnPropertyChanged("ClientInfo");
            OnPropertyChanged("Clients");
        }
        bool CanDeleteSelectedClientAsyncExecute(object p) => _selectedClient != null;
        #endregion

        #region EditSelectedClient
        public ICommand EditClient { get; }
        void OnEditSelectedClientExecute(object p)
        {
            _clientInfo = new EditClientViewModel(_selectedClient);
            
            OnPropertyChanged("ClientInfo");
        }
        bool CanEditSelectedClientExecute(object p) => _selectedClient != null;
        #endregion

        #region CreateCredit
        public IAsyncCommand CreateCreditAsync { get; }

        async Task OnCreateCreditAsyncExecuted(object p)
        {

        }

        bool CanCreateCreditAsyncExecute(object p) => _selectedClient != null && _selectedClient.Credit == 0;
        #endregion

        #region CreateDeposit
        public IAsyncCommand CreateDepositAsync { get; }

        async Task OnCreateDepositAsyncExecuted(object p)
        {

        }
        bool CanCreateDepositAsyncExecute(object p) => _selectedClient != null && _selectedClient.Deposit == 0;
        #endregion

        #region BuildCreditPlot
        public IAsyncCommand BuildCreditPlot { get; }

        async Task OnBuildCreditPlotExecuted(object p)
        {

        }

        bool CanBuildCreditPlotExecute(object p) => _selectedClient?.Credit != 0;
        #endregion

        #region BuildDepositPlot
        public IAsyncCommand BuildDepositPlot { get; }

        async Task OnBuildDepositPlotExecuted(object p)
        {

        }

        bool CanBuildDepositPlotExecute(object p) => _selectedClient?.Deposit != 0;
        #endregion

        #endregion

        #region Рабочая область
        ViewModel _workSpaceVM;
        public ViewModel WorkSpaceVM
        {
            get => _workSpaceVM;
            set => _workSpaceVM = value;
        }
        #endregion

        public MainWindowViewModel()
        {
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
            Save = new LambdaCommandAsync(OnSaveAsyncExecuted, CanSaveAsyncExecute);
            OpenDB = new LambdaCommandAsync(OnOpenDBExecuted, CanOpenDBExecute);
            SelectClient = new LambdaCommandAsync(OnSelectClientExecuted, CanSelectClient);
            AddClientsAsync = new LambdaCommandAsync(OnAddClientsAsyncExecuted, CanAddClientsAsyncExecute);
            AddClient = new LambdaCommand(OnCreateNewClientExecuted, CanCreateNewClientExecute);
            EditClient = new LambdaCommand(OnEditSelectedClientExecute, CanEditSelectedClientExecute);
            DeleteClient = new LambdaCommandAsync(OnDeleteSelectedClientAsyncExecuted, CanDeleteSelectedClientAsyncExecute);
            CreateCreditAsync = new LambdaCommandAsync(OnCreateCreditAsyncExecuted, CanCreateCreditAsyncExecute);
            CreateDepositAsync = new LambdaCommandAsync(OnCreateDepositAsyncExecuted, CanCreateDepositAsyncExecute);
            BuildCreditPlot = new LambdaCommandAsync(OnBuildCreditPlotExecuted, CanBuildCreditPlotExecute);
            BuildDepositPlot = new LambdaCommandAsync(OnBuildDepositPlotExecuted, CanBuildDepositPlotExecute);
        }
    }
}