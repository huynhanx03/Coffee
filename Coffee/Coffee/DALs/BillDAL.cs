using Coffee.DTOs;
using Coffee.Models;
using Coffee.Utils;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace Coffee.DALs
{
    public class BillDAL
    {
        private static BillDAL _ins;
        public static BillDAL Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillDAL();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// Tạo hoá đơn mới
        /// </summary>
        /// <param name="bill">hoá đơn</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. True khi tạo thành công
        /// </returns>
        public async Task<(string, bool)> createBill(BillDTO bill)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.SetTaskAsync("HoaDon/" + bill.MaHoaDon, bill);

                    return ("Thêm hoá đơn thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }

        /// <summary>
        ///     Lấy mã hoá đơn lớn nhất
        /// </summary>
        /// <returns>
        ///     Mã hoá đơn lớn nhất
        /// </returns>
        public async Task<string> getMaxMaHoaDon()
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse response = await context.Client.GetTaskAsync("HoaDon");

                    if (response.Body != null && response.Body != "null")
                    {
                        Dictionary<string, BillDTO> data = response.ResultAs<Dictionary<string, BillDTO>>();

                        string MaxMaHoaDon = data.Values.Select(i => i.MaHoaDon).Max();

                        return MaxMaHoaDon;
                    }

                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Tạo chi tiết hoá đơn mới
        /// </summary>
        /// <param name="BillID"> Mã của hoá đơn </param>
        /// <param name="detailList"> List chi tiết hoá đơn</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. True khi tạo thành công
        /// </returns>
        ///
        public async Task<(string, bool)> createDetailBill(string BillID, List<DetailBillModel> detailList)
        {
            try
            {
                using (var context = new Firebase())
                {
                    foreach (var detail in detailList)
                    {
                        await context.Client.SetTaskAsync("HoaDon/" + BillID + "/ChiTietHoaDon/" + detail.MaSanPham + "-" + detail.MaKichThuoc, detail);
                    }

                    return ("Thêm hoá đơn thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }

        /// <summary>
        /// Xoá hoá đơn
        /// </summary>
        /// <param name="BillID"></param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> DeleteBill(string BillID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.DeleteTaskAsync("HoaDon/" + BillID);
                    return ("Xoá hoá đơn thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }


        /// <summary>
        /// Tìm kiếm hoá đơn với bàn đã được đặt (chưa thanh toán)
        /// </summary>
        /// <param name="tableID">mã bàn</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. Hoá đơn
        /// </returns>
        public async Task<(string, BillDTO)> findBillByTableBooking(string tableID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse billResponse = await context.Client.GetTaskAsync("HoaDon");
                    Dictionary<string, BillDTO> billData = billResponse.ResultAs<Dictionary<string, BillDTO>>();
                    BillDTO bill = billData.Values.FirstOrDefault(x => x.MaBan == tableID && x.TrangThai == Constants.StatusBill.UNPAID);

                    if (bill != null)
                        return ("Tìm thành công", bill);
                    else
                        return ("Không tồn tại",  null);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// lấy chi tiết hoá đơn với mã hoá đơn
        /// </summary>
        /// <param name="billID">chi tiết hoá đơn</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. Chi tiết hoá đơn (danh sách)
        /// </returns>
        public async Task<(string, List<DetailBillDTO>)> getDetailBillById(string billID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse billResponse = await context.Client.GetTaskAsync("HoaDon/" + billID);
                    BillDTO bill = billResponse.ResultAs<BillDTO>();
                    List<DetailBillDTO> listDetailBill = bill.ChiTietHoaDon.Values.ToList();

                    // Lấy dữ liệu từ nút "Product" trong Firebase
                    FirebaseResponse productResponse = await context.Client.GetTaskAsync("SanPham");
                    Dictionary<string, ProductDTO> productData = productResponse.ResultAs<Dictionary<string, ProductDTO>>();

                    foreach(DetailBillDTO detailBill in listDetailBill)
                    {
                        ProductDTO product = productData.Values.First(x => x.MaSanPham == detailBill.MaSanPham);

                        detailBill.TenSanPham = product.TenSanPham;
                        detailBill.DanhSachChiTietKichThuocSanPham = new ObservableCollection<ProductSizeDetailDTO>(product.ChiTietKichThuocSanPham.Values);
                        detailBill.SelectedProductSize = detailBill.DanhSachChiTietKichThuocSanPham.First(x => x.MaKichThuoc == detailBill.MaKichThuoc);
                    }

                    return ("Lấy danh sách chi tiết hoá đơn thành công", listDetailBill);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }
    }
}
