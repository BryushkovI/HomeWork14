using HomeWork15.Command;
using HomeWork15.Command.Base;
using HomeWork15.Models;
using HomeWork15.Services;
using Newtonsoft.Json.Linq;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork15.ViewModels
{
    internal class AddClientViewModel : ViewModel
    {
        #region Данные клиента
        private string _clientName;
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string ClientName
        {
            get => _clientName;
            set => Set(ref _clientName, value);
        }

        private int _bankAccount = 0;
        /// <summary>
        /// Счет
        /// </summary>
        public string BankAccount
        {
            get => _bankAccount.ToString();
            set
            {
                if (int.TryParse(value.ToString(), out _))
                {
                    Set(ref _bankAccount, int.Parse(value));
                }
            }
        } 
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

        #region Команда Создать
        /// <summary>
        /// Сохранение нового клиента
        /// </summary>
        /// 
        public IAsyncCommand CreateClientAsync { get; }

        public Client _client;

        async Task OnCreateClientAsyncExecuted(object p)
        {
            IParser parser = new Parser();
            
            if (ClientType.ClientType == typeof(Regular))
            {
                _client = new Regular(_clientName, _bankAccount);
            }
            else if(ClientType.ClientType == typeof(VIP))
            {
                _client = new VIP(_clientName,_bankAccount);
            }
            else
            {
                _client = new Entity(_clientName, _bankAccount);
            }
           
            OnSaving();
        }
        bool CanCreateClientAsyncExecute(object p) => _clientType.ClientType != null && _clientName != null;

        #endregion


        public AddClientViewModel()
        {
            CreateClientAsync = new LambdaCommandAsync(OnCreateClientAsyncExecuted, CanCreateClientAsyncExecute);
        }
    }
    /// <summary>
    /// Тип клиента - название
    /// </summary>
    struct ClientTypeDic
    {
        Type _clientType;

        string _clientTypeName;

        public Type ClientType
        {
            get => _clientType;
            set => _clientType = value;
        }

        public string ClientTypeName
        {
            get => _clientTypeName;
            set => _clientTypeName = value;
        }
    }
}