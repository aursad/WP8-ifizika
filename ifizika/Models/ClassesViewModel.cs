using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using Contracts.Class;
using ifizika.ViewModel;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;

namespace ifizika.Models
{
    public class ClassesViewModel : INotifyPropertyChanged
    {
        const string ApiUrl = @"http://api.ifizika.info/v1/class";

        public ClassesViewModel()
        {
            this.Items = new ObservableCollection<ClassModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ClassModel> Items { get; private set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            if (this.IsDataLoaded == false)
            {
                this.Items.Clear();
                
                var webClient = new WebClient();
                webClient.Headers["Accept"] = "application/json";
                webClient.Headers["Securitykey"] = App.SecurityKey;
                webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadCatalogCompleted);
                webClient.DownloadStringAsync(new Uri(ApiUrl));
            }
        }

        private void webClient_DownloadCatalogCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            try
            {
                this.Items.Clear();
                if (e.Result != null)
                {
                    var books = JsonConvert.DeserializeObject<ClassViewModel>(e.Result);
                    int id = 0;
                    foreach (ClassModel classObject in books.Classes)
                    {
                        this.Items.Add(classObject);
                    }
                    this.IsDataLoaded = true;
                    SystemTray.ProgressIndicator.IsVisible = false; 
                }
            }
            catch (Exception ex)
            {
               throw new Exception("Test exceptipon");
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