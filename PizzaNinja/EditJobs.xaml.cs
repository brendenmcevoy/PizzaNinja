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
        public EditJobs()
        {
            conn = new DatabaseConnectionFactory();
            uow = new UnitOfWork(conn);
            InitializeComponent();
            JobIdBox.Focus();
            SaveButton.IsEnabled = false;
        }

        private async void JobList_Initialized(object sender, EventArgs e)
        {
            foreach (Job j in new ObservableCollection<Job>(await Task.Run(() => uow.Jobs.GetAllAsync().Result)))
            {
                JobList.Items.Add(j);
            }
        }

        private async void RefreshList()
        {
            JobList.Items.Clear();

            foreach (Job j in new ObservableCollection<Job>(await Task.Run(() => uow.Jobs.GetAllAsync().Result)))
            {
                JobList.Items.Add(j);
            }
        }
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a name.");
                mes.Show();
                ClearBoxes();
            }
            else if (JobIdBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a Job Id.");
                mes.Show();
                ClearBoxes();
            }
            else if (DescriptionBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a description.");
                mes.Show();
                ClearBoxes();
            }
            else if (TruckIdBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a Truck Id.");
                mes.Show();
                ClearBoxes();
            }
            else
            {
                try
                {
                    Job job = new Job();
                    if (job != null)
                    {
                        job.JobId = int.Parse(JobIdBox.Text);
                        job.Name = NameBox.Text;
                        job.Description = DescriptionBox.Text;
                        job.TruckId = int.Parse(TruckIdBox.Text);

                        await Task.Run(() => uow.Jobs.AddAsync(job));
                        RefreshList();
                        ClearBoxes();
                        JobIdBox.Focus();
                    }
                }
                catch (FormatException)
                {
                    Message mes = new Message("Invalid input.", "Job Id and Truck Id must be a positive number. (ex.1002)");
                    mes.Show();
                    ClearBoxes();
                }
            }    
        }

        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var job = JobList.SelectedItem as Job;

            if(job != null)
            {
                await Task.Run(() => uow.Jobs.DeleteAsync(job.Id));
                RefreshList();
                ClearBoxes();
                JobIdBox.Focus();
            }          
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            ClearBoxes();

            Job job = JobList.SelectedItem as Job;

            if (job != null)
            {
                JobIdBox.Text = job.JobId.ToString();
                NameBox.Text = job.Name;
                DescriptionBox.Text = job.Description;
                TruckIdBox.Text = job.TruckId.ToString();         
            }

            SaveButton.IsEnabled = true;
        }
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (NameBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a name.");
                mes.Show();
                ClearBoxes();
            }
            else if (JobIdBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a Job Id.");
                mes.Show();
                ClearBoxes();
            }
            else if (DescriptionBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a description.");
                mes.Show();
                ClearBoxes();
            }
            else if (TruckIdBox.Text == string.Empty)
            {
                Message mes = new Message("Invalid input.", "Must enter a Truck Id.");
                mes.Show();
                ClearBoxes();
            }
            else
            {
                try
                {
                    Job job = JobList.SelectedItem as Job;

                    if (job != null)
                    {
                        job.JobId = int.Parse(JobIdBox.Text);
                        job.Name = NameBox.Text;
                        job.Description = DescriptionBox.Text;
                        job.TruckId = int.Parse(TruckIdBox.Text);

                        await Task.Run(() => uow.Jobs.UpdateAsync(job));
                        ClearBoxes();
                        JobIdBox.Focus();
                        RefreshList();
                        SaveButton.IsEnabled = false;
                    }
                }catch (FormatException)
                {
                    Message mes = new Message("Invalid input.", "Job Id and Truck Id must be a positive number. (ex.1002)");
                    mes.Show();
                    ClearBoxes();
                }
            }
            
        }
        private void ClearBoxes()
        {
            JobIdBox.Text = string.Empty;
            NameBox.Text = string.Empty;
            DescriptionBox.Text = string.Empty;
            TruckIdBox.Text = string.Empty;
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

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        
    }
}
