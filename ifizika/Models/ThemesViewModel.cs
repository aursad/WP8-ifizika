using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Windows;
using ifizika.ViewModel;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;

namespace ifizika.Models
{
    public class ThemesViewModel : INotifyPropertyChanged
    {
        const string ApiUrl = @"http://api.ifizika.info/v1/theme";

        public ThemesViewModel()
        {
            this.Items = new ObservableCollection<ThemeModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ThemeModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData(string idClass, string idTheme)
        {
            if (this.IsDataLoaded == false)
            {
                this.Items.Clear();
                
                var webClient = new WebClient();
                webClient.Headers["Accept"] = "application/json";
                webClient.Headers["Securitykey"] = App.SecurityKey;
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCatalogCompleted);

                string url = string.Format("{0}/{1}/{2}", ApiUrl, idClass, idTheme);
                webClient.DownloadStringAsync(new Uri(url));
            }
            this.IsDataLoaded = false;
        }

        private void webClient_DownloadCatalogCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                this.Items.Clear();
                if (e.Result != null)
                {
                    var books = JsonConvert.DeserializeObject<ThemeViewModel>(e.Result);
                    foreach (var classObject in books.Themes)
                    {
                        this.Items.Add(classObject);
                    }
                    NotifyPropertyChanged("Items");
                    SystemTray.ProgressIndicator.IsVisible = false; 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("CodeMonkeys get error: " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}