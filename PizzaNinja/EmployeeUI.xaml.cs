using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Printing;
using System.Runtime.CompilerServices;
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
        private Employee _employee;
        private ObservableCollection<Truck> trucks;
        public EmployeeUI(Employee employee)
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            _employee = employee;
            InitializeComponent();
            trucks = new ObservableCollection<Truck>();
            TruckBox.ItemsSource = trucks;
        }

        private async void TruckBox_Initialized(object sender, EventArgs e)
        {       
            foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
            {
                trucks.Add(t);
            }
        }
        public async void RefreshList()
        {
            JobsDisplay.Items.Clear();

            foreach (Job j in new ObservableCollection<Job>(await Task.Run(() => uow.Jobs.GetAllAsync().Result)))
            {
                JobsDisplay.Items.Add(j);
            }
        }
        private async void TruckBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JobsDisplay.Items.Clear();
            Truck truck = (Truck)TruckBox.SelectedItem;

            foreach (Job j in new List<Job>(await Task.Run(() => uow.Jobs.GetAllByIdAsync(truck.TruckId).Result)))
            {
                JobsDisplay.Items.Add(j);
            }
        }
        private void JobButton_Click(object sender, RoutedEventArgs e)
        {
            Job job = (Job)JobsDisplay.SelectedItem;
            Truck truck = (Truck)TruckBox.SelectedItem;
            JobsUI jobsUi = new JobsUI(job,_employee,truck);
            jobsUi.Show();  
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
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
