using Coffee.DTOs;
using Coffee.Services;
using Coffee.Utils;
using Coffee.Utils.Helper;
using Coffee.Views.MessageBox;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
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
        public List<MyObject> myObjects = new List<MyObject>();

        public MainWindow()
        {
            InitializeComponent();
            myObjects = new List<MyObject>
            {
                new MyObject { Row = 0, Column = 0, Content = "Button 1" },
                new MyObject { Row = 0, Column = 1, Content = "Button 2" },
                new MyObject { Row = 1, Column = 0, Content = "Button 3" },
                new MyObject { Row = 1, Column = 1, Content = "Button 4" },
                new MyObject { Row = 4, Column = 1, Content = "Button 5" },
            };
            itemsource.ItemsSource = myObjects;
            this.DataContext = this;
        }

        public class MyObject
        {
            public int Row { get; set; }
            public int Column { get; set; }
            public string Content { get; set; }
        }

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer.ScrollToVerticalOffset(ScrollViewer.VerticalOffset - e.Delta);
            e.Handled = true;   
        }
    }
}
