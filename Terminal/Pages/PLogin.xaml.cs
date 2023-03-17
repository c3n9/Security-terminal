using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Логика взаимодействия для PLogin.xaml
    /// </summary>
    public partial class PLogin : Page
    {
        public PLogin()
        {
            InitializeComponent();
            Refresh();
            //var t = new Thread(() =>
            //{
            //    while (true)
            //    {
            //        System.Media.SystemSounds.Asterisk.Play();
            //    }
            //});
            //t.Start();
        }

        private void BLogin_Click(object sender, RoutedEventArgs e)
        {
            var employee = App.DB.Employee.FirstOrDefault(c => c.Code == TBCodeEmployee.Text);
            if (employee == null || employee.DepartmentId != 7)
            {
                MessageBox.Show("Неверный код");
                return;
            }
            App.LoggedEmployee = employee;
            NavigationService.Navigate(new PApprovalOfPasses(App.LoggedEmployee));
        }
        private void Refresh()
        {
            App.MainWindowInstance.BBack.Visibility = Visibility.Collapsed;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Refresh();
        }
    }
}
