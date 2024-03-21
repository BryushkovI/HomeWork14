using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.Models
{   
    /// <summary>
    /// Клинет
    /// </summary>
    abstract class Client
    {

        protected DateTime _CreateDate;
        /// <summary>
        /// Дата создания УЗ клиента
        /// </summary>
        public DateTime CreateDate { get => _CreateDate; set => _CreateDate = value; }
        
        protected string _Name;
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get => _Name; set => _Name = value; }

        protected readonly int _AccountNumber;
        /// <summary>
        /// Аккаунт
        /// </summary>
        public int AccountNumber
        {
            get => _AccountNumber;
        }

        /// <summary>
        /// Ключевая ставка
        /// </summary>
        protected readonly double _KeyRate;

        protected double _Deposit;
        /// <summary>
        /// Вклад
        /// </summary>
        public double Deposit { get => _Deposit; set => _Deposit = value; }


        protected double _Credit;
        /// <summary>
        /// Кредит
        /// </summary>
        public double Credit { get => _Credit; set => _Credit = value; }


        protected double _Debt;
        /// <summary>
        /// Дебит (баланс)
        /// </summary>
        public double Debt { get => _Debt; set => _Debt = value; }

        protected double _CreditPercent;
        /// <summary>
        /// Процент по кредиту
        /// </summary>
        public virtual double CreditPercent { get => _CreditPercent; set => _CreditPercent = value; }


        protected double _DepositPercent;
        public virtual double DepositePercent { get => _DepositPercent; set => _DepositPercent = value; }

    }
}
