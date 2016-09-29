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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (App._loaded) return;
            App._SelectedBoardId = 1;
            App._toDoNotes = await BoardRequests.getAllNotesFromBoard(App._Email, App._VerificationKey, App._SelectedBoardId);
            updateItemSources();
            App._loaded = true;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            updateItemSources();
        }

        private void updateItemSources()
        {
            lbx_toDo.ItemsSource = App._toDoNotes;
            lbx_inProgress.ItemsSource = App._inProgressNotes;
            lbx_done.ItemsSource = App._doneNotes;
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
            var param = new { from = 0, note = lbx_toDo.SelectedItem };
            Frame.Navigate(typeof(P_TicketView), param);
        }

        private async void abb_delete_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_toDo.SelectedItem == null) return;
            var noteToDelete = (lbx_toDo.SelectedItem as Note);
            App._toDoNotes = await BoardRequests.deleteNote(App._Email, App._VerificationKey, noteToDelete);
            lbx_toDo.ItemsSource = App._toDoNotes;
            //Sende Request an server. update lbx source
        }

        private async void abb_add_Click(object sender, RoutedEventArgs e)
        {
            App._toDoNotes = await BoardRequests.createNewNote(App._Email, App._VerificationKey, 1);
            lbx_toDo.ItemsSource = App._toDoNotes;
            //Sende Request Erstelle neues Ticket, navigiere zu ticket ansicht vom ticket.

            //Frame.Navigate(typeof(P_TicketView));
        }

        private void abb_register_Click(object sender, RoutedEventArgs e)
        {
            //Sende Request um sich für ein Ticket anzumelden. 
        }

        private void abb_view_Click(object sender, RoutedEventArgs e)
        {
            var param = new { from = 1, note = lbx_inProgress.SelectedItem };
            Frame.Navigate(typeof(P_TicketView), param);
        }

        private async void abb_assign_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new D_ToDo_Assign();
            await dialog.ShowAsync();
        }

        private void abb_viewDone_Click(object sender, RoutedEventArgs e)
        {
            var param = new { from = 2, note = lbx_done.SelectedItem };
            Frame.Navigate(typeof(P_TicketView), param);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(P_TeamManagement));
        }
    }
}
