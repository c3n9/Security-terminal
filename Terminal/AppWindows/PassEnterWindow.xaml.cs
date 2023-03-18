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
using System.Windows.Shapes;
using Terminal.Model;

namespace Terminal.AppWindows
{
    /// <summary>
    /// Логика взаимодействия для PassEnterWindow.xaml
    /// </summary>
    public partial class PassEnterWindow : Window
    {
        Pass contextPass;
        public PassEnterWindow(Pass pass)
        {
            InitializeComponent();
            contextPass = pass;
            DataContext = contextPass;
        }

        private void BAgree_Click(object sender, RoutedEventArgs e)
        {
            var passLog = new PassLog() { Pass = contextPass, IsEnter = true, DateTime = DateTime.Now, Employee = App.LoggedEmployee};
            App.DB.PassLog.Add(passLog);
            App.DB.SaveChanges();
            System.Media.SystemSounds.Beep.Play();
            this.DialogResult = true;
        }

        private void BCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;

        }
    }
}
