using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ifizika.Resources;
using ifizika.ViewModel;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ifizika
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
            var prog = new ProgressIndicator
            {
                IsVisible = true,
                IsIndeterminate = true,
                Text = AppResources.LoadingData
            };
            SystemTray.SetProgressIndicator(this, prog);

            ClassList.DataContext = App.ViewModel.Items;
            this.Loaded += DataLoaded;

        }

        // Build a localized ApplicationBar
        private void BuildLocalizedApplicationBar()
        {
            // Set the page's ApplicationBar to a new instance of ApplicationBar.
            ApplicationBar = new ApplicationBar();

            // Create a new button and set the text value to the localized string from AppResources.
            var appBarButton =
                new ApplicationBarIconButton(new Uri("/Assets/Images/refresh.png", UriKind.Relative))
                {
                    Text = "Refresh"
                };
            ApplicationBar.Buttons.Add(appBarButton);

            // Create a new menu item with the localized string from AppResources.
            var appBarMenuItemContacts = new ApplicationBarMenuItem(AppResources.Contacts);
            appBarMenuItemContacts.Click += Contacts;
            var appBarMenuItemFeedback = new ApplicationBarMenuItem(AppResources.Feedback);
            appBarMenuItemFeedback.Click += Feedback;

            ApplicationBar.MenuItems.Add(appBarMenuItemContacts);
            ApplicationBar.MenuItems.Add(appBarMenuItemFeedback);
        }

        private void DataLoaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }
        // Handle selection changed on ListBox
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected index is -1 (no selection) do nothing
            if (ClassList.SelectedIndex == -1)
                return;

            // Navigate to the new page
            var tempClassModel = ClassList.SelectedItem as ClassModel;
            if (tempClassModel != null)
                NavigationService.Navigate(new Uri("/Pages/Details.xaml?selectedItem=" + tempClassModel.Class+"&name="+tempClassModel.Name, UriKind.Relative));

            // Reset selected index to -1 (no selection)
            ClassList.SelectedIndex = -1;
        }

        private void Contacts(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Pages/Contacts.xaml", UriKind.Relative));
        }

        private void Feedback(object sender, EventArgs e)
        {
            MessageBox.Show("Feedback button works!");
        }
    }
}