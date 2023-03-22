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
    /// Interaction logic for AdminUI.xaml
    /// </summary>
    public partial class AdminUI : Window
    {
        private IConnectionFactory conn;
        private UnitOfWork uow;
        private Employee _adminEmployee;
        private ObservableCollection<Truck> trucks;
        public AdminUI(Employee adminEmployee)
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            _adminEmployee = adminEmployee;
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
        private async void RefreshList()
        {
            trucks.Clear();

            foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
            {
                trucks.Add(t);
            }
        }
        private async void TruckBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JobsDisplay.Visibility = Visibility.Visible;
            CompletedJobsDisplay.Visibility = Visibility.Hidden;

            JobsDisplay.Items.Clear();

            Truck truck = (Truck)TruckBox.SelectedItem;  
            if (truck != null) 
            {
                foreach (Job j in new List<Job>(await Task.Run(() => uow.Jobs.GetAllByIdAsync(truck.TruckId).Result)))
                {
                    JobsDisplay.Items.Add(j);
                }
            }
        }
        private void JobButton_Click(object sender, RoutedEventArgs e)
        {
            Job job = (Job)JobsDisplay.SelectedItem;
            Truck truck = (Truck)TruckBox.SelectedItem;
            JobsUI jobsUi = new JobsUI(job, _adminEmployee, truck);
            jobsUi.Show();
        }
        private void EditEmployees_Click(object sender, RoutedEventArgs e)
        {
            EditEmployees editE = new EditEmployees(_adminEmployee);
            editE.Show();
        }
        private void EditTrucks_Click(object sender, RoutedEventArgs e)
        {
            EditTrucks editT = new EditTrucks();
            editT.Show();
            RefreshList();
        }
        private void EditJobs_Click(object sender, RoutedEventArgs e)
        {
            EditJobs editJ = new EditJobs();
            editJ.Show();
        }
        private async void CompletedJobsButton_Click(object sender, RoutedEventArgs e)
        {
            CompletedJobsDisplay.Visibility = Visibility.Visible;
            JobsDisplay.Visibility = Visibility.Hidden;

            CompletedJobsDisplay.Items.Clear();

            foreach(CompletedJob cj in new List<CompletedJob>(await Task.Run(() => uow.CompletedJobs.GetAllAsync().Result)))
            {
                CompletedJobsDisplay.Items.Add(cj);
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
            Application.Current.Shutdown();
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
