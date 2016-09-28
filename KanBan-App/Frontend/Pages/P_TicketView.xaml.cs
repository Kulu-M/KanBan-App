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
    public sealed partial class P_TicketView : Page
    {
        private object navParam = null;
        public int from = -1;
        public Note note = null;

        public P_TicketView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var test = e.Parameter as dynamic;
            from = test.from;
            note = test.note;
            this.DataContext = note;

            switch (from)
            {
                case 0:
                    rb_toDo.IsChecked = true;
                    break;
                case 1:
                    rb_inProgress.IsChecked = true;
                    break;
                case 2:
                    rb_done.IsChecked = true;
                    break;
                default:
                    break;
            }

        }


        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void rb_toDo_Checked(object sender, RoutedEventArgs e)
        {
            switch (from)
            {
                case 1:
                    App._inProgressNotes.Remove(note);
                    App._toDoNotes.Add(note);
                    break;
                case 2:
                    App._doneNotes.Remove(note);
                    App._toDoNotes.Add(note);
                    break;
                default:
                    break;
            }
        }

        private void rb_inProgress_Checked(object sender, RoutedEventArgs e)
        {
            switch (from)
            {
                case 0:
                    App._toDoNotes.Remove(note);
                    App._inProgressNotes.Add(note);
                    break;
                case 2:
                    App._toDoNotes.Remove(note);
                    App._doneNotes.Add(note);
                    break;
                default:
                    break;
            }
        }

        private void rb_done_Checked(object sender, RoutedEventArgs e)
        {
            switch (from)
            {
                case 0:
                    App._toDoNotes.Remove(note);
                    App._doneNotes.Add(note);
                    break;
                case 1:
                    App._inProgressNotes.Remove(note);
                    App._doneNotes.Add(note);
                    break;
                default:
                    break;
            }
        }
    }
}
