using HomeWork15.Models;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.Services
{
    internal class PlotDataProvider
    {
        public static LineSeries LoadData(Client client)
        {
            LineSeries series = new();
            var finConstructor = new FinancialConstructor(client);
            var items = finConstructor.GetTableOfPayments();
            foreach ( var item in items )
            {
                series.Points.Add(new OxyPlot.DataPoint(DateTimeAxis.ToDouble(item.Key), item.Value));
            }
            return series;
        }
    }
}
