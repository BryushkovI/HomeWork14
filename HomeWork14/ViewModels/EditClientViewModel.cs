using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HomeWork15.ViewModels
{
    internal class EditClientViewModel : ViewModel
    {
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

        private double _credit;
        public string Credit
        {
            get => _credit.ToString();
            set
            {
                if (double.TryParse(value.ToString(), out _))
                {
                    Set(ref _credit, double.Parse(value));
                    SelectedClient.Credit = _credit;
                }
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

        private double _deposit;
        public string Deposit
        {
            get => _deposit.ToString();
            set
            {
                if (double.TryParse(value.ToString(), out _))
                {
                    Set(ref _deposit, double.Parse(value));
                    SelectedClient.Deposit = _deposit;
                }
            }
        }
        #endregion

        #region Данные клиента
        private Client _selectedClient;
        public Client SelectedClient
        {
            get => _selectedClient;
            set => Set(ref _selectedClient, value);
        }
        private double _bankAccount = 0;
        /// <summary>
        /// Счет
        /// </summary>
        public string BankAccount
        {
            get => _bankAccount.ToString();
            set
            {
                if (double.TryParse(value.ToString(), out _))
                {
                    Set(ref _bankAccount, double.Parse(value));
                    SelectedClient.BankAccount = _bankAccount;
                }
            }
        }

        public int AccountNumber { get; }
        #endregion

        #region Типы клиентов
        readonly ObservableCollection<ClientTypeDic> _clientTypes = new()
        {
            { new(){ ClientType = typeof(Regular), ClientTypeName = "Базовый" } },
            { new(){ ClientType = typeof(VIP), ClientTypeName = "VIP" } },
            { new(){ ClientType = typeof(Entity), ClientTypeName="Для Бизнеса"} }
        };
        /// <summary>
        /// Типы клиентов 
        /// </summary>
        public ObservableCollection<ClientTypeDic> ClientTypes
        {
            get => _clientTypes;
        }
        ClientTypeDic _clientType;
        /// <summary>
        /// Тип клиента
        /// </summary>
        public ClientTypeDic ClientType
        {
            get => _clientType;
            set
            {
                Set(ref _clientType, value);
                Client bufferClient = _selectedClient;
                switch (_clientTypes.IndexOf(_clientTypes.Where(e => e.ClientType == _clientType.ClientType).Single()))
                {
                    case 0:
                        _selectedClient = new Regular(bufferClient.Name, bufferClient.AccountNumber, bufferClient.BankAccount);
                        break;
                    case 1:
                        _selectedClient = new VIP(bufferClient.Name, bufferClient.AccountNumber, bufferClient.BankAccount);
                        break;
                    case 2:
                        _selectedClient = new Entity(bufferClient.Name,   bufferClient.AccountNumber, bufferClient.BankAccount);
                        break;
                    default:
                        break;
                }
                _selectedClient.Capitalization = bufferClient.Capitalization;
                _selectedClient.Credit = bufferClient.Credit;
                _selectedClient.DateCreditBegin = bufferClient.DateCreditBegin;
                _selectedClient.DateCreditEnd = bufferClient.DateCreditEnd;
                _selectedClient.DateDepositBegin = bufferClient.DateDepositBegin;
                _selectedClient.DateDepositEnd = bufferClient.DateDepositEnd;
                _selectedClient.Deposit = bufferClient.Deposit;
            }
        }
        #endregion


        public EditClientViewModel(Client client)
        {
            _selectedClient = client;
            BankAccount = client.BankAccount.ToString();
            ClientType = _clientTypes.Where(e => e.ClientType == client.GetType()).Single();
            Credit = client.Credit.ToString();
            Deposit = client.Deposit.ToString();
        }
    }
}
