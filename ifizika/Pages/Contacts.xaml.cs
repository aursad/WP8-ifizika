using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Tasks;

namespace ifizika.Pages
{
    public partial class Contacts : PhoneApplicationPage
    {
        public Contacts()
        {
            InitializeComponent();
        }

        private void Go_To_Page(object sender, RoutedEventArgs e)
        {
            var webBrowserTask = new WebBrowserTask
            {
                Uri = new Uri("http://www.aursad.eu/")
            };
            webBrowserTask.Show(); 
        }

        private void Write_Email(object sender, RoutedEventArgs e)
        {
            var emailComposeTask = new EmailComposeTask
            {
                Subject = "iFizika.info : ",
                To = "info@ifizika.info",
                Bcc = "aurimas.sadauskas@gmail.com"
            };

            emailComposeTask.Show();
        }
    }
}