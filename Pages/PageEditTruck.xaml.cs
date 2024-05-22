using System.Windows.Controls;
using TransComp.Data.ViewModels;

namespace TransComp.Pages;

public partial class PageEditTruck : Page
{
    public PageEditTruck(int TruckId)
    {
        InitializeComponent();

        DataContext = new EditTruckViewModel(TruckId);
    }
}