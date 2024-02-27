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
using System.Windows.Input;

namespace HomeWork15.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Транжирбанк";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
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
        

        public ICommand OpenDB { get; }
        private void OnOpenDBExecuted(object p)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.InitialDirectory = "c:\\";
            openFileDialog.ShowDialog();
            string filename = openFileDialog.FileName ?? null;
            
        }

        private bool CanOpenDBExecute(object p) => true;
        #endregion
        public MainWindowViewModel()
        {
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
            OpenDB = new LambdaCommand(OnOpenDBExecuted, CanOpenDBExecute);
        }
    }
}
