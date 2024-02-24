using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Homework_13
{
    public abstract class Client : INotifyPropertyChanged
    {
        /// <summary>
        /// Номер счета
        /// </summary>
        public int Number { get; }
        /// <summary>
        /// Имя клиента
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Тип клиента
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// Деньги на счету
        /// </summary>
        public int Bank_Account { get; set; }
        /// <summary>
        /// Депозитарный счет
        /// </summary>
        public int Deposite { get; set; }
        /// <summary>
        /// Кредитный счет
        /// </summary>
        public int Credit { get; set; }
        /// <summary>
        /// Процент по депозиту
        /// </summary>
        public abstract int Deposite_percent { get; }
        /// <summary>
        /// Процент по кредиту
        /// </summary>
        public abstract int Credit_percent { get; }
        /// <summary>
        /// Тип депозита
        /// </summary>
        public string Deposite_Type { get; set; }
        /// <summary>
        /// Дата открытия кредита
        /// </summary>
        public DateTime Date_credit { get; set; }
        /// <summary>
        /// Дата открытия вклада
        /// </summary>
        public DateTime Date_deposite { get; set; }

        public Client(int number)
        {
            Number = number;
            Date_credit = default;
            Date_deposite = default;
        }
        public Client(JObject client)
        {
            Number = int.Parse(client["Number"].ToString());
            Name = client["Name"].ToString();
            Type = GetType().ToString().Substring(12);
            Bank_Account = int.Parse(client["Bank_Account"].ToString());
            Deposite = int.Parse(client["Deposite"].ToString());
            Credit = int.Parse(client["Credit"].ToString());
            Deposite_Type = client["Deposite_type"].ToString();
            Date_credit = DateTime.Parse(client["Date_credit"].ToString());
            Date_deposite = DateTime.Parse(client["Date_deposite"].ToString());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged ([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public JObject SerializeClient(Client client)
        {
            JObject entity = new JObject()
            {
                ["Name"] = client.Name,
                ["Number"] = client.Number,
                ["Bank_Account"] = client.Bank_Account,
                ["Deposite"] = client.Deposite,
                ["Credit"] = client.Credit,
                ["Deposite_percent"] = client.Deposite_percent,
                ["Credit_percent"] = client.Credit_percent,
                ["Deposite_type"] = client.Deposite_Type,
                ["Date_credit"] = client.Date_credit,
                ["Date_deposite"] = client.Date_deposite
            };
            return entity;
        }

        public class WithCapital
        {
            public int Capital(Client x, DateTime now)
            {
                int DeltaDate = (now.Year - x.Date_deposite.Year) * 12 + (now.Month - x.Date_deposite.Month);
                int CurentDeposite = Convert.ToInt32(Math.Round(Convert.ToDouble(x.Deposite) * Math.Pow((1+Convert.ToDouble(x.Deposite_percent)/1200),DeltaDate)));
                return CurentDeposite;
            }
        }
        private class WithoutCapital
        {
            public int Capital(Client x, DateTime now)
            {
                int DeltaDate = now.Year - x.Date_deposite.Year;
                int CurentDeposite = Convert.ToInt32(Math.Round(Convert.ToDouble(x.Deposite)*(Convert.ToDouble(Convert.ToDouble(x.Deposite_percent)/100)*Convert.ToDouble(DeltaDate)+1)));
                return CurentDeposite;
            }
        }
        /// <summary>
        /// Расчет текущего вклада в зависимости от типа и от даты
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static int Capitalization(Client client, DateTime now)
        {
                if (client.Deposite_Type == "WithCapital") return new WithCapital().Capital(client, now);
                else return new WithoutCapital().Capital(client,now);
        }
        /// <summary>
        /// Расчет текущего кредита в зависимости от даты
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static int Creditation(Client client,DateTime now)
        {
                int DeltaDate = now.Year - client.Date_credit.Year;
                int CurentCredite = Convert.ToInt32(Math.Round(Convert.ToDouble(client.Credit) * (Convert.ToDouble(Convert.ToDouble(client.Credit_percent) / 100) * Convert.ToDouble(DeltaDate) + 1)));
                return CurentCredite;
        }

    }
}
