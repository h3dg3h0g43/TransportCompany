using System.Windows;
using System.Windows.Controls;
using TransComp.Data;
using TransComp.Data.ViewModels;

namespace TransportComp.Pages
{

    public partial class PageDeleteDriver : Page
    {
        public PageDeleteDriver(int DriverId)
        {
            InitializeComponent();

            DataContext = new DeleteDriverViewModel(DriverId);
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            SupportObject.mainFrame.GoBack();
        }
    }
}