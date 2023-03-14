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
        private Employee _adminEmployee;
        private ObservableCollection<Truck> trucks;
        public EditTrucks()
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            InitializeComponent();
            trucks = new ObservableCollection<Truck>();
            TruckList.ItemsSource = trucks;
        }
        private async void TruckList_Initialized(object sender, EventArgs e)
        {
            foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
            {
                trucks.Add(t);
            }
        }
        private async void RefreshList()
        {
            trucks.Clear();

            foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
            {
                trucks.Add(t);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Truck truck = new Truck();
            truck.Name = NameBox.Text;
            uow.Trucks.AddAsync(truck);
            RefreshList();
        }

        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Truck truck = TruckList.SelectedItem as Truck;
            var output = await Task.Run(() => uow.Trucks.DeleteAsync(truck.TruckId));
            RefreshList();
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
