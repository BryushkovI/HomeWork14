using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Homework_13
{
    public class Function
    {
        /// <summary>
        /// Логика перевода средств со счета на счет
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="recipent"></param>
        /// <param name="sum"></param>
        public void Remit(int sender, int recipent, int sum)
        {
            MainWindow mainWindow = (MainWindow)App.Current.MainWindow;
            foreach(var e in mainWindow.departments)
            {
                foreach(var i in e.ClientList)
                {
                    if (i.Number == sender) i.Bank_Account -= sum;
                    if (i.Number == recipent) i.Bank_Account += sum;
                }
            }
            mainWindow.Entity_List.Items.Refresh();
            mainWindow.Individual_regular_List.Items.Refresh();
            mainWindow.Individual_VIP_List.Items.Refresh();
            mainWindow.Entity_List.SelectedItem = null;
            mainWindow.Individual_regular_List.SelectedItem = null;
            mainWindow.Individual_VIP_List.SelectedItem = null;
            mainWindow.Remittance1.IsEnabled = false;
        }
        /// <summary>
        /// Логика создания нового клиента
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public Client AddClient(AddClient window)
        {
            Methods methods = new Methods();
            Client client;
            switch (window.ComboBox_Type.Text)
            {

                case "Юр. лицо":
                    client = new Entity(methods.GetNum(window));
                    break;
                case "Физ. лицо":
                    client = new Individual_regular(methods.GetNum(window));
                    break;
                default:
                    client = new Individual_VIP(methods.GetNum(window));
                    break;
            }

            client.Name = window.TextBox_Name.Text;
            client.Bank_Account = 0;
            client.Deposite = 0;
            if(window.Combobox_Caital.Text == "С капитализацией")
            {
                client.Deposite_Type = "WithCapital";
            }
            else
            {
                client.Deposite_Type = "WithoutCapital";
            }
            client.Credit = 0;
            client.Type = client.GetType().ToString().Substring(12);
            return client;
        }
        /// <summary>
        /// Логика открытия кредита
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="newCredit"></param>
        public void OpenNewCredit(MainWindow mainWindow, NewCredit newCredit)
        {
            ListView listView;
            if (mainWindow.Entity_List.SelectedItem != null) listView = mainWindow.Entity_List;
            else if (mainWindow.Individual_regular_List.SelectedItem != null) listView = mainWindow.Individual_regular_List;
            else listView = mainWindow.Individual_VIP_List;

            Client client = (Client)listView.SelectedItem;
            client.Credit = int.Parse(newCredit.Sum.Text);
            client.Date_credit = DateTime.Now;
            listView.SelectedItem = null;
            listView.Items.Refresh();
        }
        /// <summary>
        /// Логика открытия вклада
        /// </summary>
        /// <param name="mainWindow"></param>
        /// <param name="newDeposite"></param>
        public void OpenNewDeposite(MainWindow mainWindow, NewDeposite newDeposite)
        {
            ListView listView;
            if (mainWindow.Entity_List.SelectedItem != null) listView = mainWindow.Entity_List;
            else if (mainWindow.Individual_regular_List.SelectedItem != null) listView = mainWindow.Individual_regular_List;
            else listView = mainWindow.Individual_VIP_List;

            Client client = (Client)listView.SelectedItem;
            client.Deposite = int.Parse(newDeposite.Sum.Text);
            client.Date_deposite = DateTime.Now;
            listView.SelectedItem = null;
            listView.Items.Refresh();
        }
        /// <summary>
        /// Логика удаления клиента
        /// </summary>
        /// <param name="mainWindow"></param>
        public void DeliteClient(MainWindow mainWindow)
        {
            Department<Client> clients;
            if (mainWindow.Entity_List.SelectedItem != null) clients = mainWindow.departments[0];
            else if (mainWindow.Individual_regular_List.SelectedItem != null) clients = mainWindow.departments[1];
            else clients = mainWindow.departments[2];

            foreach(var e in clients.ClientList)
            {
                if(e.Number == int.Parse(mainWindow.Number.Text))
                {
                    clients.ClientList.Remove(e);
                    break;
                }
            }
        }
    }
}
