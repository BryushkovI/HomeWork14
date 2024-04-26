using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.ViewModels
{
    internal class AddDepositBlockViewModel : AddBlockViewModel
    {
        bool _capitalization;
        /// <summary>
        /// Капитализация
        /// </summary>
        public bool Capitalization
        {
            get => _capitalization;
            set
            {
                Set(ref _capitalization, value);
                if (_date != null)
                {
                    _sumEnd = _capitalization ? _finConstructor.GetDepositSumCap()
                                              : _finConstructor.GetDepositSumNoCap();
                    OnPropertyChanged(nameof(SumEnd));
                }
            }
        }

        public override string Sum
        {
            get => base.Sum;
            set
            {
                base.Sum = value;
                if (_date != null)
                {
                    _finConstructor.UpdateClientDepositSum(_sumStart);
                    _sumEnd = _capitalization ? _finConstructor.GetDepositSumCap()
                                              : _finConstructor.GetDepositSumNoCap();
                    OnPropertyChanged(nameof(SumEnd));
                }
            }
        }

        public override DateTime? Date
        {
            get => base.Date;
            set
            {
                base.Date = value;
                if (_date != null)
                {
                    _finConstructor.UpdateClientDepositSum(_sumStart);
                    _finConstructor.UpdateClientDepositEnd((DateTime)_date);
                    _sumEnd = _capitalization ? _finConstructor.GetDepositSumCap()
                                              : _finConstructor.GetDepositSumNoCap();
                    OnPropertyChanged(nameof(SumEnd));
                }
            }
        }
        public AddDepositBlockViewModel(Client client) : base(client)
        {
        }

    }
}
