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

        private DateTime _CreateDate;
        /// <summary>
        /// Дата создания УЗ клиента
        /// </summary>
        public DateTime CreateDate { get => _CreateDate; set => _CreateDate = value; }
        
        private string _Name;
        /// <summary>
        /// Имя
        /// </summary>
        public string Name { get => _Name; set => _Name = value; }

        private readonly int _AccountNumber;
        public int AccountNumber
        {
            get => _AccountNumber;
        }

        private double _Deposit;
        /// <summary>
        /// Вклад
        /// </summary>
        public double Deposit { get => _Deposit; set => _Deposit = value; }


        private double _Credit;
        /// <summary>
        /// Кредит
        /// </summary>
        public double Credit { get => _Credit; set => _Credit = value; }


        private double _Debt;
        /// <summary>
        /// Дебит (баланс)
        /// </summary>
        public double Debt { get => _Debt; set => _Debt = value; }

        private double _CreditPercent;
        /// <summary>
        /// Процент по кредиту
        /// </summary>
        public virtual double CreditPercent { get => _CreditPercent; set => _CreditPercent = value; }


        private double _DepositPercent;
        public virtual double DepositePercent { get => _DepositPercent; set => _DepositPercent = value; }

    }
}
