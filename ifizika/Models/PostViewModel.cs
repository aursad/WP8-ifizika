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
    public class PostViewModel : INotifyPropertyChanged
    {
        const string ApiUrl = @"http://api.ifizika.info/v1/post";

        public PostViewModel()
        {
            this.Item = new PostModel();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public PostModel Item { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData(string idPost)
        {
            if (this.IsDataLoaded == false)
            {
                var webClient = new WebClient();
                webClient.Headers["Accept"] = "application/json";
                webClient.Headers["Securitykey"] = App.SecurityKey;
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCatalogCompleted);

                string url = string.Format("{0}/{1}", ApiUrl, idPost);
                webClient.DownloadStringAsync(new Uri(url));
            }
            this.IsDataLoaded = false;
        }

        private void webClient_DownloadCatalogCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                if (e.Result != null)
                {
                    var books = JsonConvert.DeserializeObject<PostModel>(e.Result);
                    Item = books;
                    NotifyPropertyChanged("Item");
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