using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Utils.Helper;
using Coffee.Utils;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Services
{
    public class IngredientService
    {
        private static IngredientService _ins;
        public static IngredientService Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new IngredientService();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// Thêm nguyên liệu
        /// </summary>
        /// <param name="ingredient"> Nguyên liệu </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: Nguyên liệu
        /// </returns>
        public async Task<(string, IngredientDTO)> createIngredient(IngredientDTO ingredient)
        {
            // Tạo mã nguyên liệu mới nhất
            string MaxMaNguyenLieu = await this.getMaxMaNguyenLieu();
            string NewMaNguyenLieu = Helper.nextID(MaxMaNguyenLieu, "NL");

            ingredient.MaNguyenLieu = NewMaNguyenLieu;

            return await IngredientDAL.Ins.createIngredient(ingredient);
        }

        /// <summary>
        /// Cập nhật nguyên liệu
        /// </summary>
        /// <param name="ingredient"> Nguyên liệu </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: Nguyên liệu
        /// </returns>
        public async Task<(string, IngredientDTO)> updateIngredient(IngredientDTO ingredient)
        {
            return await IngredientDAL.Ins.updateIngredient(ingredient);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Mã nguyên liệu lớn nhất
        /// </returns>
        public async Task<string> getMaxMaNguyenLieu()
        {
            return await IngredientDAL.Ins.getMaxMaNguyenLieu();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>
        ///     Danh sách nguyên liệu
        /// </returns>
        public async Task<(string, List<IngredientDTO>)> getListIngredient()
        {
            return await IngredientDAL.Ins.getListIngredient();
        }

        /// <summary>
        /// Xoá nguyên liệu 
        /// </summary>
        /// <param name="IngredientID"></param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> DeleteIngredient(string IngredientID)
        {
            return await IngredientDAL.Ins.DeleteIngredient(IngredientID);
        }
    }
}
