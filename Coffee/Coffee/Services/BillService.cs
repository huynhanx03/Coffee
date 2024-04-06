using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Models;
using Coffee.Utils;
using Coffee.Utils.Helper;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Services
{
    public class BillService
    {
        private static BillService _ins;
        public static BillService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillService();
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
        public async Task<(string, bool)> createBill(BillDTO bill, ObservableCollection<DetailBillDTO> detailBillList)
        {
            // Tìm mã hoá đơn
            string MaHoaDonMax = await this.getMaxMaHoaDon();
            bill.MaHoaDon = Helper.nextID(MaHoaDonMax, "HD");

            (string labelCreateBill, bool isCreateBill) = await BillDAL.Ins.createBill(bill);

            if (isCreateBill)
            {
                // Nếu tạo hoá đơn thành công thì tạo các chi tiết hoá đơn
                List<DetailBillModel> listDetailBill = new List<DetailBillModel>();

                foreach (DetailBillDTO detail in detailBillList)
                {
                    listDetailBill.Add(new DetailBillModel
                    {
                        MaHoaDon = bill.MaHoaDon,
                        MaSanPham = detail.MaSanPham,
                        MaKichThuoc = detail.SelectedProductSize.MaKichThuoc,
                        SoLuong = detail.SoLuong,
                        ThanhTien = detail.ThanhTien
                    });
                }

                (string labelCreateDetailBillImprot, bool isCreateDetailBillImprot) = await this.createDetailBill(bill.MaHoaDon, listDetailBill);

                if (isCreateDetailBillImprot)
                {
                    // Trừ số lượng vào món ăn


                    return (labelCreateBill, isCreateBill);
                }
                else
                {
                    // Tạo các chi tiết thất bại
                    // Xoá hoá đơn
                    await this.DeleteBill(bill.MaHoaDon);

                    return (labelCreateDetailBillImprot, false);
                }
            }
            else
                return (labelCreateBill, isCreateBill);
        }

        /// <summary>
        ///     Lấy mã hoá đơn lớn nhất
        /// </summary>
        /// <returns>
        ///     Mã hoá đơn lớn nhất
        /// </returns>
        public async Task<string> getMaxMaHoaDon()
        {
            return await BillDAL.Ins.getMaxMaHoaDon();
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
            return await BillDAL.Ins.createDetailBill(BillID, detailList);
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
            return await BillDAL.Ins.DeleteBill(BillID);
        }

        /// <summary>
        /// Danh sách hóa đơn
        /// </summary>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, List<BillDTO>)> getListBill()
        {
            return await BillDAL.Ins.getListBill();
        }

        /// <summary>
        /// Tìm kiếm hoá đơn của bàn đặt chỗ (chưa thanh toán)
        /// </summary>
        /// <param name="tableID">
        /// 
        /// </param>
        /// <returns></returns>
        public async Task<(string, BillDTO, List<DetailBillDTO>)> findBillByTableBooking(string tableID)
        {
            (string label, BillDTO bill) = await BillDAL.Ins.findBillByTableBooking(tableID);

            if (bill != null)
            {
                (string labelDetailBIll, List<DetailBillDTO> detailBillDTOList) = await BillDAL.Ins.getDetailBillById(bill.MaHoaDon);

                if (detailBillDTOList != null)
                    return ("Tìm thành công", bill, detailBillDTOList);
                else
                    return (labelDetailBIll, null, null);
            }
            else
                return (label, null, null);
        }
    }
}
