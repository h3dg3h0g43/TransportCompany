using System;
using System.Windows;
using System.Windows.Controls;
using TransComp.Data;

namespace TransportComp.Pages
{

    public partial class PageTrucks : Page
    {
        public PageTrucks()
        {
            InitializeComponent();
        }

        private void BtnEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Id = Convert.ToInt32(button.Tag);
            SupportObject.mainFrame.Navigate(new PageEditTruck(Id));
        }

        private void BtnDelete_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var Id = Convert.ToInt32(button.Tag);
            SupportObject.mainFrame.Navigate(new PageDeleteTruck(Id));
        }
    }
}