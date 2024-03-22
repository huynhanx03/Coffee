using Coffee.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Ingredient
{
    public partial class IngredientViewModel: BaseViewModel
    {
        #region variable
        private ObservableCollection<DetailImportDTO> _DetailImportList;

        public ObservableCollection<DetailImportDTO> DetailImportList
        {
            get { return _DetailImportList; }
            set { _DetailImportList = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand
        public ICommand importIC { get; set; }

        #endregion

        private void import()
        {
            MessageBox.Show(DetailImportList[0].TenNguyenLieu);
        }
    }
}
