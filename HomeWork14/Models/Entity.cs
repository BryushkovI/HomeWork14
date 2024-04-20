using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.Models
{
    class Entity : Client
    {
        public Entity(string name, double bankAccout = 0) : base(name, bankAccout)
        {
            Random rnd = new();
            AccountNumber = rnd.Next(30000000, 39999999);
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
