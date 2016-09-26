using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Frontend;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class P_TeamManagement : Page
    {
        public P_TeamManagement()
        {
            this.InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App._SelectedBoardId = 1;
            lbx_users.ItemsSource = await BoardRequests.getAllUserFromBoard(App._Email, App._VerificationKey, App._SelectedBoardId);
        }

        private async void abb_add_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new D_AddUserToBoard();
            await dialog.ShowAsync();

            lbx_users.ItemsSource = await BoardRequests.addUserToBoard(App._Email, App._VerificationKey, 1, App._DialogHelper);
            App._DialogHelper = "";
        }

        private async void abb_delete_Click(object sender, RoutedEventArgs e)
        {
            if (!(lbx_users.SelectedItem is BoardUser)) return;
            lbx_users.ItemsSource = await BoardRequests.removeUserFromBoard(App._Email, App._VerificationKey, 1, ((BoardUser)lbx_users.SelectedItem).UserEMail);
        }

        private void abb_select_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_users.SelectedItem == null) return;
            //TODO send request to make user admin
        }

        private void abb_leaveGroup_Click(object sender, RoutedEventArgs e)
        {
            //TODO Send request to delete User from Group
        }

        private void abb_deleteGroupd_Click(object sender, RoutedEventArgs e)
        {
            //TODO Send request to delete the whole Group ( == revoke access to Board for all Users)
        }
    }
}
