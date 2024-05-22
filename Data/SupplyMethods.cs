using System;
using System.Threading.Tasks;

namespace TransComp.Data
{

    public class SupplyMethods
    {
        public static async void SetMessageToStatusBar(string message)
        {
            SupportObject.statusBarMsg.Text = message;
            await Task.Delay(3000);
            SupportObject.statusBarMsg.Text = String.Empty;
        }
    }
}