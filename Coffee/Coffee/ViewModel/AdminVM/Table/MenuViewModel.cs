using Coffee.DTOs;
using Coffee.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Table
{
    public partial class MainTableViewModel: BaseViewModel
    {
        #region variable
        private ObservableCollection<ProductDTO> _ProductList;
        public ObservableCollection<ProductDTO> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; OnPropertyChanged(); }
        }

        private List<ProductDTO> __ProductList;
        private List<ProductDTO> __ProductSearchList;

        private ProductDTO _SelectedProduct;
        public ProductDTO SelectedProduct
        {
            get { return _SelectedProduct; }
            set { _SelectedProduct = value; OnPropertyChanged(); }
        }

        private ObservableCollection<ProductTypeDTO> _ProductTypeList;
        public ObservableCollection<ProductTypeDTO> ProductTypeList
        {
            get { return _ProductTypeList; }
            set { _ProductTypeList = value; OnPropertyChanged(); }
        }

        private ProductTypeDTO _SelectedProductType;
        public ProductTypeDTO SelectedProductType
        {
            get { return _SelectedProductType; }
            set { _SelectedProductType = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommend
        public ICommand loadMenuListIC { get; set; }
        public ICommand selectedTypeProductIC { get; set; }
        public ICommand loadProductTypeListIC { get; set; }
        public ICommand searchProductIC { get; set; }

        #endregion

        private async void loadMenuList()
        {
            (string label, List<ProductDTO> listProduct) = await ProductService.Ins.getListProduct();

            if (listProduct != null)
            {
                ProductList = new ObservableCollection<ProductDTO>(listProduct);
                __ProductList = new List<ProductDTO>(listProduct);
                __ProductSearchList = new List<ProductDTO>(listProduct);
            }
        }

        private void selectedTypeProduct()
        {
            if (SelectedProductType != null) 
                if (SelectedProductType.MaLoaiSanPham == "LS0000")
                    ProductList = new ObservableCollection<ProductDTO>(__ProductSearchList);
                else
                    ProductList = new ObservableCollection<ProductDTO>(__ProductSearchList.FindAll(p => p.MaLoaiSanPham == SelectedProductType.MaLoaiSanPham));
        }

        private async void loadProductTypeList()
        {
            (string label, List<ProductTypeDTO> listProductType) = await ProductTypeService.Ins.getAllProductType();

            if (listProductType != null)
            {
                ProductTypeList = new ObservableCollection<ProductTypeDTO>(listProductType);
                ProductTypeList.Insert(0, new ProductTypeDTO
                {
                    MaLoaiSanPham = "LS0000",
                    LoaiSanPham = "Toàn bộ",
                });
            }
        }
    }
}
