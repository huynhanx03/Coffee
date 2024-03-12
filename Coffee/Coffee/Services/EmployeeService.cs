using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Utils;
using Coffee.Utils.Helper;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Services
{
    public class EmployeeService
    {
        private static EmployeeService _ins;
        public static EmployeeService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new EmployeeService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// Thêm nhân viên
        /// INPUT: employee: Nhân viên
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>
        ///     1: Lỗi khi thêm dữ liệu
        ///     2: Nhân viên
        /// </returns>
        public async Task<(string, EmployeeDTO)> createEmpoloyee(EmployeeDTO employee)
        {
            // Tạo mã nhân viên mới nhất
            string MaxMaNhanVien = await this.getMaxMaNhanVien();
            string NewMaNhanVien = Helper.nextID(MaxMaNhanVien, "NV");

            // Tạo employee
            EmployeeDTO _employee = new EmployeeDTO { 
                MaNhanVien = NewMaNhanVien,
                MaChucVu = employee.MaChucVu,
                Luong = employee.Luong,
            };

            // Thêm user
            UserDTO user = new UserDTO {
                HoTen = employee.HoTen,
                CCCD_CMND = employee.CCCD_CMND,
                DiaChi = employee.DiaChi,
                Email = employee.Email,
                GioiTinh = employee.GioiTinh,
                NgaySinh = employee.NgaySinh,
                NgayTao = employee.NgayLam,
                SoDienThoai = employee.SoDienThoai,
                TaiKhoan = employee.TaiKhoan,
                MatKhau = employee.MatKhau,
                VaiTro = 3,
                MaNguoiDung = NewMaNhanVien,
                HinhAnh = employee.HinhAnh
            };

            //Kiểm tra có trùng CCCD/CMND không
            bool IsCheckIDCard = await UserService.Ins.checkIDCard(user);

            if (IsCheckIDCard)
                return ("CCCD/CMND đã tồn tại", null);

            //Kiểm tra có trùng email không
            bool IsCheckEmail = await UserService.Ins.checkEmail(user);

            if (IsCheckEmail)
                return ("Email đã tồn tại", null);

            //Kiểm tra có trùng số điện thoại không
            bool IsCheckNumberPhone = await UserService.Ins.checkNumberPhone(user);

            if (IsCheckNumberPhone)
                return ("Số điện thoại đã tồn tại", null);

            //Kiểm tra có trùng tên đăng nhập không
            bool IsCheckUsername = await UserService.Ins.checkNumberPhone(user);

            if (IsCheckUsername)
                return ("Tên tài khoản đã tồn tại", null);

            (string labelEmployee, EmployeeDTO __employee) = await EmployeeDAL.Ins.createEmpoloyee(_employee);
            (string labelUser, UserDTO _user) = await UserService.Ins.createUser(user);

            employee.MaNhanVien = NewMaNhanVien;

            if (_user != null && __employee != null)
            {
                return ("Thêm nhân viên thành công", employee);
            }
            else
            {
                return ("Thêm nhân viên thất bại", null);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Mã nhân viên lớn nhất
        /// </returns>
        public async Task<string> getMaxMaNhanVien()
        {
            return await EmployeeDAL.Ins.getMaxMaNhanVien();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Danh sách nhân viên
        /// </returns>
        public async Task<(string, List<EmployeeDTO>)> getListEmployee()
        {
            return await EmployeeDAL.Ins.getListEmployee();
        }
    }
}
