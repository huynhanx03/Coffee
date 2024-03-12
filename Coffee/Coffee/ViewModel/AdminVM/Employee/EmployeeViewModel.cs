using Coffee.DTOs;
using Coffee.Services;
using Coffee.Utils;
using Coffee.Views.Admin.EmployeePage;
using Coffee.Views.MessageBox;
using Microsoft.Win32;
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

        private ObservableCollection<EmployeeDTO> _EmployeeList;

        public ObservableCollection<EmployeeDTO> EmployeeList
        {
            get { return _EmployeeList; }
            set { _EmployeeList = value; OnPropertyChanged(); }
        }

        private List<EmployeeDTO> __employeeList;

        #endregion

        #region ICommand
        public ICommand loadShadowMaskIC { get; set; }
        public ICommand openWindowAddEmployeeIC { get; set; }
        public ICommand loadEmployeeListIC { get; set; }
        public ICommand searchEmployeeIC { get; set; }
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

            loadEmployeeListIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                loadEmployeeList();
            });

            openWindowAddEmployeeIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                MaskName.Visibility = Visibility.Visible;
                loadPosition();
                WorkingDay = DateTime.Now;
                OperationEmployeeWindow w = new OperationEmployeeWindow();
                TypeOperation = 1; // Add employee
                w.ShowDialog();
                MaskName.Visibility = Visibility.Collapsed;
            });

            searchEmployeeIC = new RelayCommand<TextBox>(null, (p) =>
            {
                if (p.Text != null)
                {
                    if (__employeeList != null)
                        EmployeeList = new ObservableCollection<EmployeeDTO>(__employeeList.FindAll(x => x.HoTen.ToLower().Contains(p.Text.ToLower())));
                }
            });

            #region operation
            confirmOperationEmployeeIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                confirmOperationEmployee();
            }); 

            closeOperationEmployeeWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            uploadImageIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.webp;*.gif|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {

                    Image = openFileDialog.FileName;
                    if (Image != null)
                    {
                        // Image was uploaded successfully.                        
                    }
                    else
                    {
                        MessageBoxCF ms = new MessageBoxCF("Tải ảnh lên thất bại", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                    }
                }
            });
            #endregion
        }

        private async void loadEmployeeList()
        {
            (string label, List<EmployeeDTO> employees) = await EmployeeService.Ins.getListEmployee();

            if (employees != null)
            {
                EmployeeList = new ObservableCollection<EmployeeDTO>(employees);
                __employeeList = new List<EmployeeDTO>(employees);
            }
        }
    }
}
