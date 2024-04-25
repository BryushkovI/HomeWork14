using HomeWork15.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.Models
{
    class VIP : Client
    {
        /// <summary>
        /// Конструктор для новых пользователей
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="bankAccout">Счет</param>
        [JsonConstructor]
        public VIP(string name, double bankAccout = 0) : base(name, bankAccout)
        {
                Random rnd = new();
                AccountNumber = rnd.Next(20000000, 29999999);
        }
        /// <summary>
        /// Конструктор для изменения клиентов
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="accountNumber">Текущий номер аккаунта</param>
        /// <param name="bankAccout">Счет</param>
        public VIP(string name, int accountNumber, double bankAccout) : base(name, accountNumber, bankAccout)
        {

        }

        public override double CreditPercent
        {
            get { return _CreditPercent = _KeyRate + 1; }
            set => _CreditPercent = value;
        }

        public override double DepositPercent
        {
            get { return _DepositPercent = _KeyRate - 1; }
            set => _DepositPercent = value;
        }
        public override int AccountType { get => 1; }
    }
}
