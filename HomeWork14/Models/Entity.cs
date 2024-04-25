using HomeWork15.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HomeWork15.Models
{
    class Entity : Client
    {
        /// <summary>
        /// Конструктор для новых пользователей
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="bankAccout">Счет</param>
        [JsonConstructor]
        public Entity(string name, double bankAccout = 0) : base(name, bankAccout)
        {
            Random rnd = new();
            AccountNumber = rnd.Next(30000000, 39999999);
        }
        /// <summary>
        /// Конструктор для изменения клиентов
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="accountNumber">Текущий номер аккаунта</param>
        /// <param name="bankAccout">Счет</param>
        public Entity(string name, int accountNumber, double bankAccout) : base(name, accountNumber, bankAccout)
        {

        }

        public override double CreditPercent
        {
            get { return _CreditPercent = _KeyRate + 0.5; }
            set => _CreditPercent = value;
        }

        public override double DepositPercent
        {
            get { return _DepositPercent = _KeyRate - 0.5; }
            set => _DepositPercent = value;
        }

        public override int AccountType { get => 2; }
    }
}
