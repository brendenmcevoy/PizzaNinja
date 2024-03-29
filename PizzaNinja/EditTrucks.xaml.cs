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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PizzaNinja
{
    /// <summary>
    /// Interaction logic for EditTrucks.xaml
    /// </summary>
    public partial class EditTrucks : Window
    {
        private IConnectionFactory conn;
        private UnitOfWork uow;
        public EditTrucks()
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            InitializeComponent();
            NameBox.Focus();
            SaveButton.IsEnabled = false;
        }
        private async void TruckList_Initialized(object sender, EventArgs e) // populate DataGrid with all Trucks in the DB
        {
            foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
            {
                TruckList.Items.Add(t);
            }
        }
        private async void RefreshList() // updates trucks in the DataGrid
        {
            TruckList.Items.Clear();

            foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
            {
                TruckList.Items.Add(t);
            }
        }
        private async void AddButton_Click(object sender, RoutedEventArgs e) //Adds a truck to the DB using text from the textbox
        {
            if (NameBox.Text == string.Empty) // Check to see if textbox isn't empty, load an error message if its empty
            {
                Message mes = new Message("Invalid input.", "Must enter a name.");
                mes.Show();
                NameBox.Text = string.Empty;
            }
            else
            {
                Truck truck = new Truck();
                truck.Name = NameBox.Text;
                await Task.Run(() => uow.Trucks.AddAsync(truck));
                RefreshList();
                NameBox.Text = string.Empty;  // clears the textbox
                NameBox.Focus();
            }      
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)  // Fills each textbox w/ specific data based on the selected truck (selected from DataGrid)
        {
            NameBox.Text = string.Empty;
            Truck truck = TruckList.SelectedItem as Truck;

            if (truck != null)  // Makes sure there is actually a selected truck
            {
                NameBox.Text = truck.Name;
            }

            SaveButton.IsEnabled = true;
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)  // Saves the changes made to selected truck (Selected w/ Edit Button click)
        {
            if (NameBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a name.");  
                mes.Show();
                NameBox.Text = string.Empty;
            }
            else
            {
                Truck truck = TruckList.SelectedItem as Truck;

                if (truck != null)
                {
                    truck.Name = NameBox.Text;

                    await Task.Run(() => uow.Trucks.UpdateAsync(truck));
                    NameBox.Text = string.Empty;
                    NameBox.Focus();
                    RefreshList();
                    SaveButton.IsEnabled = false;
                }
            }
                
        }

        private async void RemoveButton_Click(object sender, RoutedEventArgs e) // Removes selected truck from the DB
        {
            Truck truck = TruckList.SelectedItem as Truck;

            if (truck != null)
            {
                var output = await Task.Run(() => uow.Trucks.DeleteAsync(truck.TruckId));
                RefreshList(); 
                NameBox.Text = string.Empty;
                NameBox.Focus();
            }
            
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
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
