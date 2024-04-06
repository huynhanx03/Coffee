using Coffee.Views.Admin.StatisticPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Statistic
{
    public partial class StatisticViewModel : BaseViewModel
    {
        #region variable
        public Frame MainFrame;
        #endregion

        #region Icommand
        public ICommand loadMainFrame { get; set; }
        public ICommand loadSaleHistoryIC { get; set; }
        public ICommand loadImportHistoryIC { get; set; }
        public ICommand loadStatisticIC { get; set; }
        #endregion

        public StatisticViewModel()
        {
            loadMainFrame = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame = p;
            });
            loadSaleHistoryIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new SaleHistoryPage();
            });
            loadImportHistoryIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new ImportHistoryPage();
            });
            loadStatisticIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                MainFrame.Content = new StatisticPage();
            });

            #region
            loadBillListIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                loadBillList();
            });
            #endregion
        }
        #region operation

        #endregion
    }
}
