using Coffee.DTOs;
using Coffee.Services;
using Coffee.Utils;
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
                        PositionDTO cd = new PositionDTO
                        {
                            MaChucVu = "CD0003",
                            TenChucVu = "Nhân viên phục vụ",
                            Luong = 3000000

                        };

                        await context.Client.SetTaskAsync("ChucDanh/" + cd.MaChucVu, cd);
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
    }
}
