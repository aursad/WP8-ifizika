using System.Windows;
using System.Windows.Navigation;
using ifizika.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ifizika.Pages
{
    public partial class Details : PhoneApplicationPage
    {
        public string IdPost;

        public Details()
        {
            InitializeComponent();
            App.PostViewModel.Items.Clear();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("id", out selectedIndex))
            {
                IdPost = selectedIndex;
                string detailsName = "";
                NavigationContext.QueryString.TryGetValue("name", out detailsName);
                Name.Text = detailsName;

                var prog = new ProgressIndicator
                {
                    IsVisible = true,
                    IsIndeterminate = true,
                    Text = AppResources.LoadingData
                };
                SystemTray.SetProgressIndicator(this, prog);

                List.DataContext = App.PostViewModel.Items;
                this.Loaded += DataLoaded;
            }
        }

        private void DataLoaded(object sender, RoutedEventArgs e)
        {
            App.PostViewModel.LoadData(IdPost);
        }
    }
}