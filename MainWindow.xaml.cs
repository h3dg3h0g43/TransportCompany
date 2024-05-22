using System.Windows;
using TransportComp.Data;
using TransportComp.Data.Context;
using TransportComp.Pages;

namespace TransportComp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            new ApplicationContext();
            InitializeComponent();
            SupportObject.mainFrame = MainFrame;
            SupportObject.statusBarMsg = StatusBar;
            SupportObject.mainFrame.Navigate(new PageMain());
        }

        private void BtnBack_OnClick(object sender, RoutedEventArgs e)
        {
            if (!SupportObject.mainFrame.CanGoBack)
                SupplyMethods.SetMessageToStatusBar("You are on the main page!");
            else
            {
                SupportObject.mainFrame.GoBack();
            }
        }

        private void BtnFaq_OnClick(object sender, RoutedEventArgs e)
        {
            SupplyMethods.SetMessageToStatusBar("Work in progress!");
        }
    }
}
