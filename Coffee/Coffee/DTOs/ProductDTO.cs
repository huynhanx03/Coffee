using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DTOs
{
    public class ProductDTO
    {
        public string MaSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public string MaLoaiSanPham { get; set; }
        public string HinhAnh { get; set; }
        public int SoLuong { get; set; }
    }
}
