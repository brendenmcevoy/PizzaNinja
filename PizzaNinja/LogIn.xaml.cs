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
using System.Windows.Media.Converters;

namespace PizzaNinja
{
    public partial class LogIn : Window
    {
        private IConnectionFactory conn; // Connects to DB
        private UnitOfWork uow; // Contains Repos to manipulate and retrieve data from DB
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

            var emp = await Task.Run(() => uow.Employees.GetEmployeeByUsernameAsync(username)); // Get Employee

            var userKey =  await Task.Run(() => uow.Employees.GetUsernameAsync(username).Result); // Get username
            var passwordKey = await Task.Run(() => uow.Employees.GetPasswordAsync(emp.Id).Result); // Get password

            if(username == userKey && password == passwordKey) // Checks to see if username and password match 
            {
                Employee employee = await Task.Run(() => uow.Employees.GetEmployeeByUsernameAsync(username).Result); // if they match retreive the user data

                if (employee.IsAdmin) // if user is an Admin, load AdminUI
                {
                    AdminUI adminUI = new AdminUI(employee); 
                    adminUI.Show();
                    this.Close();
                }
                else // if user is not an Admin, load EmployeeUI
                {
                    EmployeeUI employeeUI = new EmployeeUI(employee);
                    employeeUI.Show();
                    this.Close();
                }                
            }
            else // if username and password do not match, send error message
            {
                string mes = "Username and Password did not match.";
                Message message = new Message(mes);
                message.Show();
            }
        }
        private void PasswordBox_GotFocus(object sender, RoutedEventArgs e) // removes placeholder text when clicked or tabbed to
        {           
            PasswordBox.Password = string.Empty;
        }
        private void UsernameBox_GotFocus(object sender, RoutedEventArgs e)  // removes placeholder text when clicked or tabbed to
        {
            UsernameBox.Text= string.Empty;
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
