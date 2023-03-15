﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
using Terminal.Model;

namespace Terminal.Pages
{
    /// <summary>
    /// Логика взаимодействия для PApprovalOfPasses.xaml
    /// </summary>
    public partial class PApprovalOfPasses : Page
    {
        Employee contextEmployee;
        public PApprovalOfPasses(Employee employee)
        {
            InitializeComponent();
            contextEmployee = employee;
            CBStatus.ItemsSource = App.DB.PassStatus.ToList();
            CBStatusForFilter.ItemsSource = App.DB.PassStatus.ToList();
            CBDepartment.ItemsSource = App.DB.Department.ToList();
            Refresh();
        }

        private void BSave_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = DGGuests.SelectedItem as Pass;
            if (selectedGuest == null) 
            {
                MessageBox.Show("Выберите гостя");
                return;
            }
            selectedGuest.PassStatus = CBStatus.SelectedItem as PassStatus;
            App.DB.SaveChanges();
            this.Refresh();
        }
        private void Refresh()
        {
            int index = contextEmployee.Id;
            DGGuests.ItemsSource = App.DB.Pass.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void BFilter_Click(object sender, RoutedEventArgs e)
        {
            var selectedDepartment = CBDepartment.SelectedItem as Department;
            var selectedStatus = CBStatusForFilter.SelectedItem as PassStatus;
            if (selectedDepartment != null && selectedStatus == null)
            {
                DGGuests.ItemsSource = App.DB.Pass.Where(g => g.Employee.DepartmentId == selectedDepartment.Id).ToList();
            }
            else if (selectedStatus !=null && selectedDepartment == null)
            {
                DGGuests.ItemsSource = App.DB.Pass.Where(g => g.PassStatus.Id == selectedStatus.Id).ToList();
            }
            else if (selectedDepartment != null && selectedStatus != null)
            {
                DGGuests.ItemsSource = App.DB.Pass.Where(g => g.Employee.DepartmentId == selectedDepartment.Id && g.PassStatus.Id == selectedStatus.Id).ToList();
            }
        }
    }
}
