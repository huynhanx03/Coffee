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
        /// <param name="ingredient">Nguyên Liệu</param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu xoá thành công, False xoá thất bại
        /// </returns>
        public async Task<(string, bool)> DeleteIngredient(IngredientDTO Ingredient)
        {
            (string labelIngredient, bool isDeleteIngredient) = await IngredientDAL.Ins.DeleteIngredient(Ingredient.MaNguyenLieu);
            if (isDeleteIngredient)
            {
                return (labelIngredient, true);
            }
            else
            {
                return (labelIngredient, false);
            }
        }

        /// <summary>
        /// Lấy nguyên liệu theo mã nguyên liệu
        /// </summary>
        /// <param name="IngredientID"></param>
        /// <returns></returns>
        public async Task<(string, IngredientDTO)> GetIngredient(string IngredientID)
        {
            return await IngredientDAL.Ins.GetIngredient(IngredientID);
        }

        /// <summary>
        /// Tăng số lượng nguyên liệu
        /// </summary>
        /// <param name="IngredientID"> Mã nguyên liệu </param>
        /// <param name="Quantity"> Số lượng </param>
        /// <returns>
        ///     1: Thông báo
        ///     2: True nếu update thành công, False nếu update thất bại
        /// </returns>
        public async Task<(string, bool)> updateIngredientQuantity(string IngredientID, double Quantity, string UnitID)
        {
            (string labelGetIngredient, IngredientDTO ingredient) = await this.GetIngredient(IngredientID);

            // Nếu lấy được nguyên liệu thì update số lượng
            if (ingredient != null)
            {
                if (ingredient.MaDonVi == UnitID)
                    ingredient.SoLuong += Quantity;
                else
                {
                    if (ingredient.MaDonVi == "DV0001" || ingredient.MaDonVi == "DV0003")
                        ingredient.SoLuong += (Quantity / 1000);
                    else
                        ingredient.SoLuong += (Quantity * 1000);
                }

                (string labelUpdate, IngredientDTO ingredientUpdate) = await this.updateIngredient(ingredient);

                // Update thành công
                if (ingredientUpdate != null)
                    return ("Tăng số lượng nguyên liệu thành công", true);
                else
                    return (labelUpdate, false);
            }

            return ("Không tìm được nguyên liệu", false);
        }
    }
}
