using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork14.ViewModels
{
    class MainWindowViewModel : ViewModel
    {
        private string _Title = "Транжирбанк";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        public MainWindowViewModel()
        {

        }
    }
}
