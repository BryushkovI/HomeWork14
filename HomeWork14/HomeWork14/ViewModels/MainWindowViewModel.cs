using HomeWork15.Command;
using HomeWork15.DataProvider;
using HomeWork15.Models;
using HomeWork15.Services;
using Microsoft.Win32;
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
            //set { ClientType = value ? ClientTypes.Regular : ClientType; }
            set
            {
                if (value)
                {
                    ClientType = ClientTypes.Regular;
                    OnOpenDBExecuted(null);
                }
            }
        }
        public bool IsVIP
        {
            get { return ClientType == ClientTypes.VIP; }
            //set { ClientType = value ? ClientTypes.VIP : ClientType; }
            set
            {
                if (value)
                {
                    ClientType = ClientTypes.VIP;
                    OnOpenDBExecuted(null);
                }
            }
        }
        public bool IsEntity
        {
            get { return ClientType == ClientTypes.Entity; }
            //set { ClientType = value ? ClientTypes.Entity : ClientType; }
            set
            {
                if (value)
                {
                    ClientType = ClientTypes.Entity;
                    OnOpenDBExecuted(null);
                }
            }
        }

        private ClientTypes _client_type;
        public ClientTypes ClientType
        {
            get => _client_type;
            set { _client_type = value; } 
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

        #region Комманды
        #region CloseAppCommand
        public ICommand CloseAppCommand { get; }
        private void OnCloseAppCommandExecuted(object p)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private bool CanCloseAppCommandExecute(object p) => true;
        #endregion
        public ICommand GetData { get; }


        #region OpenDB
        public ICommand OpenDB { get; }
        private async void OnOpenDBExecuted(object p)
        {
            ProgressBarVisibility = Visibility.Visible;
            Status = statusPairs[ReadyStatus.Busy];
            IParser parser = new Parser();

            //Clients = await parser.DeserializeClientsAsync<TitleClient>(@"Clients.json");

            Clients = await parser.DeserializeClientsLinqAsync<TitleClient>(@"Clients.json",(int)ClientType);

            Status = statusPairs[ReadyStatus.Ready];
            ProgressBarVisibility = Visibility.Hidden;
        }

        private bool CanOpenDBExecute(object p) => true; 
        #endregion


        #endregion
        public MainWindowViewModel()
        {
                      
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
            OpenDB = new LambdaCommand(OnOpenDBExecuted, CanOpenDBExecute);
        }
    }
}
