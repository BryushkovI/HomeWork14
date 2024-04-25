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
            set => Set(ref _capitalization, value);
        }

        public AddDepositBlockViewModel(Client client) : base(client)
        {
        }

    }
}
