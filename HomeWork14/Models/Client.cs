using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.Models
{   
    /// <summary>
    /// Клинет
    /// </summary>
    abstract class Client : INotifyPropertyChanged
    {
        
        protected DateTime _CreateDate;
        /// <summary>
        /// Дата создания УЗ клиента
        /// </summary>
        [JsonProperty("CreateDate")]
        protected DateTime CreateDate { get => _CreateDate; set => _CreateDate = value; }
        
        protected string _Name;
        /// <summary>
        /// Имя
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get => _Name; set => _Name = value; }

        protected int _AccountNumber;
        /// <summary>
        /// Аккаунт
        /// </summary>
        [JsonProperty("AccountNumber")]
        public int AccountNumber
        {
            get => _AccountNumber;
            set => _AccountNumber = value;
        }

        /// <summary>
        /// Ключевая ставка
        /// </summary>
        protected readonly double _KeyRate = 10;

        protected double _Deposit;
        /// <summary>
        /// Вклад
        /// </summary>
        [JsonProperty("Deposit")]
        public double Deposit { get => _Deposit; set => _Deposit = value; }

        protected DateTime _DateDepositBegin;
        /// <summary>
        /// Дата открытия вклада
        /// </summary>
        public DateTime DateDepositBegin { get => _DateDepositBegin; set => _DateDepositBegin = value; }

        protected DateTime _DateDepositEnd;
        /// <summary>
        /// Дата погашения вклада
        /// </summary>
        public DateTime DateDepositEnd { get => _DateDepositEnd; set => _DateDepositEnd = value; }

        protected bool _Capitalization;
        [JsonProperty("Capitalization")]
        public bool Capitalization { get => _Capitalization; set => _Capitalization = value; }

        protected double _Credit;
        /// <summary>
        /// Кредит
        /// </summary>
        [JsonProperty("Credit")]
        public double Credit { get => _Credit; set => _Credit = value; }

        protected DateTime _DateCreditBegin;
        /// <summary>
        /// Дата начала кредита
        /// </summary>
        public DateTime DateCreditBegin { get => _DateCreditBegin; set => _DateCreditBegin = value; }

        protected DateTime _DateCreditEnd;
        /// <summary>
        /// Дата погашения кредита
        /// </summary>
        public DateTime DateCreditEnd { get => _DateCreditEnd; set => _DateCreditEnd = value; }

        protected double _CreditPercent;
        /// <summary>
        /// Процент по кредиту
        /// </summary>
        public abstract double CreditPercent { get; set; }


        protected double _DepositPercent;
        public abstract double DepositPercent { get; set; }

        protected double _BankAccount;

        public event PropertyChangedEventHandler PropertyChanged;

        [JsonProperty("BankAccount")]
        public double BankAccount { get => _BankAccount; set => _BankAccount = value; }
        
        public Client()
        {
            
        }

    }

    struct TitleClient
    {
        public string AccountNumber { get; set; }
        public string Name { get; set; }

        public TitleClient(string name, string accountNumber)
        {
            AccountNumber = accountNumber;
            Name = name;
        }
    }
}
