﻿using Coffee.ViewModel.AdminVM.Employee;
using Coffee.ViewModel.AdminVM.Ingredient;
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

namespace Coffee.Views.Admin.IngredientPage
{
    /// <summary>
    /// Interaction logic for MainIngredientPage.xaml
    /// </summary>
    public partial class MainIngredientPage : Page
    {
        public MainIngredientPage()
        {
            InitializeComponent();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //(DataContext as EmployeeViewModel).openWindowEditEmployee();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //(DataContext as EmployeeViewModel).deleteEmployee();
        }
        
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            (DataContext as IngredientViewModel).addIngredientToImport();
        }
    }
}
