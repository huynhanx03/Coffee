using Coffee.DTOs;
using Coffee.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Employee
{
    public partial class EmployeeViewModel: BaseViewModel
    {
        #region variable
        public string _FullName { get; set; }
        public string FullName
        {
            get { return _FullName; }
            set { _FullName = value; OnPropertyChanged(); }
        }

        public string _Gender { get; set; }
        public string Gender
        {
            get { return _Gender; }
            set { _Gender = value; OnPropertyChanged(); }
        }

        public string _Email { get; set; }
        public string Email
        {
            get { return _Email; }
            set { _Email = value; OnPropertyChanged(); }
        }

        public string _NumberPhone { get; set; }
        public string NumberPhone
        {
            get { return _NumberPhone; }
            set { _NumberPhone = value; OnPropertyChanged(); }
        }

        public string _IDCard { get; set; }
        public string IDCard
        {
            get { return _IDCard; }
            set { _IDCard = value; OnPropertyChanged(); }
        }

        public string _Address { get; set; }
        public string Address
        {
            get { return _Address; }
            set { _Address = value; OnPropertyChanged(); }
        }

        public DateTime _Birthday { get; set; }
        public DateTime Birthday
        {
            get { return _Birthday; }
            set { _Birthday = value; OnPropertyChanged(); }
        }

        public DateTime _WorkingDay { get; set; }
        public DateTime WorkingDay
        {
            get { return _WorkingDay; }
            set { _WorkingDay = value; OnPropertyChanged(); }
        }

        public string _Position { get; set; }
        public string Position
        {
            get { return _Position; }
            set { _Position = value; OnPropertyChanged(); }
        }

        public decimal _Wage { get; set; }
        public decimal Wage
        {
            get { return _Wage; }
            set { _Wage = value; OnPropertyChanged(); }
        }

        public ObservableCollection<PositionDTO> _ListPosition { get; set; }
        public ObservableCollection<PositionDTO> ListPosition
        {
            get { return _ListPosition; }
            set { _ListPosition = value; OnPropertyChanged(); }
        }

        public string _SelectedPositionID { get; set; }
        public string SelectedPositionID
        {
            get { return _SelectedPositionID; }
            set { _SelectedPositionID = value; OnPropertyChanged(); }
        }

        public ObservableCollection<string> _ListGender { get; set; }
        public ObservableCollection<string> ListGender
        {
            get { return _ListGender; }
            set { _ListGender = value; OnPropertyChanged(); }
        }

        public string _SelectedGender { get; set; }
        public string SelectedGender
        {
            get { return _SelectedGender; }
            set { _SelectedGender = value; OnPropertyChanged(); }
        }

        public int TypeOperation { get; set; }

        #endregion

        #region ICommand
        public ICommand confirmOperationEmployeeIC { get; set; }
        public ICommand closeOperationEmployeeWindowIC { get; set; }

        #endregion

        #region function
        /// <summary>
        /// Lấy danh sách chức vị
        /// </summary>
        public async void loadPosition()
        {
            (string label, List<PositionDTO> listPosition) = await PositionService.Ins.getAllPosition();

            if (listPosition != null)
            {
                ListPosition = new ObservableCollection<PositionDTO>(listPosition);
            }
            else
            {

            }
        }
        #endregion
    }
}
