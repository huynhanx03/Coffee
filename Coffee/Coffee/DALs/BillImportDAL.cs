using Coffee.DTOs;
using Coffee.Utils;
using FireSharp.Response;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.DALs
{
    public class BillImportDAL
    {
        private static BillImportDAL _ins;
        public static BillImportDAL Ins
        {
            get
            {
                if (_ins == null)
                {
                    _ins = new BillImportDAL();
                }
                return _ins;
            }
            private set => _ins = value;
        }

        /// <summary>
        /// Tạo phiếu nhập kho mới
        /// </summary>
        /// <param name="import">Phiếu nhập kho</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. True khi tạo thành công
        /// </returns>
        public async Task<(string, bool)> createBillImport(ImportDTO import)
        {
            try
            {
                using (var context = new Firebase())
                {
                    await context.Client.SetTaskAsync("PhieuNhapKho/" + import.MaPhieuNhapKho, import);

                    return ("Thêm phiếu nhập kho thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }

        /// <summary>
        ///     Lấy mã phiêu nhập kho lớn nhất
        /// </summary>
        /// <returns>
        ///     Mã phiếu nhập kho lớn nhất
        /// </returns>
        public async Task<string> getMaxMaPhieuNhapKho()
        {
            try
            {
                using (var context = new Firebase())
                {
                    FirebaseResponse response = await context.Client.GetTaskAsync("PhieuNhapKho");

                    if (response.Body != null && response.Body != "null")
                    {
                        Dictionary<string, ImportDTO> data = response.ResultAs<Dictionary<string, ImportDTO>>();

                        string MaxMaPhieuNhapKho = data.Values.Select(i => i.MaPhieuNhapKho).Max();

                        return MaxMaPhieuNhapKho;
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
        /// Tạo chi tiết phiếu nhập kho mới
        /// </summary>
        /// <param name="detailImportList"> List chi tiết phiếu nhập kho</param>
        /// <returns>
        ///     1. Thông báo
        ///     2. True khi tạo thành công
        /// </returns>
        public async Task<(string, bool)> createDetailBillImport(ObservableCollection<DetailImportDTO> detailImportList)
        {
            try
            {
                using (var context = new Firebase())
                {
                    foreach (var detailImport in detailImportList)
                    {
                        await context.Client.SetTaskAsync("PhieuNhapKho/" + detailImport.MaPhieuNhapKho, detailImport);
                    }

                    return ("Thêm phiếu nhập kho thành công", true);
                }
            }
            catch (Exception ex)
            {
                return (ex.Message, false);
            }
        }
    }
}
