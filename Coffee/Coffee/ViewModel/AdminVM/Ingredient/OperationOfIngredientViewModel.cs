using Coffee.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Coffee.ViewModel.AdminVM.Ingredient
{
    public partial class IngredientViewModel: BaseViewModel
    {

        #region variable
        public string _IngredientName { get; set; }
        public string IngredientName
        { 
            get {  return _IngredientName; }
            set { _IngredientName = value; OnPropertyChanged(); }
        }

        public string _SelectedUnitName { get; set; }
        public string SelectedUnitName
        {
            get { return _SelectedUnitName; }
            set { _SelectedUnitName = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommand

        #endregion
    }
}
