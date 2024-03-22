using Coffee.DTOs;
using Coffee.Services;
using Coffee.Utils;
using Coffee.Utils.Helper;
using Coffee.Views.MessageBox;
using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Coffee
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var context = new Firebase())
                {
                    if (context.Client != null)
                    {
                        IngredientDTO cd = new IngredientDTO
                        {
                            MaNguyenLieu = "NL0001",
                            TenNguyenLieu = "Muối",
                            MaDonVi = "DV0001"
                        };

                        await context.Client.SetTaskAsync("NguyenLieu/" + cd.MaNguyenLieu, cd);
                    }
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            (string label, List<PositionDTO> list) = await PositionService.Ins.getAllPosition();

            MessageBox.Show(label);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MessageBoxCF ms = new MessageBoxCF("test", MessageType.Accept, MessageButtons.YesNo);
            ms.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            MessageBoxCF ms = new MessageBoxCF("test", MessageType.Waitting, MessageButtons.OK);
            ms.ShowDialog();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            MessageBoxCF ms = new MessageBoxCF("test", MessageType.Error, MessageButtons.OK);
            ms.ShowDialog();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {

        }
    }
}
