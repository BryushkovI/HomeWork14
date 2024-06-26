﻿using HomeWork15.Command;
using HomeWork15.Command.Base;
using HomeWork15.Models;
using HomeWork15.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.ViewModels
{
    internal class AddBlockViewModel : ViewModel
    {

        Client _client;

        protected FinancialConstructor _finConstructor;

        protected int _sumStart;
        virtual public string Sum
        {
            get => _sumStart.ToString();
            set
            {
                if (int.TryParse(value, out _))
                {
                    Set(ref _sumStart, int.Parse(value));
                }
                else
                {
                    Set(ref _sumStart, 0);
                }
            }
        }

        protected double _sumEnd;

        virtual public string SumEnd
        {
            get =>  string.Format("{0:f2}", _sumEnd);
        }

        protected DateTime? _date;

        virtual public DateTime? Date
        {
            get => _date;
            set
            {
                if (value >= DateTime.Today.AddMonths(1))
                {
                    Set(ref _date, value);
                }
                
            }
        }

        #region CreateDeposit
        public IAsyncCommand Create { get; }

        async Task OnCreateExecuted(object p)
        {
            OnSaving();
        }

        bool CanCreateExecute(object p) => _sumEnd != 0;
        #endregion

        public AddBlockViewModel(Client client)
        {
            _client = client;
            _finConstructor = new(_client);
            Create = new LambdaCommandAsync(OnCreateExecuted, CanCreateExecute);
        }


    }
}