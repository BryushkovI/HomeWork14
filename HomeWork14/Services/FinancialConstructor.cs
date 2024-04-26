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
        public FinancialConstructor(Client client)
        {
            Client = client;
        }
        /// <summary>
        /// Получение полной суммы кредита к концу выплаты
        /// </summary>
        /// <param name="beginSum">Сумма выданного кредита</param>
        /// <param name="endCreditDate">Дата погашения кредта</param>
        /// <returns>Сумма кредита с процентами</returns>
        public double GetCreditSum(double beginSum, DateTime endCreditDate)
        {
            double monthlyPayment = GetMonthlyCreditPayment(beginSum, endCreditDate);
            return Math.Round(monthlyPayment * CountMonthCredit(endCreditDate), 2);
        }
        /// <summary>
        /// Получение суммы ежемесячного платежа по кредиту
        /// </summary>
        /// <param name="beginSum">Сумма выданного кредита</param>
        /// <param name="endCreditDate">Дата погашения кредита</param>
        /// <returns></returns>
        double GetMonthlyCreditPayment(double beginSum, DateTime endCreditDate)
        {
            int monthCount = CountMonthCredit(endCreditDate);
            double monthlyRate = GetMonthlyCreditRate();
            return beginSum * Math.Pow(monthlyRate,monthCount) * (monthlyRate-1) / (Math.Pow(monthlyRate,monthCount) - 1);
        }
        /// <summary>
        /// Получение количества месяцев, на которое выдан кредит
        /// </summary>
        /// <param name="endCreditDate">Дата погашения кредита</param>
        /// <returns></returns>
        int CountMonthCredit(DateTime endCreditDate)
        {
            Period period = Period.Between(new LocalDate(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day),
                                           new LocalDate(endCreditDate.Year, endCreditDate.Month, endCreditDate.Day));
            return DateTime.Today.Day == endCreditDate.Day ? period.Months + period.Years * 12 : period.Months + period.Years * 12 + 1;
        }
        /// <summary>
        /// Получение месячной ставки по кредиту (с учетом тела кредита)
        /// </summary>
        /// <returns></returns>
        double GetMonthlyCreditRate()
        {
            return 1 + Client.CreditPercent / 12 / 100;
        }
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
        /// <param name="beginSum">Сумма вклада</param>
        /// <returns></returns>
        double GetMonthlyDepositPeymentNoCap(double beginSum)
        {
            return beginSum * GetMonthlyDepositRate();
        }
        /// <summary>
        /// Получение полной суммы вклада (без капитализации)
        /// </summary>
        /// <param name="beginSum">Сумма вклада</param>
        /// <param name="endDepositDate">Дата закрытия вклада</param>
        /// <returns></returns>
        public double GetDepositSumNoCap(double beginSum, DateTime endDepositDate)
        {
            return Math.Round(beginSum * (1 + GetMonthlyDepositRate() * CountMonthDeposit(endDepositDate)), 2);
        }
        /// <summary>
        /// Получение полной суммы вклада (с капитализацией)
        /// </summary>
        /// <param name="beginSum"></param>
        /// <param name="endDepositDate"></param>
        /// <returns></returns>
        public double GetDepositSumCap(double beginSum, DateTime endDepositDate)
        {
            return Math.Round( beginSum * Math.Pow(1 + GetMonthlyDepositRate(), CountMonthDeposit(endDepositDate)) );
        }
        /// <summary>
        /// Получение количества месяцев, на которое открыт вклад
        /// </summary>
        /// <param name="endDepositDate"></param>
        /// <returns></returns>
        int CountMonthDeposit(DateTime endDepositDate)
        {
            Period period = Period.Between(new LocalDate(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day),
                                           new LocalDate(endDepositDate.Year, endDepositDate.Month, endDepositDate.Day));
            return DateTime.Today.Day == endDepositDate.Day ? period.Months + period.Years * 12 : period.Months + period.Years * 12;
        }
    }
}
