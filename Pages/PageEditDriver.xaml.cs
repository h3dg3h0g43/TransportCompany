using System.Windows.Controls;
using TransportComp.Data.ViewModels;

namespace TransportComp.Pages
{

    public partial class PageEditDriver : Page
    {
        public PageEditDriver(int DriverId)
        {
            InitializeComponent();

            DataContext = new EditDriverViewModel(DriverId);
        }
    }
}