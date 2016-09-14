using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using System.Net;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class P_Register : Page
    {
        public P_Register()
        {
            InitializeComponent();
        }

        private async void b_registrieren_Click(object sender, RoutedEventArgs e)
        {
            string email = tbx_email.Text;
            string pw = pb_pw.Password;

            string result = await MyWebRequests.requestRegister(email, pw);

            switch (result)
            {
                case "You are now registered!":
                    App._VerificationKey = await MyWebRequests.requestLogin(email, pw);
                    Frame.Navigate(typeof(P_MainPage));
                    break;
                default:
                    tblk_error.Text = result;
                    tblk_error.Visibility = Visibility.Visible;
                    break;
            }
        }
    }
}
