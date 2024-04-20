using ChartKit.SkiaSharpView;
using ChartKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChartKit.SkiaSharpView.Painting.Effects;
using ChartKit.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.ObjectModel;
using ChartKit.Defaults;
using System.Globalization;
using Coffee.DTOs;
using System.Windows.Input;
using Coffee.Services;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json.Linq;
using System.Windows.Markup;
using ChartKit.SkiaSharpView.WPF;
using Coffee.Views.Admin.StatisticPage;

namespace Coffee.ViewModel.AdminVM.Statistic
{
   

    public partial class StatisticViewModel:BaseViewModel 
    {
        #region variable
    public IEnumerable<ISeries> Series;
    public List<DateTimePoint> dateTimePoints = new List<DateTimePoint>();
    private ObservableCollection<BillDTO> _BillListchart;
    public ObservableCollection<BillDTO> BillListchart
    {
        get { return _BillListchart; }
        set { _BillListchart = value; OnPropertyChanged(); }
    }

    #endregion

        //Biểu đồ cột x
        public Axis[] XAxes { get; set; } =
        {
        new DateTimeAxis(TimeSpan.FromDays(1), date => date.ToString("dd/MM/yyyy"))
        };
        //Biểu đồ cột y
        public Axis[] YAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                     Labeler = value => string.Format(new CultureInfo("vi-VN"), "{0:C0}", value)
                }
            };
        //public async void Loadthongtinbill(DateTime fromDate = default(DateTime), DateTime toDate = default(DateTime))
        //{
        //    (string label, List<BillDTO> billlist) = await BillService.Ins.getListBilltime(fromDate, toDate);

        //    if (billlist != null)
        //    {
        //        BillListchart = new ObservableCollection<BillDTO>(billlist);
        //    }
        //    var groupedBills = BillListchart.GroupBy(
        //b => DateTime.ParseExact(b.NgayTao, "HH:mm:ss dd/MM/yyyy", CultureInfo.InvariantCulture).Date,
        //b => Convert.ToInt32(b.TongTien),
        //(date, totals) => new { Date = date, Total = totals.Sum() });

        //    foreach (var group in groupedBills)
        //    {

        //        dateTimePoints.Add(new DateTimePoint(group.Date, group.Total));
        //    }
        //    Series = new ObservableCollection<ISeries>
        //   {
        //       new LineSeries<DateTimePoint>
        //       {
        //           Values = dateTimePoints,
        //           Fill=null
        //       }
        //   };
        //    StatisticPage statisticPage = new StatisticPage();
        //    statisticPage.linechart.Series = Series;
        //}

    }
}


