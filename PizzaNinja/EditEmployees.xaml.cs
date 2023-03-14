using PN.DB.Interfaces;
using PN.DB.UOW;
using PN.Logic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private Employee _adminEmployee;
        private ObservableCollection<Employee> employees;
        public EditEmployees(Employee adminEmployee)
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            _adminEmployee = adminEmployee;
            InitializeComponent();
            employees = new ObservableCollection<Employee>();
            EmployeeList.ItemsSource = employees;
        } 
        private async void EmployeeList_Initialized(object sender, EventArgs e)
        {
            foreach (Employee em in new ObservableCollection<Employee>(await Task.Run(() => uow.Employees.GetAllAsync().Result)))
            {
                employees.Add(em);
            }
        }
        private async void RefreshList()
        {
            employees.Clear();

            foreach (Employee em in new ObservableCollection<Employee>(await Task.Run(() => uow.Employees.GetAllAsync().Result)))
            {
                employees.Add(em);
            }
        }
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = new Employee(int.Parse(IdBox.Text),NameBox.Text,bool.Parse(AdminBox.Text),UsernameBox.Text,PasswordBox.Text);
            var output = await Task.Run(() => uow.Employees.AddAsync(employee));
            RefreshList();
        }
        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = EmployeeList.SelectedItem as Employee;
            var output = await Task.Run(() => uow.Employees.DeleteAsync(employee.Id));
            RefreshList();
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

        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
    }
}
