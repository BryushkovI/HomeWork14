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
        public WorkSpaceViewModel(Client client, bool isCredit)
        {
            PlotModel = new PlotModel();
            var dateAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                Title = "Дата",
                StringFormat = "dd.MM.yyyy",
                Minimum = isCredit ? DateTimeAxis.ToDouble(client.DateCreditBegin) : DateTimeAxis.ToDouble(client.DateDepositBegin),
                Maximum = isCredit ? DateTimeAxis.ToDouble(client.DateCreditEnd) : DateTimeAxis.ToDouble(client.DateDepositEnd)
            };
            PlotModel.Axes.Add(dateAxis);
            var valueAxis = new LinearAxis { Position = AxisPosition.Left, Minimum = 0 };
            PlotModel.Axes.Add(valueAxis);
            PlotModel.Series.Add(HomeWork15.Services.PlotDataProvider.LoadData(client));
        }
    }
}
