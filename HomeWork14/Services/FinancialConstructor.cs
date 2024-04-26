using HomeWork15.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NodaTime.Calendars;
using NodaTime;

namespace HomeWork15.Services
{
    class FinancialConstructor
    {
        Client Client {  get; set; }

        public void UpdateClientCreditSum(double sum) => Client.Credit = sum;

        public void UpdateClientCreditEnd(DateTime date)
        {
            Client.DateCreditEnd = date;
            Client.DateCreditBegin = DateTime.Now;
        }

        public void UpdateClientDepositSum(double sum) => Client.Deposit = sum;

        public void UpdateClientDepositEnd(DateTime date)
        {
            Client.DateDepositEnd = date;
            Client.DateDepositBegin = DateTime.Now;
        }

        public FinancialConstructor(Client client)
        {
            Client = client;
        }

        #region Выдача кредитов

        /// <summary>
        /// Получение полной суммы кредита к концу выплаты
        /// </summary>
        public double GetCreditSum()
        {
            double monthlyPayment = GetMonthlyCreditPayment();
            return Math.Round(monthlyPayment * CountMonthCredit(), 2);
        }
        /// <summary>
        /// Получение суммы ежемесячного платежа по кредиту
        /// </summary>
        double GetMonthlyCreditPayment()
        {
            int monthCount = CountMonthCredit();
            double monthlyRate = GetMonthlyCreditRate();
            return Client.Credit * Math.Pow(monthlyRate, monthCount) * (monthlyRate - 1) / (Math.Pow(monthlyRate, monthCount) - 1);
        }
        /// <summary>
        /// Получение количества месяцев, на которое выдан кредит
        /// </summary>
        int CountMonthCredit()
        {
            Period period = Period.Between(new LocalDate(Client.DateCreditBegin.Year, Client.DateCreditBegin.Month, Client.DateCreditBegin.Day),
                                           new LocalDate(Client.DateCreditEnd.Year, Client.DateCreditEnd.Month, Client.DateCreditEnd.Day));
            return DateTime.Today.Day == Client.DateCreditEnd.Day ? period.Months + period.Years * 12 : period.Months + period.Years * 12 + 1;
        }


        /// <summary>
        /// Получение месячной ставки по кредиту (с учетом тела кредита)
        /// </summary>
        /// <returns></returns>
        double GetMonthlyCreditRate()
        {
            return 1 + Client.CreditPercent / 12 / 100;
        } 
        #endregion

        #region Открытие вкладов
        /// <summary>
        /// Получение месячной ставки по вкладу (с учетом тела вклада)
        /// </summary>
        /// <returns></returns>
        double GetMonthlyDepositRate()
        {
            return Client.DepositPercent / 12 / 100;
        }
        /// <summary>
        /// Получение суммы месячной выплаты по вкладу
        /// </summary>
        /// <returns></returns>
        double GetMonthlyDepositPaymentNoCap()
        {
            return Client.Deposit * GetMonthlyDepositRate();
        }
        /// <summary>
        /// Получение полной суммы вклада (без капитализации)
        /// </summary>
        /// <returns></returns>
        public double GetDepositSumNoCap()
        {
            return Math.Round(Client.Deposit * (1 + GetMonthlyDepositRate() * CountMonthDeposit()), 2);
        }
        /// <summary>
        /// Получение полной суммы вклада (с капитализацией)
        /// </summary>
        /// <param name="beginSum"></param>
        /// <param name="endDepositDate"></param>
        /// <returns></returns>
        public double GetDepositSumCap()
        {
            return Math.Round(Client.Deposit * Math.Pow(1 + GetMonthlyDepositRate(), CountMonthDeposit()));
        }
        /// <summary>
        /// Получение количества месяцев, на которое открыт вклад
        /// </summary>
        /// <param name="endDepositDate"></param>
        /// <returns></returns>
        int CountMonthDeposit()
        {
            Period period = Period.Between(new LocalDate(Client.DateDepositBegin.Year, Client.DateDepositBegin.Month, Client.DateDepositBegin.Day),
                                           new LocalDate(Client.DateDepositEnd.Year, Client.DateDepositEnd.Month, Client.DateDepositEnd.Day));
            return DateTime.Today.Day == Client.DateDepositEnd.Day ? period.Months + period.Years * 12 : period.Months + period.Years * 12;
        }
        #endregion

        #region Логика для графиков (уже выданных кредитов/депозитов)

        public Dictionary<DateTime, double> GetTableOfPayments()
        {
            double creditSum = GetCreditSum();
            Dictionary<DateTime, double> tablePaymants = new()
            {
                { Client.DateCreditBegin, creditSum }
            };

            for (int i = 1; i < CountMonthCredit() - 1; i++)
            {
                creditSum -= GetMonthlyCreditPayment();
                tablePaymants.Add(Client.DateCreditBegin.AddMonths(i), creditSum);
            }
            tablePaymants.Add(Client.DateCreditEnd, 0);
            return tablePaymants;
        }
        #endregion

        public Dictionary<DateTime, double> GetTableOfProfits()
        {
            Dictionary<DateTime, double> tableProfits = new()
            {
                { Client.DateDepositBegin, 0 }
            };



            return tableProfits;
        }
    }
}
