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
        private IConnectionFactory conn; // Used to connect to DB
        private UnitOfWork uow; // Contains repos to retrive and maipulate data in the DB
        private Employee _employee; // Contains data about current user
        public EmployeeUI(Employee employee)
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            _employee = employee;
            InitializeComponent();
        }

        private async void TruckBox_Initialized(object sender, EventArgs e) //Populates ComboBox with all trucks in the DB
        {       
            foreach (Truck t in new ObservableCollection<Truck>(await Task.Run(() => uow.Trucks.GetAllAsync().Result)))
            {
                TruckBox.Items.Add(t);
            }
        }
        public async void RefreshList()  //Refresh list of trucks in the ComboBox
        {
            JobsDisplay.Items.Clear();

            foreach (Job j in new ObservableCollection<Job>(await Task.Run(() => uow.Jobs.GetAllAsync().Result)))
            {
                JobsDisplay.Items.Add(j);
            }
        }
        private async void TruckBox_SelectionChanged(object sender, SelectionChangedEventArgs e)  //Displays list of jobs in the DataGrid based on selected truck
        {
            JobsDisplay.Items.Clear();
            Truck truck = (Truck)TruckBox.SelectedItem;

            foreach (Job j in new List<Job>(await Task.Run(() => uow.Jobs.GetAllByIdAsync(truck.TruckId).Result)))
            {
                JobsDisplay.Items.Add(j);
            }
        }
        private void JobButton_Click(object sender, RoutedEventArgs e)  //Load the Job View, used to complete a job
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
            WindowState = WindowState.Minimized;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }       
    }
}
