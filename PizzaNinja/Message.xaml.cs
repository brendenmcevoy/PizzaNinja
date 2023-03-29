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

namespace PizzaNinja
{
    /// <summary>
    /// Interaction logic for Message.xaml
    /// </summary>
    public partial class Message : Window // Used to show stylized error messages to the user
    {       
        public Message(string message) // Used for a short message
        {
            InitializeComponent();
            MessageLabel.Text = message;
        }
        public Message(string message, string subMessage) // used for a longer message
        {
            InitializeComponent();
            MessageLabel.Text = message;
            SubMessageLabel.Text = subMessage;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e) // closes message box
        {
            this.Close();
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
