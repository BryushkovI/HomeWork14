﻿using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.Models
{
    internal class Department<T> where T : Client
    {
        private double _KeyRate;



        private int _DepartmentType;
        /// <summary>
        /// Тип отдела
        /// </summary>
        public int DepartmentType { get => _DepartmentType; set => _DepartmentType = value; }

        private double _BaseDepartmentCreditPercent;
        /// <summary>
        /// Базовая ставка по кредиту в отделе
        /// </summary>
        public double BaseDepartmentCreditPercent
        {
            get => _BaseDepartmentCreditPercent;
            set
            {
                switch (_DepartmentType)
                {
                    case 1:
                        _BaseDepartmentCreditPercent = _KeyRate + 0.5;
                        break;
                    case 2:
                        _BaseDepartmentCreditPercent = _KeyRate + 1.5;
                        break;
                    case 3:
                        _BaseDepartmentCreditPercent = _KeyRate + 1;
                        break;
                    default:
                        _BaseDepartmentCreditPercent = _KeyRate;
                        break;
                }
            }
        }

        private double _BaseDepartmentDepositPercent;
        public double BaseDepartmentDepositPercent
        {
            get => _BaseDepartmentDepositPercent;
            set
            {
                switch (_DepartmentType)
                {
                    case 1:
                        _BaseDepartmentDepositPercent = _KeyRate - 0.5;
                        break;
                    case 2:
                        _BaseDepartmentDepositPercent = _KeyRate - 1.5;
                        break;
                    case 3:
                        _BaseDepartmentDepositPercent = _KeyRate - 1;
                        break;
                    default:
                        _BaseDepartmentDepositPercent = _KeyRate;
                        break;
                }
            }
        }
        private ObservableCollection<Client> _Clients;
        public ObservableCollection<Client> Clients
        {
            get => _Clients;
            set => _Clients = value;
        }
    }
}
