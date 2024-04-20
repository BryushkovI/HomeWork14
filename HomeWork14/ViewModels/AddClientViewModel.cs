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
        public IAsyncCommand CreateClientAsync { get; }

        async Task OnCreateClientAsyncExecuted(object p)
        {
            if (ClientType.ClientType == typeof(Regular))
            {
                Client client = new Regular()
                {
                    Name = _clientName,
                    AccountNumber = 12312341,
                    BankAccount = _bankAccount,

                };
                IParser parser = new Parser();
                await parser.SerializeClientAsync(@"Clients.json", client);
            }
            else if(ClientType.ClientType == typeof(VIP))
            {

            }
            else if (ClientType.ClientType == typeof(Entity))
            {

            }
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