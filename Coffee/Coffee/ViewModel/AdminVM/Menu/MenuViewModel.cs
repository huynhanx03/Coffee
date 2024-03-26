using CloudinaryDotNet.Actions;
using Coffee.DTOs;
using Coffee.Services;
using Coffee.Views.Admin.EmployeePage;
using Coffee.Views.Admin.MenuPage;
using Coffee.Views.MessageBox;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Menu
{
    public partial class MenuViewModel: BaseViewModel
    {
        #region variable
        public Grid MaskName { get; set; }

        private ObservableCollection<ProductDTO> _ProductList;

        public ObservableCollection<ProductDTO> ProductList
        {
            get { return _ProductList; }
            set { _ProductList = value; OnPropertyChanged(); }
        }

        private List<ProductDTO> __ProductList;
        private ProductDTO _SelectedProduct;
        public ProductDTO SelectedProduct
        {
            get { return _SelectedProduct; }
            set { _SelectedProduct = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand loadShadowMaskIC { get; set; }
        public ICommand openWindowAddProductIC { get; set; }
        public ICommand loadProductListIC { get; set; }
        public ICommand searchItemIC { get; set; }
        public ICommand uploadImageIC { get; set; }
        #endregion

        public MenuViewModel()
        {
            loadShadowMaskIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                MaskName = p;
            });

            loadProductListIC = new RelayCommand<Grid>((p) => { return true; }, (p) =>
            {
                loadProductList();
            });

            openWindowAddProductIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                openWindowAddProduct();
            });

            searchItemIC = new RelayCommand<TextBox>(null, (p) =>
            {
                if (p.Text != null)
                {
                    if (__ProductList != null)
                        ProductList = new ObservableCollection<ProductDTO>(__ProductList.FindAll(x => x.TenSanPham.ToLower().Contains(p.Text.ToLower())));
                }
            });

            #region operation
            confirmOperationEmployeeIC = new RelayCommand<object>((p) =>
            {
                return true;
            },
            (p) =>
            {
                confirmOperationEmployee();
            });

            closeOperationEmployeeWindowIC = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                p.Close();
            });

            uploadImageIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Image Files|*.jpg;*.png;*.jpeg;*.webp;*.gif|All Files|*.*";
                if (openFileDialog.ShowDialog() == true)
                {

                    Image = openFileDialog.FileName;
                    if (Image != null)
                    {
                        // Image was uploaded successfully.                        
                    }
                    else
                    {
                        MessageBoxCF ms = new MessageBoxCF("Tải ảnh lên thất bại", MessageType.Error, MessageButtons.OK);
                        ms.ShowDialog();
                    }
                }
            });
            #endregion
        }

        /// <summary>
        /// Load danh sách sản phẩm
        /// </summary>
        private async void loadProductList()
        {
            //(string label, List<ProductDTO> pr) = await ProductService.Ins.get();

            //if (employees != null)
            //{
            //    EmployeeList = new ObservableCollection<EmployeeDTO>(employees);
            //    __ProductList = new List<ProductDTO>(employees);
            //}
        }

        /// <summary>
        /// Mở cửa số sửa sản phẩm
        /// </summary>
        public void openWindowEditEmployee()
        {
            MaskName.Visibility = Visibility.Visible;
            loadProductType();
            loadProductSizeDetail();
            OperationEmployeeWindow w = new OperationEmployeeWindow();
            TypeOperation = 2; // Edit employee
            loadProduct(SelectedProduct);
            w.ShowDialog();
            MaskName.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// Load dữ liệu sản phẩm click
        /// </summary>
        /// <param name="product"> Sản phẩm </param>
        private void loadProduct(ProductDTO product)
        {
            
        }

        /// <summary>
        /// Reset dữ liệu sản phâm trên cửa sổ thao tác của sản phẩm
        /// </summary>
        private void resetProduct()
        {
            ProductName = "";
            ProductType = "";
            Quantity = 0;
            Price = 0;
            loadProductSizeDetail();
            ProductRecipeList = new ObservableCollection<ProductRecipeDTO>();
            Image = "";
        }

        /// <summary>
        /// Xoá sản phẩm
        /// </summary>
        public async void deleteProduct()
        {
            MessageBoxCF ms = new MessageBoxCF("Xác nhận xoá sản phẩm?", MessageType.Error, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                (string label, bool isDeleteEmployee) = await ProductService.Ins.DeleteProduct(SelectedProduct);

                if (isDeleteEmployee)
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Accept, MessageButtons.OK);
                    loadProductList();
                    msn.ShowDialog();
                }
                else
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Error, MessageButtons.OK);
                    msn.ShowDialog();
                }
            }
        }

        public void openWindowAddProduct()
        {
            MaskName.Visibility = Visibility.Visible;
            resetProduct();
            loadProductType();
            loadProductSizeDetail();
            loadIngredientList();
            loadUnitList();
            OperationProductWindow w = new OperationProductWindow();
            TypeOperation = 1; // Add product
            w.ShowDialog();
            MaskName.Visibility = Visibility.Collapsed;
        }
    }
}
