using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.ViewModels
{
    internal class AddCreditBlockViewModel : AddBlockViewModel
    {
        /// <summary>
        /// Сумма выданного кредита
        /// </summary>
        public override string Sum
        {
            get => base.Sum;
            set
            {
                base.Sum = value;
                if (_date != null)
                {
                    _finConstructor.UpdateClientCreditSum(_sumStart);
                    _sumEnd = _finConstructor.GetCreditSum();
                    OnPropertyChanged(nameof(SumEnd));
                }
            }
        }
        /// <summary>
        /// Дата погашения кредита
        /// </summary>
        public override DateTime? Date
        {
            get => base.Date;
            set
            {
                base.Date = value;
                if (_date != null)
                {
                    _finConstructor.UpdateClientCreditEnd((DateTime)_date);
                    _finConstructor.UpdateClientCreditSum(_sumStart);
                    _sumEnd = _finConstructor.GetCreditSum();
                    OnPropertyChanged(nameof(SumEnd));
                }
            }
        }

        public AddCreditBlockViewModel(Client client) : base(client)
        {
        }
    }
}
