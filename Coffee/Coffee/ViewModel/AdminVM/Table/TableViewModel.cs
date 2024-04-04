using Coffee.DTOs;
using Coffee.Services;
using Coffee.Utils;
using Coffee.Views.MessageBox;
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
        private ObservableCollection<TableDTO> _TableList;
        public ObservableCollection<TableDTO> TableList
        {
            get { return _TableList; }
            set { _TableList = value; OnPropertyChanged(); }
        }

        private TableDTO _SelectedTable;
        public TableDTO SelectedTable
        {
            get { return _SelectedTable; }
            set { _SelectedTable = value; OnPropertyChanged(); }
        }

        #endregion

        #region ICommend
        public ICommand loadTableListIC { get; set; }
        public ICommand openEditTableIC { get; set; }
        public ICommand openDeleteTableIC { get; set; }
        public ICommand clickTableIC { get; set; }

        #endregion
        
        /// <summary>
        /// load danh sách bàn
        /// </summary>
        private async void loadTableList()
        {
            (string label, List<TableDTO> tableList) = await TableService.Ins.getListTable();

            if (tableList != null)
            {
                TableList = new ObservableCollection<TableDTO>(tableList);
            }
        }

        /// <summary>
        /// Xoá bàn
        /// </summary>
        private async void deleteTable(TableDTO table)
        {
            MessageBoxCF ms = new MessageBoxCF("Xác nhận xoá bàn?", MessageType.Error, MessageButtons.YesNo);

            if (ms.ShowDialog() == true)
            {
                (string label, bool isDeleteTable) = await TableService.Ins.DeleteTable(table.MaBan);

                if (isDeleteTable)
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Accept, MessageButtons.OK);
                    loadTableList();
                    msn.ShowDialog();
                }
                else
                {
                    MessageBoxCF msn = new MessageBoxCF(label, MessageType.Error, MessageButtons.OK);
                    msn.ShowDialog();
                }
            }
        }

        private void clickTable()
        {
            // Load thông tin bàn
            btnMenu.IsChecked = true;
            TableNameSale = SelectedTable.TenBan;
            currentTable = SelectedTable;
        }
    }
}
