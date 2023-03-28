using PN.DB.Interfaces;
using PN.DB.UOW;
using PN.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PizzaNinja
{
    /// <summary>
    /// Interaction logic for EditEmployees.xaml
    /// </summary>
    public partial class EditEmployees : Window
    {
        private IConnectionFactory conn;
        private UnitOfWork uow;
        public EditEmployees(Employee adminEmployee)
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            InitializeComponent();
            SaveButton.IsEnabled = false;
            NameBox.Focus();
        } 
        private async void EmployeeList_Initialized(object sender, EventArgs e)
        {
            foreach (Employee em in new ObservableCollection<Employee>(await Task.Run(() => uow.Employees.GetAllAsync().Result)))
            {
                EmployeeList.Items.Add(em);
            }
        }
        private async void RefreshList()
        {
            EmployeeList.Items.Clear();

            foreach (Employee em in new ObservableCollection<Employee>(await Task.Run(() => uow.Employees.GetAllAsync().Result)))
            {
                EmployeeList.Items.Add(em);
            }
        }
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if(NameBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a name.");
                mes.Show();
                ClearBoxes();
            }else if(UsernameBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a Username.");
                mes.Show();
                ClearBoxes();
            }
            else if (PasswordBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a password.");
                mes.Show();
                ClearBoxes();
            }
            else {
                try
                {
                    Employee employee = new Employee();
                    employee.Name = NameBox.Text;
                    employee.Username = NameBox.Text;
                    employee.Password = PasswordBox.Text;
                    employee.IsAdmin = bool.Parse(AdminBox.Text);

                    var output = await Task.Run(() => uow.Employees.AddAsync(employee));
                    ClearBoxes();
                    NameBox.Focus();
                    RefreshList();
                }
                catch (FormatException)
                {
                    Message mes = new Message("Invalid input.", "Must enter either 'True' or 'False' in Admin box.");
                    mes.Show();
                    ClearBoxes();
                }
            }
            

        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            Employee employee = EmployeeList.SelectedItem as Employee;

            if(employee != null)
            {
                NameBox.Text = employee.Name;
                UsernameBox.Text = employee.Username;
                PasswordBox.Text = employee.Password;
                AdminBox.Text = employee.IsAdmin.ToString();
            }

            SaveButton.IsEnabled = true;
            
        }
        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = EmployeeList.SelectedItem as Employee;

            if(employee != null) 
            {
                var output = await Task.Run(() => uow.Employees.DeleteAsync(employee.Id));
                ClearBoxes();
                NameBox.Focus();
                RefreshList();
            }
            
        }
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a name.");
                mes.Show();
                ClearBoxes();
            }
            else if (UsernameBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a Username.");
                mes.Show();
                ClearBoxes();
            }
            else if (PasswordBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a password.");
                mes.Show();
                ClearBoxes();
            }else
            {
                try
                {
                    Employee employee = EmployeeList.SelectedItem as Employee;

                    if (employee != null)
                    {
                        employee.Name = NameBox.Text;
                        employee.Password = PasswordBox.Text;
                        employee.IsAdmin = bool.Parse(AdminBox.Text);
                        employee.Username = UsernameBox.Text;

                        var output = await Task.Run(() => uow.Employees.UpdateAsync(employee));
                        ClearBoxes();
                        NameBox.Focus();
                        RefreshList();
                        SaveButton.IsEnabled = false;
                    }
                }
                catch (FormatException)
                {
                    Message mes = new Message("Invalid input.", "Must enter either 'True' or 'False' in Admin box.");
                    mes.Show();
                    ClearBoxes();
                }
            }      
        }
        private void ClearBoxes()
        {
            NameBox.Text = string.Empty;
            AdminBox.Text = string.Empty;
            UsernameBox.Text = string.Empty;
            PasswordBox.Text = string.Empty;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        
    }
}
