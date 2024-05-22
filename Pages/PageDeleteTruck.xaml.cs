using System.Windows;
using System.Windows.Controls;
using TransComp.Data.ViewModels;
using TransportComp.Data;
using TransportComp.Data.ViewModels;

namespace TransportComp.Pages
{

    public partial class PageDeleteTruck : Page
    {
        public PageDeleteTruck(int TruckId)
        {
            InitializeComponent();

            DataContext = new DeleteTruckViewModel(TruckId);
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            SupportObject.mainFrame.GoBack();
        }
    }
}