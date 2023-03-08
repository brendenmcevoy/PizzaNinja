﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
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
using Caliburn.Micro;
using PN.DB.Interfaces;
using PN.DB.UOW;
using PN.Logic;

namespace PizzaNinja
{
    /// <summary>
    /// Interaction logic for EmployeeUI.xaml
    /// </summary>
    public partial class EmployeeUI : Window
    {
        private IConnectionFactory conn;
        private UnitOfWork uow;
        BindableCollection<Truck> trucks;
        public EmployeeUI()
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            InitializeComponent();
            trucks = new BindableCollection<Truck>(uow.Trucks.GetAllAsync().Result);
            //Load();
        }

        //public async void Load()
        //{
        //    trucks.Clear();
        //    foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
        //    {
        //        trucks.Add(t);
        //    }
        //}

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void MaxButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Maximized;
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState |= WindowState.Minimized;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}