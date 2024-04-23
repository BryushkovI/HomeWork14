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
        [JsonConstructor]
        public VIP(string name, double bankAccout = 0) : base(name, bankAccout)
        {
                Random rnd = new();
                AccountNumber = rnd.Next(20000000, 29999999);
        }
        public VIP(string name, int accountNumber, double bankAccout = 0) : base(name, bankAccout)
        {
            AccountNumber = accountNumber;
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
