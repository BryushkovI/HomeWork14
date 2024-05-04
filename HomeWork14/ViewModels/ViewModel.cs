using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace HomeWork15.ViewModels
{
    abstract internal class ViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }
        #endregion

        #region Saving
        public delegate void ViewModelHandler();

        public event ViewModelHandler Saveing;

        public void OnSaving()
        {
            Saveing?.Invoke();
        }
        #endregion

        public delegate void LoggerHandler(string message, params object[] args);
        public event LoggerHandler Log;
        public void OnLog(string message, params object[] args)
        {
            Log?.Invoke(message, args);
        }
    }
}
