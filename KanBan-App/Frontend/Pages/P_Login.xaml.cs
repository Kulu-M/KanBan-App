using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Frontend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class P_Login : Page
    {
        public P_Login()
        {
            this.InitializeComponent();
        }

        private async void b_login_Click(object sender, RoutedEventArgs e)
        {
            string email = tbx_email.Text;
            string pw = pb_pw.Password;
            string result = string.Empty;

            try
            {
                result = await UserRequests.loginUser(email, pw);
            }
            catch (Exception ex)
            {
                tblk_error.Text = ex.Message;
                return;
            }

            switch (result)
            {
                case "User not registered!":
                case "Wrong password!":
                    tblk_error.Text = result;
                    break;
                default:
                    App._VerificationKey = result;
                    App._Email = email;
                    Frame.Navigate(typeof(P_MainPage));
                    break;
            }
        }

        private void tbx_email_and_pb_pw_TextChanged<T>(object sender, T e)
        {
            string changed = string.Empty;
            string unchanged = string.Empty;
            if (sender.GetType() == typeof(PasswordBox))
            {
                changed = (sender as PasswordBox).Password;
                unchanged = tblk_login.Text;
            }
            else if (sender.GetType() == typeof(TextBox))
            {
                changed = (sender as TextBox).Text;
                unchanged = pb_pw.Password;
            }

            if (changed != null && unchanged != null)
            {
                if (changed != string.Empty && unchanged != string.Empty)
                {
                    b_login.IsEnabled = true;
                }
                else b_login.IsEnabled = false;
            }
            else b_login.IsEnabled = false;
        }

        private void b_registrieren_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(P_Register));
        }
    }
}
