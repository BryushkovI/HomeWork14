using HomeWork15.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.Models
{
    class Regular : Client
    {
        /// <summary>
        /// Конструктор для изменения клиентов
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="accountNumber">Текущий номер аккаунта</param>
        /// <param name="bankAccout">Счет</param>
        public Regular(string name, int accountNumber, double bankAccout) : base(name, accountNumber, bankAccout)
        {

        }
        /// <summary>
        /// Конструктор для новых пользователей
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="bankAccout">Счет</param>
        [JsonConstructor]
        public Regular(string name, double bankAccout = 0) : base(name, bankAccout)
        {
                Random rnd = new();
                AccountNumber = rnd.Next(10000000, 19999999);
        }
        

        public override double CreditPercent
        {
            get { return _CreditPercent = _KeyRate + 1.5; }
            set => _CreditPercent = value;
        }
        
        public override double DepositPercent
        {
            get { return _DepositPercent = _KeyRate - 1.5; }
            set => _DepositPercent = value;
        }

        public override int AccountType { get => 0; }
    }
}
