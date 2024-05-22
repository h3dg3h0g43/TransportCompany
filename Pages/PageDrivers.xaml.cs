using System;
using System.Windows;
using System.Windows.Controls;
using TransComp.Data;

namespace TransportComp.Pages
{

    public partial class PageDrivers : Page
    {
        public PageDrivers()
        {
            InitializeComponent();
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Id = Convert.ToInt32(button.Tag);
            SupportObject.mainFrame.Navigate(new PageEditDriver(Id));
        }

        private void ButtonDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Id = Convert.ToInt32(button.Tag);
            SupportObject.mainFrame.Navigate(new PageDeleteDriver(Id));
        }
    }
}