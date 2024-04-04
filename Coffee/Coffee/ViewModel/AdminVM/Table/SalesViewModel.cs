using Coffee.DTOs;
using Coffee.Models;
using Coffee.Services;
using Coffee.Utils;
using Coffee.Views.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Coffee.Utils.Constants;

namespace Coffee.ViewModel.AdminVM.Table
{
    public partial class MainTableViewModel: BaseViewModel
    {
        #region variable
        private ObservableCollection<DetailBillDTO> _DetailBillList = new ObservableCollection<DetailBillDTO>();
        public ObservableCollection<DetailBillDTO> DetailBillList
        {
            get { return _DetailBillList; }
            set { _DetailBillList = value; OnPropertyChanged(); }
        }

        private DetailBillDTO _SelectedDetailBill;
        public DetailBillDTO SelectedDetailBill
        {
            get { return _SelectedDetailBill; }
            set { _SelectedDetailBill = value; OnPropertyChanged(); }
        }

        private decimal _TotalBill;

        public decimal TotalBill
        {
            get { return _TotalBill; }
            set { _TotalBill = value; OnPropertyChanged(); }
        }

        private string _TableNameSale;

        public string TableNameSale
        {
            get { return _TableNameSale; }
            set { _TableNameSale = value; OnPropertyChanged(); }
        }

        private string _Created;

        public string Created
        {
            get { return _Created; }
            set { _Created = value; OnPropertyChanged(); }
        }

        private string _EmployeeName;

        public string EmployeeName
        {
            get { return _EmployeeName; }
            set { _EmployeeName = value; OnPropertyChanged(); }
        }

        private TableDTO currentTable { get; set; }

        #endregion

        #region ICommend
        public ICommand changeSizeProductIC { get; set; }
        public ICommand subQuantityBillIC { get; set; }
        public ICommand plusQuantityBillIC { get; set; }
        public ICommand removeBillIC { get; set; }
        public ICommand loadDateSalesIC { get; set; }
        public ICommand bookingIC { get; set; }

        #endregion


        /// <summary>
        /// Thay đổi kích thước
        /// </summary>
        /// <param name="detalBill"> chi tiết hoá đơn </param>
        private void changeSizeProduct(DetailBillDTO detalBill)
        {
            SelectedDetailBill.ThanhTien = SelectedDetailBill.SoLuong * SelectedDetailBill.SelectedProductSize.Gia;
            
            DetailBillDTO find = DetailBillList.FirstOrDefault(x => x != SelectedDetailBill && x.MaSanPham == SelectedDetailBill.MaSanPham && x.SelectedProductSize == SelectedDetailBill.SelectedProductSize);
            
            if (find != null)
            {
                SelectedDetailBill.SoLuong += find.SoLuong;
                SelectedDetailBill.ThanhTien = SelectedDetailBill.SoLuong * SelectedDetailBill.SelectedProductSize.Gia;
                DetailBillList.Remove(find);
            }

            DetailBillList = new ObservableCollection<DetailBillDTO>(DetailBillList);
            CalculateTotalBill();
        }

        /// <summary>
        /// Giảm số lượng của sản phẩm tại hoá đơn
        /// </summary>
        private void subQuantityBill()
        {
            if (SelectedDetailBill.SoLuong > 1)
            {
                SelectedDetailBill.SoLuong -= 1;
                SelectedDetailBill.ThanhTien = SelectedDetailBill.SelectedProductSize.Gia * SelectedDetailBill.SoLuong;
            }
            else
            {
                //
            }

            DetailBillList = new ObservableCollection<DetailBillDTO>(DetailBillList);
            CalculateTotalBill();
        }

        /// <summary>
        /// Tăng số lượng của sản phẩm tại hoá đơn
        /// </summary>
        private void plusQuantityBill()
        {
            if (SelectedDetailBill.SoLuong > -1)
            {
                SelectedDetailBill.SoLuong += 1;
                SelectedDetailBill.ThanhTien = SelectedDetailBill.SelectedProductSize.Gia * SelectedDetailBill.SoLuong;
            }
            else
            {
                //
            }

            DetailBillList = new ObservableCollection<DetailBillDTO>(DetailBillList);
            CalculateTotalBill();
        }

        /// <summary>
        /// Xoá sản phẩm ra khỏi hoá đơn
        /// </summary>
        private void removeBill()
        {
            MessageBoxCF ms = new MessageBoxCF("Xác nhận gỡ sản phẩm này khỏi hoá đơn", MessageType.Waitting, MessageButtons.YesNo);
            if (ms.ShowDialog() == true)
            {
                DetailBillList.Remove(SelectedDetailBill);
                DetailBillList = new ObservableCollection<DetailBillDTO>(DetailBillList);
                CalculateTotalBill();
            }
        }

        /// <summary>
        /// Tính tổng tiền của hoá đơn
        /// </summary>
        private void CalculateTotalBill()
        {
            decimal total = 0;

            foreach (var item in DetailBillList)
                total += item.ThanhTien;
        
            TotalBill = total;
        }

        /// <summary>
        /// load dữ liệu của bán hàng
        /// </summary>
        private void loadDateSales()
        {
            Created = DateTime.Now.ToString("dd/MM/yyyy");
            EmployeeName = Memory.user.HoTen;
        }

        /// <summary>
        /// Đặt món: đặt bàn tại thu ngân nhưng chưa tính tiền
        /// </summary>
        private async void booking()
        {
            // Lưu thế thông tin chi tiết hoá đơn lên trên cơ sở dữ liệu
            BillDTO bill = new BillDTO
            {
                MaBan = currentTable.MaBan,
                MaNhanVien = Memory.user.MaNguoiDung,
                NgayTao = DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy"),
                TongTien = TotalBill,
                TrangThai = StatusBill.UNPAID
            };

            (string label, bool isCreate) = await BillService.Ins.createBill(bill, DetailBillList);
        
            if (isCreate)
            {
                MessageBoxCF ms = new MessageBoxCF(label, MessageType.Accept, MessageButtons.OK);
                ms.ShowDialog();
            }
            else
            {
                MessageBoxCF ms = new MessageBoxCF(label, MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
            }
        }
    }
}
