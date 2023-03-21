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
        private Job _job;
        private Employee _employee;
        private Truck _truck;
        private IConnectionFactory conn;
        private UnitOfWork uow;
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
        }
        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            CompletedJob cj = new CompletedJob();
            cj.JobId = _job.JobId;
            cj.EmployeeId = _employee.Id;
            cj.TruckId = _truck.TruckId;
            cj.Date = DateTime.Now.ToShortDateString();
            cj.Notes = NotesBox.Text;
            cj.Name = _job.Name;
            cj.Description = _job.Description;
            
            var test = await Task.Run(() => uow.CompletedJobs.AddAsync(cj));
            var testicle = await Task.Run(() => uow.Jobs.DeleteByIdAsync(_job.JobId,_truck.TruckId));

            this.Close();
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
