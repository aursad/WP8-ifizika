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
    public class CategoriesViewModel : INotifyPropertyChanged
    {
        const string ApiUrl = @"http://api.ifizika.info/v1/category";

        public CategoriesViewModel()
        {
            this.Items = new ObservableCollection<CategoryModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<CategoryModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData(string idTheme)
        {
            if (this.IsDataLoaded == false)
            {
                this.Items.Clear();
                
                var webClient = new WebClient();
                webClient.Headers["Accept"] = "application/json";
                webClient.Headers["Securitykey"] = App.SecurityKey;
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCatalogCompleted);

                string url = string.Format("{0}/{1}", ApiUrl, idTheme);
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
                    var books = JsonConvert.DeserializeObject<CategoryViewModel>(e.Result);
                    foreach (var classObject in books.Categories)
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