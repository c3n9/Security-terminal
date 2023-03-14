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
        int indexEmployee;
        public PApprovalOfPasses(int employee)
        {
            InitializeComponent();
            Refresh();
            CBStatus.ItemsSource = App.DB.PassStatus.ToList();
            indexEmployee = employee;
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
            DGGuests.ItemsSource = App.DB.Pass.Where(p => p.EmployeeId == indexEmployee).ToList();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
