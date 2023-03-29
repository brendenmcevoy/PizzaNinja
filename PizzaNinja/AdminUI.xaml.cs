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
        private IConnectionFactory conn; //Connects to DB
        private UnitOfWork uow; // Contains Repos that retrevie and manipulate data in the DB
        private Employee _adminEmployee; // Contains all the info for the current user
        public AdminUI(Employee adminEmployee)
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            _adminEmployee = adminEmployee;
            InitializeComponent(); 
        }
        private async void TruckBox_Initialized(object sender, EventArgs e) //Populates ComboBox with all trucks in the DB
        {
            foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
            {
                TruckBox.Items.Add(t);
            }
        }
        private async void RefreshList() //Refresh list of trucks in the ComboBox
        {
            TruckBox.Items.Clear();

            foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
            {
                TruckBox.Items.Add(t);
            }
        }
        private async void TruckBox_SelectionChanged(object sender, SelectionChangedEventArgs e) //Displays list of jobs in the DataGrid based on selected truck
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
        private void JobButton_Click(object sender, RoutedEventArgs e) //Load the Job View, used to complete a job
        {
            Job job = (Job)JobsDisplay.SelectedItem;
            Truck truck = (Truck)TruckBox.SelectedItem;
            JobsUI jobsUi = new JobsUI(job, _adminEmployee, truck);
            jobsUi.Show();
        }
        private void EditEmployees_Click(object sender, RoutedEventArgs e) //Load Edit Employee View
        {
            EditEmployees editE = new EditEmployees(_adminEmployee);
            editE.Show();
        }
        private void EditTrucks_Click(object sender, RoutedEventArgs e) //Load Edit Truck View
        {
            EditTrucks editT = new EditTrucks();
            editT.Show();
            RefreshList();
        }
        private void EditJobs_Click(object sender, RoutedEventArgs e) //Load Edit Jobs View
        {
            EditJobs editJ = new EditJobs();
            editJ.Show();
        }
        private async void CompletedJobsButton_Click(object sender, RoutedEventArgs e) //Show the list of Completed Jobs in its own DataGrid
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
        } // Allows user to drag the form from anywhere

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        } // Close app

        private void MinimizeButton_Click(object sender, RoutedEventArgs e) // Minimize window
        {
            WindowState = WindowState.Minimized;
        }

        
    }
}
