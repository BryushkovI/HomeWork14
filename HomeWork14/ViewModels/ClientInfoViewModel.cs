using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static HomeWork15.ViewModels.MainWindowViewModel;

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
        public Visibility GridInfoVisibility
        {
            get
            {
                return _selectedClient == null ? Visibility.Hidden : Visibility.Visible;
            }
        }
        #region Карточка инфо
        public Dictionary<Type, string> ClientTypeDictionary = new()
        {
            { typeof(Regular), "Базовый" },
            { typeof(VIP), "VIP" },
            { typeof(Entity), "Для Бизнеса" }
        };
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


        public ClientInfoViewModel(Client selectedClient)
        {
            SelectedClient = selectedClient;
        }
    }
}
