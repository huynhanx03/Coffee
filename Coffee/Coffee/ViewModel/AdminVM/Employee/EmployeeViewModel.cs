using Coffee.DTOs;
using Coffee.Views.Admin.EmployeePage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Employee
{
    public partial class EmployeeViewModel: BaseViewModel
    {
        #region variable
        public Grid MaskName { get; set; }

        #endregion

        #region ICommand
        public ICommand loadShadowMaskIC { get; set; }
        public ICommand openWindowAddEmployeeIC { get; set; }
        #endregion

        public EmployeeViewModel()
        {
            ListGender = new ObservableCollection<string>{
                (string)Application.Current.Resources["Male"],
                (string)Application.Current.Resources["Female"],
                (string)Application.Current.Resources["Other"],
            };

            loadShadowMaskIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            openWindowAddEmployeeIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MaskName.Visibility = Visibility.Visible;
                OperationEmployeeWindow w = new OperationEmployeeWindow();
                TypeOperation = 1; // Add
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            #region operation
            confirmOperationEmployeeIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                EmployeeDTO employee = new EmployeeDTO
                {
                    HoTen = FullName,
                    CCCD_CMND = IDCard,
                    Email = Email,
                    SoDienThoai = NumberPhone,
                    DiaChi = Address,
                    GioiTinh = SelectedGender,
                    Chuc = SelectedPositionID,
                    Luong = Wage
                };
            }); 

            closeOperationEmployeeWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            #endregion
        }
    }
}
