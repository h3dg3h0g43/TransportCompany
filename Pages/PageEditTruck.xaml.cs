using System.Windows.Controls;
using TransportComp.Data.ViewModels;

namespace TransportComp.Pages
{

    public partial class PageEditTruck : Page
    {
        public PageEditTruck(int TruckId)
        {
            InitializeComponent();

            DataContext = new EditTruckViewModel(TruckId);
        }
    }
}