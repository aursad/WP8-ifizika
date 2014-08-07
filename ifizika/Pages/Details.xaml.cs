using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ifizika.Resources;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ifizika.Pages
{
    public partial class Page1 : PhoneApplicationPage
    {
        public string IdTheme;

        public Page1()
        {
            InitializeComponent();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string selectedIndex = "";
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                IdTheme = selectedIndex;
                string themeName = "";
                NavigationContext.QueryString.TryGetValue("name", out themeName);
                ThemeName.Text = themeName;
                var prog = new ProgressIndicator
                {
                    IsVisible = true,
                    IsIndeterminate = true,
                    Text = AppResources.LoadingData
                };
                SystemTray.SetProgressIndicator(this, prog);

                List.DataContext = App.ThemesViewModel.Items;
                this.Loaded += DataLoaded;
            }
        }

        // Handle selection changed on ListBox
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show("Clicked link with ID: " + List.SelectedIndex);
        }

        private void DataLoaded(object sender, RoutedEventArgs e)
        {
            if (!App.ThemesViewModel.IsDataLoaded)
            {
                App.ThemesViewModel.LoadData(IdTheme);
            }
        }
    }
}