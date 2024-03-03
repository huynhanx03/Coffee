using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM
{
    public class MainAdminViewModel: BaseViewModel
    {
        public int _role { get; set; }
        public int role 
        {
            get { return _role; }
            set { _role = value; OnPropertyChanged(); } 
        }

        public ICommand loadDashboardPageIC { get; set; }
        public ICommand loadTablesPageIC { get; set; }
        public ICommand loadItemsPageIC { get; set; }
        public ICommand loadIngredientsPageIC { get; set; }
        public ICommand loadBillsPageIC { get; set; }
        public ICommand loadEmployeePageIC { get; set; }
        public ICommand loadSettingPageIC { get; set; }
        public ICommand loadRoleIC { get; set; }

        private string _optionName { get; set; }
        public string optionName
        {
            get { return _optionName; }
            set { _optionName = value; OnPropertyChanged(); }
        }

        public MainAdminViewModel()
        {
            loadRoleIC = new RelayCommand<object>((p) => { return true; }, async (p) =>
            {
                role = 1;
            });

            loadDashboardPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                optionName = "Trang chủ";
            });

            loadTablesPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                optionName = "Bàn";
            });

            loadItemsPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                optionName = "Thực đơn";
            });

            loadIngredientsPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                optionName = "Nguyên liệu";
            });

            loadBillsPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                optionName = "Hoá đơn";
            });

            loadEmployeePageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                optionName = "Nhân viên";
            });

            loadSettingPageIC = new RelayCommand<Frame>((p) => { return true; }, (p) =>
            {
                optionName = "Cài đặt";
            });
        }
    }
}
