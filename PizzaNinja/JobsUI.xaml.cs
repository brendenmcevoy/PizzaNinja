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
        private IConnectionFactory conn;
        private UnitOfWork uow;
        public JobsUI(Job job,Employee employee)
        {
            _job = job;
            _employee = employee;
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            InitializeComponent();
            NameBox.DataContext = _job; 
            DescriptionBox.DataContext = _job;
            empName.DataContext = _employee;
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            int jobId = _job._jobId;
            int employeeId = _employee._employeeId;
            int truckId = 1;
            string date = DateTime.Now.ToShortDateString();
            CompletedJob cj = new CompletedJob(1,jobId,employeeId,truckId,date);
            var test = await Task.Run(() => uow.CompletedJobs.CompleteJobAsync(cj));
            var testicle = await Task.Run(() => uow.Jobs.DeleteAsync(jobId));

            this.Close();
        }
    }
}
