using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;

namespace Homework_13
{
    public class FrontEndMethods
    {
        /// <summary>
        /// Показывает или убирает данные о выделенном клиенте
        /// </summary>
        /// <param name="mainWindow"></param>
        public void ClearOrFill(MainWindow mainWindow, DateTime now)
        {
            Client client;
            if (mainWindow.Entity_List.SelectedItem != null)
            {
                client = (Client)mainWindow.Entity_List.SelectedItem;
            }
            else if (mainWindow.Individual_regular_List.SelectedItem != null)
            {
                client = (Client)mainWindow.Individual_regular_List.SelectedItem;
            }
            else
            {
                client = (Client)mainWindow.Individual_VIP_List.SelectedItem;
            }

            if (client != null)
            {
                mainWindow.Name.Text = client.Name;
                mainWindow.Number.Text = client.Number.ToString();
                mainWindow.Bank_account.Text = client.Bank_Account.ToString();
                mainWindow.Deposite.Text = Client.Capitalization(client, now).ToString();
                mainWindow.Credit.Text = Client.Creditation(client,now).ToString();
            }
            else
            {
                mainWindow.Name.Text = "";
                mainWindow.Number.Text = "";
                mainWindow.Bank_account.Text = "";
                mainWindow.Deposite.Text = "";
                mainWindow.Credit.Text = "";
            }
        }
        /// <summary>
        ///  Проверяет является ли вводимый текст цифрами
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool InputTextCheck(TextCompositionEventArgs e)
        {
            if (!int.TryParse(e.Text, out int result))
            {
                return e.Handled = true;
            }
            else return e.Handled = false;
        }
        /// <summary>
        /// Метод вызывается при изменении выбора клиента из списка
        /// </summary>
        /// <param name="sender">Передаваемый оюъект из view</param>
        public void List_Selection_Changed(object sender, MainWindow mainWindow)
        {
            DateTime now = DateTime.Now;
            ClearOrFill(mainWindow, now);
            mainWindow.Remittance1.IsEnabled = true;
            ListView ClientList = (ListView)sender;
            Client client = (Client)ClientList.SelectedItem;
            if (ClientList.SelectedItem != null && client.Credit == 0)
            {
                mainWindow.OpenCredit.Visibility = Visibility.Visible;
            }
            else mainWindow.OpenCredit.Visibility = Visibility.Collapsed;
            if (ClientList.SelectedItem != null && client.Deposite == 0)
            {
                mainWindow.OpenDeposite.Visibility = Visibility.Visible;
            }
            else mainWindow.OpenDeposite.Visibility = Visibility.Collapsed;
            if (ClientList.SelectedItem != null)
            {
                mainWindow.Btn_deliteClient.IsEnabled = true;
                mainWindow.Prediction.Visibility = Visibility.Visible;
            }
            else
            {
                mainWindow.Btn_deliteClient.IsEnabled = false;
                mainWindow.Prediction.Visibility = Visibility.Collapsed;
                mainWindow.Predict_years_text.Text = "0";
                mainWindow.Predict_month_text.Text = "0";
            }
        }
        /// <summary>
        /// Метод вызывается при изменении данных клиента
        /// </summary>
        /// <param name="sender"></param>
        public void Edit_Client(object sender, MainWindow mainWindow)
        {
            TextBox text = (TextBox)sender;
            ObservableCollection<ListView> listViews = new ObservableCollection<ListView>()
            {
                mainWindow.Entity_List,
                mainWindow.Individual_regular_List,
                mainWindow.Individual_VIP_List
            };
            foreach (var i in listViews)
            {
                if (i.SelectedItem != null)
                {
                    foreach (Client u in i.Items)
                    {
                        if (!string.IsNullOrEmpty(text.Text) && u == i.SelectedItem)
                        {
                            switch (text.Name)
                            {
                                case "Name":
                                    u.Name = text.Text;
                                    break;
                                case "Bank_account":
                                    u.Bank_Account = int.Parse(text.Text);
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                i.Items.Refresh();
            }
        }
        /// <summary>
        /// Метод проводит расчет кредита и депозита на заданный срок
        /// </summary>
        public void Predict(MainWindow mainWindow)
        {
            DateTime now = DateTime.Now;
            now = now.AddYears(int.Parse(mainWindow.Predict_years_text.Text));
            now = now.AddMonths(int.Parse(mainWindow.Predict_month_text.Text));
            ClearOrFill(mainWindow, now);
            mainWindow.Predict_month_text.Text = "0";
            mainWindow.Predict_years_text.Text = "0";

        }
        /// <summary>
        /// Метод проверяет запленнность полей при выдачи кредита
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="window"></param>
        public void EnabledButton(object sender, NewCredit window)
        {
            TextBox textBox = (TextBox)sender;
            if (!string.IsNullOrEmpty(textBox.Text) && textBox.Text != "Введите сумму кредита") window.Confirm_btn.IsEnabled = true;
            if (string.IsNullOrEmpty(textBox.Text)) window.Confirm_btn.IsEnabled = false;
        }
        /// <summary>
        /// Проверка ввденных данных при переводе при выборе адресата
        /// </summary>
        /// <param name="window"></param>
        public void Remittance_Selection_Change(Remittance window)
        {
            if (window.List.SelectedItem is Client && !string.IsNullOrEmpty(window.Txtbox_Sum.Text) && int.Parse(window.Txtbox_Sum.Text) <= int.Parse(window.TxtBlockBank_account.Text))
            {
                window.btn_Remittance.IsEnabled = true;
                window.List.IsEnabled = true;
            }
            else window.btn_Remittance.IsEnabled = false;
        }
    }
}
