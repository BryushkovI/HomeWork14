using HomeWork15.Command;
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

        readonly string _path = @"Clients.json";

        readonly IParser parser;

        readonly Logger _logger;
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
        /// <summary>
        /// Список типов
        /// </summary>
        public enum ClientTypes
        {
            Regular,
            VIP,
            Entity
        }
        /// <summary>
        /// Базовый
        /// </summary>
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
        /// <summary>
        /// VIP
        /// </summary>
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
        /// <summary>
        /// Юр лицо
        /// </summary>
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
        /// <summary>
        /// Тип выбранного отдела
        /// </summary>
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
        /// <summary>
        /// Видимость кнопки "далее" для добавления следующих клиентов
        /// </summary>
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
        /// <summary>
        /// Список заголовков клиентов
        /// </summary>
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
            set => Set(ref _selectedTitleClient, value);
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => Set(ref _selectedClient, value );
        }
        /// <summary>
        /// Возвращает список клиентов выбранного отдела
        /// </summary>
        /// <returns></returns>
        private async Task<Client> OnOpenSelectedClientAsync()
        {
            return _client_type switch
            {
                ClientTypes.Regular => await parser.DeserializeClientLinqAsync<Regular>(_path, int.Parse(_selectedTitleClient.AccountNumber)).ConfigureAwait(false),
                ClientTypes.VIP => await parser.DeserializeClientLinqAsync<VIP>(_path, int.Parse(_selectedTitleClient.AccountNumber)).ConfigureAwait(false),
                ClientTypes.Entity => await parser.DeserializeClientLinqAsync<Entity>(_path, int.Parse(_selectedTitleClient.AccountNumber)).ConfigureAwait(false),
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

        #region Рабочая область
        ViewModel _workSpaceVM;
        public ViewModel WorkSpaceVM
        {
            get => _workSpaceVM;
            set => _workSpaceVM = value;
        }
        #endregion

        #region Комманды
        #region CloseAppCommand
        /// <summary>
        /// Закрыть программу
        /// </summary>
        public ICommand CloseAppCommand { get; }
        private void OnCloseAppCommandExecuted(object p)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private bool CanCloseAppCommandExecute(object p) => true;
        #endregion

        #region Save
        /// <summary>
        /// Сохранить выбранного клиента
        /// </summary>
        public IAsyncCommand Save { get; }
        /// <summary>
        /// Перезаписывает данные о выбранном клиенте
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        async Task OnSaveAsyncExecuted(object p)
        {

            EditClientViewModel editClientVM = (EditClientViewModel)_clientInfo;
            _selectedClient = editClientVM.SelectedClient;
            await parser.EditSerializeClientasync(_path, _selectedClient);
            OnLog("Данные клиента {0} были изменены {1}.", new[]
                    {
                        _selectedClient?.Name,
                        DateTime.Now.ToString(),
                    });
            _clientInfo = new ClientInfoViewModel(_selectedClient);
            OnPropertyChanged("SelectedClient");
            OnPropertyChanged("ClientInfo");
            await OnOpenDBExecuted(p);
            await OnSelectClientExecuted(p);
        }
        bool CanSaveAsyncExecute(object p) => _selectedClient != null && ( _clientInfo?.GetType() == typeof(EditClientViewModel)  
                                                                        || _clientInfo?.GetType() == typeof(ClientInfoViewModel) );

        private void ClientInfo_Saveing()
        {
            Task.Run(async () =>
            {
                AddClientViewModel addClientVM = (AddClientViewModel)_clientInfo;
                _selectedClient = addClientVM._client;
                _selectedTitleClient.AccountNumber = _selectedClient.AccountNumber.ToString();
                _selectedTitleClient.Name = _selectedClient.Name;
                await parser.CreateSerializeClientAsync(_path, _selectedClient);
                _clientInfo = new ClientInfoViewModel(_selectedClient);
                OnPropertyChanged("SelectedClient");
                OnPropertyChanged("ClientInfo");
                await OnOpenDBExecuted(null);
                await OnSelectClientExecuted(null);
            });
        }
        #endregion

        #region SelectClient
        /// <summary>
        /// Выбрать клиента
        /// </summary>
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
            _workSpaceVM = null;
            OnPropertyChanged("WorkSpaceVM");
            OnPropertyChanged("MenuItemPlots");
            _clientInfo.Saveing += ClientInfo_EditSaving;
        }

        private void ClientInfo_EditSaving()
        {
            Task.Run(async () =>
            {
                ClientInfoViewModel clientInfoVM = (ClientInfoViewModel)_clientInfo;
                _selectedClient = clientInfoVM.SelectedClient;
                if (double.TryParse(clientInfoVM.AddCreditBlockViewModel?.SumEnd, out _))
                {
                    _selectedClient.Credit = double.Parse(clientInfoVM.AddCreditBlockViewModel?.Sum);
                    _selectedClient.DateCreditBegin = DateTime.Today;
                    _selectedClient.DateCreditEnd = (DateTime)clientInfoVM.AddCreditBlockViewModel?.Date;
                    OnLog("Клиенту {0} выдан кредит на сумму {1:C2} с {2:d} до {3:d}.", new[]
                    {
                        _selectedClient?.Name,
                        _selectedClient?.Credit.ToString(),
                        _selectedClient?.DateCreditBegin.ToString(),
                        _selectedClient?.DateCreditEnd.ToString()
                    });
                }
                if (double.TryParse(clientInfoVM.AddDepositBlockViewModel?.SumEnd, out _))
                {
                    _selectedClient.Deposit = double.Parse(clientInfoVM.AddDepositBlockViewModel?.Sum);
                    _selectedClient.DateDepositBegin = DateTime.Today;
                    _selectedClient.DateDepositEnd = (DateTime)clientInfoVM.AddDepositBlockViewModel?.Date;
                    _selectedClient.Capitalization = clientInfoVM.AddDepositBlockViewModel.Capitalization;
                    OnLog("Клиенту {0} открыт вклад на сумму {1:C2} с {2:d} до {3:d}.", new[]
                    {
                        _selectedClient?.Name,
                        _selectedClient?.Deposit.ToString(),
                        _selectedClient?.DateDepositBegin.ToString(),
                        _selectedClient?.DateDepositEnd.ToString()
                    });
                }
                await parser.EditSerializeClientasync(_path, _selectedClient);
                _clientInfo = new ClientInfoViewModel(_selectedClient);
                OnPropertyChanged("SelectedClient");
                OnPropertyChanged("ClientInfo");
                await OnOpenDBExecuted(null);
                await OnSelectClientExecuted(null);
            });
        }

        private bool CanSelectClient(object p) => _selectedTitleClient.AccountNumber != null; 
        #endregion

        #region OpenDB
        /// <summary>
        /// Открыть список клиентов выбранного отдела
        /// </summary>
        public ICommand OpenDB { get; }
        private async Task OnOpenDBExecuted(object p)
        {
            ProgressBarVisibility = Visibility.Visible;
            Status = statusPairs[ReadyStatus.Busy];
            if (_clients?.Count % 10 != 0)
            {
                _skip = 0;
            }
            Clients = await parser.DeserializeClientsLinqAsync<TitleClient>(_path, (int)ClientType, _skip, 10);
            OnPropertyChanged("ListRowSpan");
            OnPropertyChanged("AddClientsButtonVisibility");
            _skip += 10;
            Status = statusPairs[ReadyStatus.Ready];
            ProgressBarVisibility = Visibility.Hidden;
        }

        private bool CanOpenDBExecute(object p) => true;
        #endregion

        #region AddClients
        /// <summary>
        /// Отобразить следующих 10 клиентов
        /// </summary>
        public IAsyncCommand AddClientsAsync { get; }
        async Task OnAddClientsAsyncExecuted(object p)
        {
            ProgressBarVisibility = Visibility.Visible;
            Status = statusPairs[ReadyStatus.Busy];
            ObservableCollection<TitleClient> newTitleClient = await parser.DeserializeClientsLinqAsync<TitleClient>(_path, (int)ClientType, _skip, 10);
            foreach (var item in newTitleClient)
            {
                Clients.Add(item);
            }
            _skip += 10;
            OnPropertyChanged("ListRowSpan");
            OnPropertyChanged("AddClientsButtonVisibility");
            Status = statusPairs[ReadyStatus.Ready];
            ProgressBarVisibility = Visibility.Hidden;
        }
        bool CanAddClientsAsyncExecute(object p) => _clients?.Count % 10 == 0;
        #endregion

        #region CreateNewClient
        /// <summary>
        /// Открыть интерфес создания нового клиента
        /// </summary>
        public ICommand AddClient { get; }
        void OnCreateNewClientExecuted(object p)
        {
            _clientInfo = new AddClientViewModel();
            OnPropertyChanged("ClientInfo");
            _clientInfo.Saveing += ClientInfo_Saveing;
        }
        bool CanCreateNewClientExecute(object p) => true;
        #endregion

        #region DeleteSelectedClient
        /// <summary>
        /// Удалить выранного клиента
        /// </summary>
        public IAsyncCommand DeleteClient { get; }
        async Task OnDeleteSelectedClientAsyncExecuted(object p)
        {
            await parser.DeleteSerializeClientAsync(_path, _selectedClient);
            OnLog("Учетная запись клиента {0} была удалена {1}", new[]
            {
                _selectedClient.Name,
                DateTime.Now.ToString()
            });
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
        /// <summary>
        /// Редактировать выбранного клиента
        /// </summary>
        public ICommand EditClient { get; }
        void OnEditSelectedClientExecute(object p)
        {
            _clientInfo = new EditClientViewModel(_selectedClient);
            
            OnPropertyChanged("ClientInfo");
            //_clientInfo.Saveing += ClientInfo_Saveing;
        }
        bool CanEditSelectedClientExecute(object p) => _selectedClient != null;
        #endregion

        #region BuildCreditPlot
        public IAsyncCommand BuildCreditPlot { get; }

        async Task OnBuildCreditPlotExecuted(object p)
        {
            _workSpaceVM = new WorkSpaceViewModel(_selectedClient, true);
            OnPropertyChanged(nameof(WorkSpaceVM));
        }

        bool CanBuildCreditPlotExecute(object p) => _selectedClient?.Credit != 0;
        #endregion

        #region BuildDepositPlot
        public IAsyncCommand BuildDepositPlot { get; }

        async Task OnBuildDepositPlotExecuted(object p)
        {
            _workSpaceVM = new WorkSpaceViewModel(_selectedClient, false);
            OnPropertyChanged(nameof(WorkSpaceVM));
        }

        bool CanBuildDepositPlotExecute(object p) => _selectedClient?.Deposit != 0;
        #endregion

        #region CreateTransfer
        public ICommand CreateTransfer { get; }

        async Task OnCreateTransferExecuted(object p)
        {
            ObservableCollection<TitleClient> titleClients = await parser.DeserializeAllClientsAsync<TitleClient>(_path);
            titleClients.Remove(titleClients.Single(e => e.AccountNumber == _selectedClient.AccountNumber.ToString()));
            _workSpaceVM = new TransferViewModel(_selectedClient, titleClients);
            OnPropertyChanged(nameof(WorkSpaceVM));
            _workSpaceVM.Saveing += WorkSpaceVM_Saveing;
        }

        bool CanCreateTransferExecuted(object p) => _selectedClient != null;

        private void WorkSpaceVM_Saveing()
        {
            Task.Run(async () =>
            {
                TransferViewModel transferVM = (TransferViewModel)_workSpaceVM;
                await parser.EditSerializeClientasync(_path, transferVM._client);
                await parser.EditSerializeClientasync(_path, transferVM._clientRecipient);
                _workSpaceVM = null;
                _clientInfo = new ClientInfoViewModel(_selectedClient);
                OnPropertyChanged("SelectedClient");
                OnPropertyChanged("ClientInfo");
                OnPropertyChanged("WorkSpaceVM");
                await OnOpenDBExecuted(null);
                await OnSelectClientExecuted(null);
            });
        }
        #endregion

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
            BuildCreditPlot = new LambdaCommandAsync(OnBuildCreditPlotExecuted, CanBuildCreditPlotExecute);
            BuildDepositPlot = new LambdaCommandAsync(OnBuildDepositPlotExecuted, CanBuildDepositPlotExecute);
            CreateTransfer = new LambdaCommandAsync(OnCreateTransferExecuted, CanCreateTransferExecuted);
            parser = new Parser();
            _logger = new Logger();
            Log += _logger.FileLog;
        }
    }
}