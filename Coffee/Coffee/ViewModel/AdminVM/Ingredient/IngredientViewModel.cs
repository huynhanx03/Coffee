using Coffee.DALs;
using Coffee.DTOs;
using Coffee.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion

        #region ICommand
        public ICommand loadShadowMaskIC { get; set; }
        public ICommand openWindowAddIngredientIC { get; set; }
        public ICommand loadIngredientListIC { get; set; }
        public ICommand loadUnitListIC { get; set; }
        //public ICommand searchEmployeeIC { get; set; }
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

            #region import
            importIC = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                import();
            });

            #endregion
        }

        public async void loadIngredientList()
        {
            (string label, List<IngredientDTO> ingredients) = await IngredientService.Ins.getListIngredient();

            if (ingredients != null)
            {
                IngredientList = new ObservableCollection<IngredientDTO>(ingredients);
                __IngredientList = new List<IngredientDTO>(ingredients);
            }
        }

        public async void loadUnitList()
        {
            (string label, List<UnitDTO> units) = await UnitService.Ins.getAllUnit();

            if (units != null)
                UnitList = new ObservableCollection<UnitDTO>(units);
        }
    }
}
