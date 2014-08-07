using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ifizika.Resources;
using ifizika.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ifizika.Pages
{
    public partial class Themes : PhoneApplicationPage
    {
        public string IdClass;
        public string IdCategory;
        private bool _isDataLoaded;

        public Themes()
        {
            InitializeComponent();
            if (_isDataLoaded == false)
            {
                App.ThemesViewModel.Items.Clear();
            }
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            base.OnNavigatingFrom(e);

            _isDataLoaded = true;
        }
        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("class", out selectedIndex))
            {
                IdClass = selectedIndex;
                string idCategory = "";
                NavigationContext.QueryString.TryGetValue("category", out idCategory);
                IdCategory = idCategory;
                string themeName = "";
                NavigationContext.QueryString.TryGetValue("name", out themeName);
                ThemeName.Text = themeName;

                List.DataContext = App.ThemesViewModel.Items;
                this.Loaded += DataLoaded;
            }
        }

        // Handle selection changed on ListBox
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (List.SelectedIndex == -1)
                return;

            // Navigate to the new page
            var tempModel = List.SelectedItem as ThemeModel;
            if (tempModel != null)
                NavigationService.Navigate(new Uri("/Pages/Details.xaml?id=" + tempModel.Id+"&name="+tempModel.Name, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            List.SelectedIndex = -1;
        }


        private void DataLoaded(object sender, RoutedEventArgs e)
        {
            if (_isDataLoaded == false)
            {
                var prog = new ProgressIndicator
                {
                    IsVisible = true,
                    IsIndeterminate = true,
                    Text = AppResources.LoadingData
                };
                SystemTray.SetProgressIndicator(this, prog);

               App.ThemesViewModel.LoadData(IdClass, IdCategory); 
            }
        }
    }
}