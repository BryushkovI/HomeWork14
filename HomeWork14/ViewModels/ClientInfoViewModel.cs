using HomeWork15.Command;
using HomeWork15.Command.Base;
using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeWork15.ViewModels
{
    class ClientInfoViewModel : ViewModel
    {
        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => Set(ref _selectedClient, value);
        }

        private AddDepositBlockViewModel _addDepositBlockViewModel;
        /// <summary>
        /// Интерфейс для открытия вклада
        /// </summary>
        public AddDepositBlockViewModel AddDepositBlockViewModel
        {
            get => _addDepositBlockViewModel;
            set => Set(ref _addDepositBlockViewModel, value);
        }

        private AddCreditBlockViewModel _addCreditBlockViewModel;
        /// <summary>
        /// Интерфейс для открытия кредита
        /// </summary>
        public AddCreditBlockViewModel AddCreditBlockViewModel
        {
            get => _addCreditBlockViewModel;
            set => Set(ref _addCreditBlockViewModel, value);
        }

        #region Карточка инфо
        public Dictionary<Type, string> ClientTypeDictionary = new()
        {
            { typeof(Regular), "Базовый" },
            { typeof(VIP), "VIP" },
            { typeof(Entity), "Для Бизнеса" }
        };
        /// <summary>
        /// Список типов клиентов
        /// </summary>
        public string ClientTypeDescription
        {
            get
            {
                return _selectedClient == null ? "" : ClientTypeDictionary[_selectedClient.GetType()];
            }
        }
        #endregion

        #region Карточка Вклад
        /// <summary>
        /// Видимость карточки Вклад
        /// </summary>
        public Visibility GridDepositVisibility
        {
            get
            {
                if (_selectedClient != null)
                {
                    return _selectedClient.Deposit == 0 ? Visibility.Collapsed : Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        Visibility _addDepositVisibility;
        /// <summary>
        /// Видимость кнопки добавдления вклада
        /// </summary>
        public Visibility AddDepositVisibility
        {
            get => _addDepositVisibility;
            set => Set(ref _addDepositVisibility, value);
        }
        #endregion

        #region Карточка Кредит
        /// <summary>
        /// Видимость карточки Кредит
        /// </summary>
        public Visibility GridCreditVisibility
        {
            get
            {
                if (_selectedClient != null)
                {
                    return _selectedClient.Credit == 0 ? Visibility.Collapsed : Visibility.Visible;
                }
                return Visibility.Collapsed;
            }
        }

        Visibility _addCreditVisibility;
        /// <summary>
        /// Видимость кнопки открытия кредита
        /// </summary>
        public Visibility AddCreditVisibility
        {
            get => _addCreditVisibility;
            set => Set(ref  _addCreditVisibility, value);
        }
        public int GridColumnCredit
        {
            get
            {
                if (_selectedClient != null)
                {
                    return _selectedClient.Deposit == 0 ? 2 : 3;
                }
                return 3;
            }
        }
        #endregion

        #region CreateCredit
        /// <summary>
        /// Открыть интерфейс открытия кредита
        /// </summary>
        public IAsyncCommand CreateCreditAsync { get; }

        async Task OnCreateCreditAsyncExecuted(object p)
        {
            AddCreditVisibility = Visibility.Collapsed;
            AddCreditBlockViewModel = new(_selectedClient);
            _addCreditBlockViewModel.Saveing += AddBlockViewModel_Saveing;
        }

        

        bool CanCreateCreditAsyncExecute(object p) => _selectedClient != null && _selectedClient.Credit == 0;
        #endregion

        #region CreateDeposit
        /// <summary>
        /// Открыть интерфейс открытия вклада
        /// </summary>
        public IAsyncCommand CreateDepositAsync { get; }

        async Task OnCreateDepositAsyncExecuted(object p)
        {
            AddDepositVisibility = Visibility.Collapsed;
            AddDepositBlockViewModel = new(_selectedClient);
            _addDepositBlockViewModel.Saveing += AddBlockViewModel_Saveing;
        }
        bool CanCreateDepositAsyncExecute(object p) => _selectedClient != null && _selectedClient.Deposit == 0;
        #endregion

        private void AddBlockViewModel_Saveing()
        {
            OnSaving();
        }

        public ClientInfoViewModel(Client selectedClient)
        {
            SelectedClient = selectedClient;
            _addCreditVisibility = selectedClient.Credit == 0 ? Visibility.Visible : Visibility.Collapsed;
            _addDepositVisibility = selectedClient.Deposit == 0 ? Visibility.Visible : Visibility.Collapsed;
            CreateCreditAsync = new LambdaCommandAsync(OnCreateCreditAsyncExecuted, CanCreateCreditAsyncExecute);
            CreateDepositAsync = new LambdaCommandAsync(OnCreateDepositAsyncExecuted, CanCreateDepositAsyncExecute);
        }
    }
}
