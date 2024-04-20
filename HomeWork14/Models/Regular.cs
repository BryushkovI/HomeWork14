using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.Models
{
    class Regular : Client
    {

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
