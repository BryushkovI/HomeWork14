using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.ViewModels
{
    internal class EditClientViewModel : ViewModel
    {
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
            set => _clientType = value;
        }
        #endregion


        public EditClientViewModel(Client client)
        {
            _selectedClient.Name = client.Name;
            BankAccount = client.BankAccount.ToString();
            ClientType = _clientTypes.Where(e => e.ClientType == client.GetType()).Single();
            _selectedClient.AccountNumber = client.AccountNumber;
        }
    }
}
