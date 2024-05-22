using System.Windows;
using System.Windows.Controls;
using TransComp.Data;
using TransComp.Data.ViewModels;

namespace TransportComp.Pages
{

    public partial class PageDeleteTruck : Page
    {
        public PageDeleteTruck(int TruckId)
        {
            InitializeComponent();

            DataContext = new DeleteDriverViewModel(TruckId);
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            SupportObject.mainFrame.GoBack();
        }
    }
}