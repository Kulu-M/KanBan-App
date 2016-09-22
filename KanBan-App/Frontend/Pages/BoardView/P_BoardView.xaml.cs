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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Frontend
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class P_BoardView : Page
    {
        public P_BoardView()
        {
            this.InitializeComponent();
        }

        private void piv_tickets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (piv_tickets.SelectedIndex)
            {
                case 0:
                    abb_edit.Visibility = Visibility.Visible;
                    abb_delete.Visibility = Visibility.Visible;
                    abb_add.Visibility = Visibility.Visible;

                    abb_register.Visibility = Visibility.Collapsed;
                    abb_view.Visibility = Visibility.Collapsed;
                    abb_assign.Visibility = Visibility.Collapsed;

                    abb_viewDone.Visibility = Visibility.Collapsed;
                    abb_archive.Visibility = Visibility.Collapsed;
                    break;
                case 1:
                    abb_edit.Visibility = Visibility.Collapsed;
                    abb_delete.Visibility = Visibility.Collapsed;
                    abb_add.Visibility = Visibility.Collapsed;

                    abb_register.Visibility = Visibility.Visible;
                    abb_view.Visibility = Visibility.Visible;
                    abb_assign.Visibility = Visibility.Visible;

                    abb_viewDone.Visibility = Visibility.Collapsed;
                    abb_archive.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    abb_edit.Visibility = Visibility.Collapsed;
                    abb_delete.Visibility = Visibility.Collapsed;
                    abb_add.Visibility = Visibility.Collapsed;

                    abb_register.Visibility = Visibility.Collapsed;
                    abb_view.Visibility = Visibility.Collapsed;
                    abb_assign.Visibility = Visibility.Collapsed;

                    abb_viewDone.Visibility = Visibility.Visible;
                    abb_archive.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }
        }

        private void abb_edit_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(P_TicketView), lbx_toDo.SelectedItem);
        }

        private void abb_delete_Click(object sender, RoutedEventArgs e)
        {
            //Sende Request an server. update lbx source
        }

        private async void abb_add_Click(object sender, RoutedEventArgs e)
        {
            lbx_toDo.ItemsSource = await BoardRequests.createNewNote("lloyd@web.de", "3", 1);

            //Sende Request Erstelle neues Ticket, navigiere zu ticket ansicht vom ticket.

            //Frame.Navigate(typeof(P_TicketView));
        }

        private void abb_register_Click(object sender, RoutedEventArgs e)
        {
            //Sende Request um sich für ein Ticket anzumelden. 
        }

        private void abb_view_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(P_TicketView), lbx_inProgress.SelectedItem);
        }

        private async void abb_assign_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new D_ToDo_Assign();
            await dialog.ShowAsync();
        }
    }
}
