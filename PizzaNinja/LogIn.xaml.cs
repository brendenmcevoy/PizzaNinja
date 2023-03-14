using PN.DB.Interfaces;
using PN.DB.UOW;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using PN.Logic;

namespace PizzaNinja
{
    public partial class LogIn : Window
    {
        private IConnectionFactory conn;
        private UnitOfWork uow;
        public LogIn()
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            InitializeComponent();
        }
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameBox.Text;
            string password = PasswordBox.Password;
            var userKey =  await Task.Run(() => uow.Employees.GetUsernameAsync(username).Result);
            var passwordKey = await Task.Run(() => uow.Employees.GetPasswordAsync(password).Result);

            if(username == userKey && password == passwordKey)
            {
                Employee employee = await Task.Run(() => uow.Employees.GetEmployeeByUsernameAsync(username).Result);

                if (employee.IsAdmin) 
                {
                    AdminUI adminUI = new AdminUI(employee); 
                    adminUI.Show();
                    this.Close();
                }
                else 
                {
                    EmployeeUI employeeUI = new EmployeeUI(employee);
                    employeeUI.Show();
                    this.Close();
                }                
            }
            else
            {
                MessageBox.Show("Username and Password did not match");
            }
        }
        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri) { UseShellExecute = true});
            e.Handled= true;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
       
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
