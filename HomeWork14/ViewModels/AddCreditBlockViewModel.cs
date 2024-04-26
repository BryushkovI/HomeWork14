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
                    _sumEnd = _finConstructor.GetCreditSum(_sumStart, (DateTime)_date);
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
                    _sumEnd = _finConstructor.GetCreditSum(_sumStart, (DateTime)_date);
                    OnPropertyChanged(nameof(SumEnd));
                }
            }
        }

        public AddCreditBlockViewModel(Client client) : base(client)
        {
        }
    }
}
