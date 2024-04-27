using HomeWork15.Models;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork15.ViewModels
{
    class WorkSpaceViewModel : ViewModel
    {
        public PlotModel PlotModel { get; set; }

        public Dictionary<DateTime, double> Items { get; set; }

        public string ColumnTitle { get; set; }
        public WorkSpaceViewModel(Client client, bool isCredit)
        {
            ColumnTitle = isCredit ? "Оставшаяся задолжность" : "Накопленный доход";
            PlotModel = new PlotModel();
            var dateAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Дата",
                StringFormat = "dd.MM.yyyy",
                Minimum = isCredit ? DateTimeAxis.ToDouble(client.DateCreditBegin) : DateTimeAxis.ToDouble(client.DateDepositBegin),
                Maximum = isCredit ? DateTimeAxis.ToDouble(client.DateCreditEnd) : DateTimeAxis.ToDouble(client.DateDepositEnd),
                IntervalType = DateTimeIntervalType.Months,
                IntervalLength = 1,
                MajorGridlineStyle = LineStyle.Solid,
                MajorGridlineColor = OxyColor.FromRgb(224, 224, 224),
                MajorGridlineThickness = 1,
                MinorGridlineStyle = LineStyle.None,
                IsZoomEnabled = true
            };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, Minimum = isCredit ? 0 : client.Deposit };
            PlotModel.Axes.Add(valueAxis);
            LineSeries line = Services.PlotDataProvider.LoadData(client, isCredit, out Dictionary<DateTime, double> items);
            Items = items;
            line.MarkerSize = 3;
            line.MarkerStrokeThickness = 3;
            line.StrokeThickness = 2;
            line.MarkerType = MarkerType.Circle;
            line.Selectable = true;
            PlotModel.Series.Add(line);
        }
    }
}
