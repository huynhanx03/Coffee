﻿using Coffee.DTOs;
using Coffee.Utils;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DALs
{
    public class ProductDAL
    {
        private static ProductDAL _ins;
        public static ProductDAL Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new ProductDAL();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// Thêm sản phẩm
        /// </summary>
        /// <param name="product"> Sản phẩm </param>
        /// <returns>
        ///     1: Lỗi khi thêm dữ liệu
        ///     2: Sản phẩm
        /// </returns>
        public async Task<(string, ProductDTO)> createProduct(ProductDTO product)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.SetTaskAsync("SanPham/" + product.MaSanPham, product);

                    return ("Thêm sản phẩm thành công", product);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// Cập nhật sản phẩm
        /// </summary>
        /// <param name="product"> Sản phẩm </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: Sản phẩm
        /// </returns>
        public async Task<(string, ProductDTO)> updateProduct(ProductDTO product)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.UpdateTaskAsync("SanPham/" + product.MaSanPham, product);

                    return ("Cập nhật sản phẩm thành công", product);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Mã sản phẩm lớn nhất
        /// </returns>
        public async Task<string> getMaxMaSanPham()
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse response = await context.Client.GetTaskAsync("SanPham");

                    if (response.Body != null && response.Body != "null")
                    {
                        Dictionary<string, ProductDTO> data = response.ResultAs<Dictionary<string, ProductDTO>>();

                        string MaxMaSanPham = data.Values.Select(p => p.MaSanPham).Max();

                        return MaxMaSanPham;
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
        /// 
        /// </summary>
        /// <returns>
        ///     Danh sách sản phẩm
        /// </returns>
        //public async Task<(string, List<ProductDTO>)> getListProduct()
        //{
        //    try
        //    {
        //        using (var context = new Firebase())
        //        {
        //            // Lấy dữ liệu từ nút "Employees" trong Firebase
        //            FirebaseResponse employeesResponse = await context.Client.GetTaskAsync("NhanVien");
        //            Dictionary<string, EmployeeDTO> employeesData = employeesResponse.ResultAs<Dictionary<string, EmployeeDTO>>();

        //            // Lấy dữ liệu từ nút "Users" trong Firebase
        //            FirebaseResponse usersResponse = await context.Client.GetTaskAsync("NguoiDung");
        //            Dictionary<string, UserDTO> usersData = usersResponse.ResultAs<Dictionary<string, UserDTO>>();

        //            // Lấy dữ liệu từ nút "Position" trong Firebase
        //            FirebaseResponse positionResponse = await context.Client.GetTaskAsync("ChucDanh");
        //            Dictionary<string, PositionDTO> positionData = positionResponse.ResultAs<Dictionary<string, PositionDTO>>();


        //            var result = (from employee in employeesData.Values
        //                          join user in usersData.Values
        //                          on employee.MaNhanVien equals user.MaNguoiDung
        //                          join position in positionData.Values
        //                          on employee.MaChucVu equals position.MaChucVu
        //                          select new EmployeeDTO
        //                          {
        //                              HoTen = user.HoTen,
        //                              CCCD_CMND = user.CCCD_CMND,
        //                              DiaChi = user.DiaChi,
        //                              Email = user.Email,
        //                              GioiTinh = user.GioiTinh,
        //                              HinhAnh = user.HinhAnh,
        //                              Luong = employee.Luong,
        //                              SoDienThoai = user.SoDienThoai,
        //                              MaNhanVien = employee.MaNhanVien,
        //                              MatKhau = user.MatKhau,
        //                              NgaySinh = user.NgaySinh,
        //                              NgayLam = user.NgayTao,
        //                              TaiKhoan = user.TaiKhoan,
        //                              MaChucVu = employee.MaChucVu,
        //                              TenChucVu = position.TenChucVu
        //                          }).ToList();

        //            return ("Lấy danh sách nhân viên thành công", result);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return (ex.Message, null);
        //    }
        //}

        /// <summary>
        /// Xoá sản phẩm
        /// </summary>
        /// <param name="ProductID"> Mã sản phẩm </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> DeleteProduct(string ProductID)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.DeleteTaskAsync("SanPham/" + ProductID);
                    return ("Xoá sản phẩm thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
    }
}
