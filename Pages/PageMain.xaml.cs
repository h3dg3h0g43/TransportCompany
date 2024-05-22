using System.Windows;
using System.Windows.Controls;
using TransportComp.Data;

namespace TransportComp.Pages
{

    public partial class PageMain : Page
    {
        public PageMain()
        {
            InitializeComponent();
        }

        private void BtnAddTruck_OnClick(object sender, RoutedEventArgs e)
        {
            SupportObject.mainFrame.Navigate(new PageAddTruck());
        }

        private void BtnPartnership_OnClick(object sender, RoutedEventArgs e)
        {
            SupplyMethods.SetMessageToStatusBar("Work in progress!");
        }

        private void BtnTrucks_OnClick(object sender, RoutedEventArgs e)
        {
            SupportObject.mainFrame.Navigate(new PageTrucks());
        }

        private void BtnStatus_OnClick(object sender, RoutedEventArgs e)
        {
            SupplyMethods.SetMessageToStatusBar("Work in progress!");
        }

        private void BtnAddStaff_OnClick(object sender, RoutedEventArgs e)
        {
            SupportObject.mainFrame.Navigate(new PageAddDriver());
        }

        private void BtnManagement_OnClick(object sender, RoutedEventArgs e)
        {
            SupplyMethods.SetMessageToStatusBar("Work in progress!");
        }

        private void BtnStaff_OnClick(object sender, RoutedEventArgs e)
        {
            SupportObject.mainFrame.Navigate(new PageDrivers());
        }

        private void BtnEconomy_OnClick(object sender, RoutedEventArgs e)
        {
            SupplyMethods.SetMessageToStatusBar("Work in progress!");
        }
    }
}