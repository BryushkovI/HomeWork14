using HomeWork15.Command;
using HomeWork15.DataProvider;
using HomeWork15.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
            set { ClientType = value ? ClientTypes.Regular : ClientType; }
        }
        public bool IsVIP
        {
            get { return ClientType == ClientTypes.VIP; }
            set { ClientType = value ? ClientTypes.VIP : ClientType; }
        }
        public bool IsEntity
        {
            get { return ClientType == ClientTypes.Entity; }
            set { ClientType = value ? ClientTypes.Entity : ClientType; }
        }

        private ClientTypes _client_type;
        public ClientTypes ClientType
        {
            get => _client_type;
            set { _client_type = value; } 
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
        private ObservableCollection<Client> _clients;
        public ObservableCollection<Client> Clients
        {
            get
            {
                return _clients;
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
        private void OnOpenDBExecuted(object p)
        {
            //OpenFileDialog openFileDialog = new();
            //openFileDialog.InitialDirectory = "c:\\";
            //openFileDialog.ShowDialog();
            //string filename = openFileDialog.FileName ?? null;
            // сделать вывод по 20 штук из общей БД, по выбранному отделу
            // при прокрутке выводить следующие 20 штук
            // при поиске искать по Имени во всех БД асинхронно
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
