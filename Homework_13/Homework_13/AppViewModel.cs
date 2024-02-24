using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Homework_13
{
    class AppViewModel : INotifyPropertyChanged
    {
        private Client client;

        private ObservableCollection<Department<Client>> clients;

        FileMethods fileMethods = new FileMethods();

        public AppViewModel()
        {
            clients = fileMethods.DeserializeClients(@"clients.json");
        }

        public Client Client
        {
            get
            {
                return this.client;
            }
            set
            {
                this.client = value;
                OnPropertyChanged("Client");
            }
        }

        public ObservableCollection<Department<Client>> Clients
        {
            get
            {
                return this.clients;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
