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
using Terminal.AppWindows;
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
            statuses.Insert(0, new PassStatus() { Name = "Показать всё" });
            CBStatusForFilter.ItemsSource = statuses.ToList();
            CBStatusForFilter.SelectedIndex = 0;
            
            var departments = App.DB.Department.ToList();
            departments.Insert(0, new Department() { Name = "Показать всё" });
            CBDepartment.ItemsSource = departments.ToList();
            CBDepartment.SelectedIndex = 0;
            Refresh();
        }
        private void Refresh()
        {
            App.MainWindowInstance.BBack.Visibility = Visibility.Visible;
            var filtred = App.DB.Pass.Where(p => p.PassStatusId == 2).ToList();
            var selectedDepartment = CBDepartment.SelectedItem as Department;
            var selectedStatus = CBStatusForFilter.SelectedItem as PassStatus;
            var searchText = TBSearch.Text.ToLower();
            if (selectedDepartment != null && selectedDepartment.Id !=0)
                filtred = filtred.Where(f => f.Employee.DepartmentId == selectedDepartment.Id).ToList();
            if (selectedStatus != null && selectedStatus.Id != 0)
                filtred = filtred.Where(f => f.PassStatusId == selectedStatus.Id).ToList();
            if (DPDateForFIlter.SelectedDate != null)
                filtred = filtred.Where(f => f.DateStart == DPDateForFIlter.SelectedDate.Value).ToList();
            if (string.IsNullOrWhiteSpace(searchText) == false)
                filtred = filtred.Where(f => f.FullName.ToLower().Contains(searchText) || f.Passport.Contains(searchText)).ToList();

            DGGuests.ItemsSource = filtred.ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
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

        private void BPassEnter_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = DGGuests.SelectedItem as Pass;
            if (selectedGuest == null)
            {
                MessageBox.Show("Выберите гостя");
                return;
            }
            new PassEnterWindow(selectedGuest).ShowDialog();
        }

        private void BPassExit_Click(object sender, RoutedEventArgs e)
        {
            var selectedGuest = DGGuests.SelectedItem as Pass;
            if(selectedGuest == null)
            {
                MessageBox.Show("Выберите гостя");
                return;
            }
            new PassExitWindow(selectedGuest).ShowDialog();
        }

        private void DPDateForFIlter_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
    }
}
