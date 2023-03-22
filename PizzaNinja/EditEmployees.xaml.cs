﻿using PN.DB.Interfaces;
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
        private ObservableCollection<Employee> employees;
        private bool edit;
        public EditEmployees(Employee adminEmployee)
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            InitializeComponent();
            employees = new ObservableCollection<Employee>();
            EmployeeList.ItemsSource = employees;
            edit = false;
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
            if (edit == true)
            {
                Employee employee = EmployeeList.SelectedItem as Employee;

                if(employee != null)
                {
                    employee.Name = NameBox.Text;
                    employee.Username = NameBox.Text;
                    employee.Password = PasswordBox.Text;
                    employee.IsAdmin = bool.Parse(AdminBox.Text);

                    var output = await Task.Run(() => uow.Employees.UpdateAsync(employee));
                    RefreshList();
                    edit = false;
                }              
            }
            else 
            {
                Employee employee = new Employee();
                employee.Name = NameBox.Text;
                employee.Username = NameBox.Text;
                employee.Password = PasswordBox.Text;
                employee.IsAdmin = bool.Parse(AdminBox.Text);

                var output = await Task.Run(() => uow.Employees.AddAsync(employee));
                RefreshList();
            }           
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();
            var employee = EmployeeList.SelectedItem as Employee;

            if(employee != null) 
            {
                NameBox.Text = employee.Name;
                UsernameBox.Text = employee.Username;
                PasswordBox.Text = employee.Password;
                AdminBox.Text = employee.IsAdmin.ToString();
                edit = true;
            }
            
        }
        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Employee employee = EmployeeList.SelectedItem as Employee;
            if(employee != null) 
            {
                var output = await Task.Run(() => uow.Employees.DeleteAsync(employee.Id));
                RefreshList();
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
