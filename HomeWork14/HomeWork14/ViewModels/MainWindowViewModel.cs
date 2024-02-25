using HomeWork14.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HomeWork14.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Транжирбанк";
        #region Комманды
        #region CloseAppCommand
        public ICommand CloseAppCommand { get; }
        private void OnCloseAppCommandExecuted(object p)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private bool CanCloseAppCommandExecute(object p) => true; 
        #endregion
        #endregion
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        public MainWindowViewModel()
        {
            CloseAppCommand = new LambdaCommand(OnCloseAppCommandExecuted, CanCloseAppCommandExecute);
        }
    }
}
