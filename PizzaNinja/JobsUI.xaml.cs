using System;
using System.Collections.Generic;
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
using PN.DB.Interfaces;
using PN.DB.UOW;
using PN.Logic;

namespace PizzaNinja
{
    /// <summary>
    /// Interaction logic for JobsUI.xaml
    /// </summary>
    public partial class JobsUI : Window
    {
        private Job _job; // Contains data for the current job
        private Employee _employee; // Contains data for the current user
        private Truck _truck; // Contains data for the specific user
        private IConnectionFactory conn; // Connects to DB
        private UnitOfWork uow; // Contains repos to manipulate and retreive data from DB
        public JobsUI(Job job,Employee employee,Truck truck)
        {
            _job = job;
            _employee = employee;
            _truck = truck;
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            InitializeComponent();
            NameBox.DataContext = _job; 
            DescriptionBox.DataContext = _job;
            NotesBox.Focus(); // Loads with focus set to the first textbox to promote quick input
        }
        private async void SubmitButton_Click(object sender, RoutedEventArgs e) // Deleted the current job from the DB and makes a new Completed Job using data from the previous job
        {
            CompletedJob cj = new CompletedJob(); // creates the completed job that is a copy of the current job
            cj.JobId = _job.JobId;
            cj.EmployeeId = _employee.Id;
            cj.TruckId = _truck.TruckId;
            cj.Date = DateTime.Now.ToShortDateString();
            cj.Notes = NotesBox.Text;
            cj.Name = _job.Name;
            cj.Description = _job.Description;
            
            var test = await Task.Run(() => uow.CompletedJobs.AddAsync(cj)); // add completed job to the Db
            var testicle = await Task.Run(() => uow.Jobs.DeleteByIdAsync(_job.JobId,_truck.TruckId)); // remove the finished job from Job table
 
            this.Close(); // closes Job View
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }       
    }
}
