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
    /// Interaction logic for EditJobs.xaml
    /// </summary>
    public partial class EditJobs : Window
    {
        private IConnectionFactory conn;
        private UnitOfWork uow;
        private ObservableCollection<Job> jobs;
        public EditJobs()
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            jobs = new ObservableCollection<Job>();
            InitializeComponent();
            JobList.ItemsSource = jobs;
        }

        private async void JobList_Initialized(object sender, EventArgs e)
        {
            foreach (Job j in new ObservableCollection<Job>(await Task.Run(() => uow.Jobs.GetAllAsync().Result)))
            {
                jobs.Add(j);
            }
        }

        private async void RefreshList()
        {
            jobs.Clear();

            foreach (Job j in new ObservableCollection<Job>(await Task.Run(() => uow.Jobs.GetAllAsync().Result)))
            {
                jobs.Add(j);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Job job = new Job();
            job.JobId = int.Parse(JobIdBox.Text);
            job.Name = NameBox.Text;
            job.Description = DescriptionBox.Text;
            job.TruckId = int.Parse(TruckIdBox.Text);

            var output = uow.Jobs.AddAsync(job);    
            RefreshList();
        }

        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var job = JobList.SelectedItem as Job;
            var output = await Task.Run(() => uow.Jobs.DeleteAsync(job.JobId));
            RefreshList();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
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
