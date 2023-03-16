using System;
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
            var statuses = App.DB.PassStatus.ToList();
            statuses.Insert(0, new PassStatus() { Name = "All" });
            CBStatus.ItemsSource = statuses.ToList();
            CBStatus.SelectedIndex = 0;
            CBStatusForFilter.ItemsSource = App.DB.PassStatus.ToList();
            CBDepartment.ItemsSource = App.DB.Department.ToList();


            if (contextEmployee.DepartmentId == 7)
            {
                CBStatus.Visibility = Visibility.Collapsed;
                BSave.Visibility = Visibility.Collapsed;
                CBStatusForFilter.Visibility = Visibility.Collapsed;
                TBSearch.Visibility = Visibility.Visible;
            }
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
            var filtred = App.DB.Pass.ToList();
            var selectedDepartment = CBDepartment.SelectedItem as Department;
            var selectedStatus = CBStatusForFilter.SelectedItem as PassStatus;
            var searchText = TBSearch.Text.ToLower();

            if (contextEmployee.DepartmentId == 7)
                filtred = filtred.Where(g => g.PassStatusId == 2).ToList();
            if (selectedDepartment != null)
                filtred = filtred.Where(f => f.Employee.DepartmentId == selectedDepartment.Id).ToList();
            if (selectedStatus != null && selectedStatus.Id != 0)
                filtred = filtred.Where(f => f.PassStatusId == selectedStatus.Id).ToList();
            if (string.IsNullOrWhiteSpace(searchText) == false)
            {
                filtred = filtred.Where(f => f.FullName.ToLower().Contains(searchText) || f.Passport.Contains(searchText)).ToList();
            }

            DGGuests.ItemsSource = filtred.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }

        private void BFilter_Click(object sender, RoutedEventArgs e)
        {
            Refresh();

            //var selectedDepartment = CBDepartment.SelectedItem as Department;
            //var selectedStatus = CBStatusForFilter.SelectedItem as PassStatus;
            //if (selectedDepartment != null && selectedStatus == null)
            //{
            //    if (contextEmployee.DepartmentId == 7)
            //        DGGuests.ItemsSource = App.DB.Pass.Where(g => g.Employee.DepartmentId == selectedDepartment.Id && g.PassStatus.Id == 2).ToList();
            //    else
            //        DGGuests.ItemsSource = App.DB.Pass.Where(g => g.Employee.DepartmentId == selectedDepartment.Id).ToList();

            //}
            //else if (selectedStatus != null && selectedDepartment == null)
            //{
            //    DGGuests.ItemsSource = App.DB.Pass.Where(g => g.PassStatus.Id == selectedStatus.Id).ToList();
            //}
            //else if (selectedDepartment != null && selectedStatus != null)
            //{
            //    DGGuests.ItemsSource = App.DB.Pass.Where(g => g.Employee.DepartmentId == selectedDepartment.Id && g.PassStatus.Id == selectedStatus.Id).ToList();
            //}
        }

        private void CBDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void CBStatusForFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }
    }
}
