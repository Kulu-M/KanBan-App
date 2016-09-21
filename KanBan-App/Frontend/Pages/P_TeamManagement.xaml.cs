using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class P_TeamManagement : Page
    {
        public P_TeamManagement()
        {
            this.InitializeComponent();

            var userList = new ObservableCollection<User>();
            userList.Add(new User{ EMail = "testmail", Password = "123", VerificationKey = "key"});
            userList.Add(new User{ EMail = "testmail2", Password = "123", VerificationKey = "key" });
            userList.Add(new User{ EMail = "testmail3", Password = "123", VerificationKey = "key" });

            lbx_users.ItemsSource = userList;
        }

        private void abb_add_Click(object sender, RoutedEventArgs e)
        {
            Popup pop = new Popup();
            pop.IsOpen = true;
        }
    }
}
