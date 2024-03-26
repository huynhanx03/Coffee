using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Services;
using Coffee.Utils;
using Coffee.Views.Admin.IngredientPage;
using Coffee.Views.MessageBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Ingredient
{
    public partial class IngredientViewModel : BaseViewModel
    {
        #region variable
        public Grid MaskName { get; set; }

        private ObservableCollection<IngredientDTO> _IngredientList;

        public ObservableCollection<IngredientDTO> IngredientList
        {
            get { return _IngredientList; }
            set { _IngredientList = value; OnPropertyChanged(); }
        }

        private List<IngredientDTO> __IngredientList;

        private ObservableCollection<UnitDTO> _UnitList;

        public ObservableCollection<UnitDTO> UnitList
        {
            get { return _UnitList; }
            set { _UnitList = value; OnPropertyChanged(); }
        }
        
        private IngredientDTO _SelectedIngredient;

        public IngredientDTO SelectedIngredient
        {
            get { return _SelectedIngredient; }
            set { _SelectedIngredient = value; OnPropertyChanged(); }
        }
        
       
        #endregion

        #region ICommand
        public ICommand loadShadowMaskIC { get; set; }
        public ICommand openWindowAddIngredientIC { get; set; }
        public ICommand loadIngredientListIC { get; set; }
        public ICommand loadUnitListIC { get; set; }
        public ICommand addIngredientToImportIC { get; set; }
        public ICommand openBillImportWindowIC { get; set; }
        public ICommand searchDetailImportIC { get; set; }
        #endregion

        public IngredientViewModel()
        {
            loadShadowMaskIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            loadIngredientListIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadIngredientList();
            });

            loadUnitListIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                loadUnitList();
            });

            openWindowAddIngredientIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                
            });

            openBillImportWindowIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                openBillImportWindow();
            });

            #region import
            confirmImportIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                confirmImport(p);
            });

            closeBillImportWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            searchDetailImportIC = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                searchDetailImport(p.Text);
            });

            #endregion
        }

        /// <summary>
        /// Load list nguyên liệu
        /// </summary>
        public async void loadIngredientList()
        {
            (string label, List<IngredientDTO> ingredients) = await IngredientService.Ins.getListIngredient();

            if (ingredients != null)
            {
                IngredientList = new ObservableCollection<IngredientDTO>(ingredients);
                __IngredientList = new List<IngredientDTO>(ingredients);
            }
        }

        /// <summary>
        /// load list đơn vị
        /// </summary>
        public async void loadUnitList()
        {
            (string label, List<UnitDTO> units) = await UnitService.Ins.getAllUnit();

            if (units != null)
                UnitList = new ObservableCollection<UnitDTO>(units);
        }

        /// <summary>
        /// Thêm nguyên liệu vào phiếu nhập kho
        /// </summary>
        public void addIngredientToImport()
        {
            // Kiểm tra nguyên liệu đã tồn tại chưa
            // Đã tồn tại thì số lượng +1
            foreach (var detailImport in DetailImportList)
            {
                if (detailImport.MaNguyenLieu == SelectedIngredient.MaNguyenLieu)
                {
                    detailImport.SoLuong += 1;
                    DetailImportList = new ObservableCollection<DetailImportDTO>(DetailImportList);
                    return;
                }
            }

            // Chưa tồn tại thì thêm vào
            DetailImportList.Add(new DetailImportDTO
            {
                MaNguyenLieu = SelectedIngredient.MaNguyenLieu,
                TenNguyenLieu = SelectedIngredient.TenNguyenLieu,
                MaDonVi = SelectedIngredient.MaDonVi,
                TenDonVi = SelectedIngredient.TenDonVi,
                SoLuong = 1,
                Gia = 1
            });
        }

        private void openBillImportWindow()
        {
            // Chưa có sản phẩm để nhập thì không mở
                if (DetailImportList.Count <= 0)
            {
                MessageBoxCF ms = new MessageBoxCF("Không có sản phẩm để nhập vào kho", MessageType.Error, MessageButtons.OK);
                ms.ShowDialog();
                return;
            }

            MaskName.Visibility = Visibility.Visible;

            // Cập nhật dữ liệu
            EmployeeName = Memory.user.HoTen;
            InvoiceDate = DateTime.Now.ToString("dd/MM/yyyy");

            InvoiceValue = 0;
            foreach (var detailImport in DetailImportList)
            {
                InvoiceValue += detailImport.Gia;
            }

            BillImportWindow w = new BillImportWindow();
            w.ShowDialog();

            MaskName.Visibility = Visibility.Collapsed;
        }
    }
}
