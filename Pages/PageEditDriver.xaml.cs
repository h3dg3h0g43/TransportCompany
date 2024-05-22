using System.Windows.Controls;
using TransComp.Data.ViewModels;

namespace TransComp.Pages;

public partial class PageEditDriver : Page
{
    public PageEditDriver(int DriverId)
    {
        InitializeComponent();
        
        DataContext = new EditDriverViewModel(DriverId);
    }
}