using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using ifizika.Models;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace ifizika.Pages
{
    public partial class Settings : PhoneApplicationPage
    {
        private readonly string[] _colorList = new string[]
        {
            "Black", "White"
        };
        private readonly AppSettings _settings = new AppSettings();

        public Settings()
        {
            InitializeComponent();
            InitializeMenuBar();
            //Load ListPicker settings
            ListPicker.ItemsSource = _colorList;
            ListPicker.SelectedIndex = SelectedColorSwitch(_settings.ColorSetting);
            //ListPicker.SelectionChanged += ChangeThemeColor;
            //Load Save data check box
            SaveDataCheckBox.IsChecked = _settings.SaveDataSetting;
        }

        private void ChangeThemeColor(object sender, SelectionChangedEventArgs e)
        {
            if (ListPicker.SelectedItem.ToString() == "White")
            {
                DarkTheme();
            }
        }


        private void InitializeMenuBar()
        {
            ApplicationBar = new ApplicationBar
            {
                IsMenuEnabled = true, 
                IsVisible = true, 
                Opacity = 1.0
            };

            var doneButton = new ApplicationBarIconButton(new Uri("/Assets/Images/check.png", UriKind.Relative))
            {
                Text = "Atlikta"
            };
            doneButton.Click += doneButton_Click;

            var cancelButton = new ApplicationBarIconButton(new Uri("/Assets/Images/close.png", UriKind.Relative))
            {
                Text = "Atšaukti"
            };
            cancelButton.Click += cancelButton_Click;

            ApplicationBar.Buttons.Add(doneButton);
            ApplicationBar.Buttons.Add(cancelButton);
        }

        /// <summary> 
        /// Done button clicked event handler 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        void doneButton_Click(object sender, EventArgs e)
        {
            var selectedColor = ListPicker.SelectedItem.ToString();
            _settings.ColorSetting = listPickerSwitch(selectedColor);

            if (SaveDataCheckBox.IsChecked != null)
            {
                _settings.SaveDataSetting = (bool) SaveDataCheckBox.IsChecked;
            }
            NavigationService.GoBack();
        }
        /// <summary> 
        /// Cancel button clicked event handler 
        /// </summary> 
        /// <param name="sender"></param> 
        /// <param name="e"></param> 
        void cancelButton_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }

        private Color listPickerSwitch(string color)
        {
            Color tempColor;
            switch (color)
            {
                case "Black":
                    tempColor = Colors.Black;
                    break;
                case "White":
                    tempColor = Colors.White;
                    break;
                default:
                    tempColor = Colors.Black;
                    break;
            }
            return tempColor;
        }
        private int SelectedColorSwitch(Color color)
        {
            int tempColor;
            switch (color.ToString())
            {
                case "#FF000000":
                    tempColor = Array.IndexOf(_colorList, "Black");
                    break;
                case "#FFFFFFFF":
                    tempColor = Array.IndexOf(_colorList, "White");
                    break;
                default:
                    tempColor = Array.IndexOf(_colorList, "Black");
                    break;
            }
            return tempColor;
        }

        private void DarkTheme()
        {
            ((SolidColorBrush)Resources["PhoneRadioCheckBoxCheckBrush"]).Color = ((SolidColorBrush)Resources["PhoneRadioCheckBoxBorderBrush"]).Color = ((SolidColorBrush)Resources["PhoneForegroundBrush"]).Color = Color.FromArgb(0xDE, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneBackgroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneContrastForegroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneContrastBackgroundBrush"]).Color = Color.FromArgb(0xDE, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneDisabledBrush"]).Color = Color.FromArgb(0x4D, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneProgressBarBackgroundBrush"]).Color = Color.FromArgb(0x19, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextCaretBrush"]).Color = Color.FromArgb(0xDE, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextBoxBrush"]).Color = Color.FromArgb(0x26, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextBoxForegroundBrush"]).Color = Color.FromArgb(0xDE, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextBoxEditBackgroundBrush"]).Color = Color.FromArgb(0x00, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextBoxReadOnlyBrush"]).Color = Color.FromArgb(0x2E, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneSubtleBrush"]).Color = Color.FromArgb(0x66, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextBoxSelectionForegroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneButtonBasePressedForegroundBrush"]).Color = Color.FromArgb(0xFF, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneTextHighContrastBrush"]).Color = Color.FromArgb(0xDE, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextMidContrastBrush"]).Color = Color.FromArgb(0x73, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneTextLowContrastBrush"]).Color = Color.FromArgb(0x40, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneSemitransparentBrush"]).Color = Color.FromArgb(0xAA, 0xFF, 0xFF, 0xFF);
            ((SolidColorBrush)Resources["PhoneChromeBrush"]).Color = Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD);

            ((SolidColorBrush)Resources["PhoneInactiveBrush"]).Color = Color.FromArgb(0x33, 0x00, 0x00, 0x00);
            ((SolidColorBrush)Resources["PhoneInverseInactiveBrush"]).Color = Color.FromArgb(0xFF, 0xE5, 0xE5, 0xE5);
            ((SolidColorBrush)Resources["PhoneInverseBackgroundBrush"]).Color = Color.FromArgb(0xFF, 0xDD, 0xDD, 0xDD);
            ((SolidColorBrush)Resources["PhoneBorderBrush"]).Color = Color.FromArgb(0x99, 0x00, 0x00, 0x00);

        }
    }
}