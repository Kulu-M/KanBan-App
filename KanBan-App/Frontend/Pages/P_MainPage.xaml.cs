using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
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
    public sealed partial class P_MainPage : Page
    {
        public static string data = string.Empty;


        public P_MainPage()
        {
            this.InitializeComponent();
        }

        private async void abb_delete_Click(object sender, RoutedEventArgs e)
        {
            if (lbx_boards.SelectedIndex != -1)
            {
                bool result = false;

                try
                {
                    result = await MyWebRequests.requestIsBoardAdmin("blub","blub");
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }

                if (result)
                {
                    var dialog = new MessageDialog("Möchten Sie \"" + (lbx_boards.SelectedItem as JObject).GetValue("ID") + "\" wirklich löschen?","Sind Sie sicher?");

                    dialog.Commands.Add(new UICommand("Ja", new UICommandInvokedHandler(CommandInvokeHandler)) { Id = 0 });
                    dialog.Commands.Add(new UICommand("Nein") { Id = 1 });

                    dialog.DefaultCommandIndex = 1;
                    dialog.CancelCommandIndex = 1;

                    var diaResult = dialog.ShowAsync();
                }
                else
                {
                    await new MessageDialog("User is not Board-Admin.").ShowAsync();
                }
            }
        }

        private void CommandInvokeHandler(IUICommand command)
        {
            bool result = false;
            try
            {
                //result = await MyWebRequests.requestBoardDelete()
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            lbx_boards.ItemsSource = await BoardRequests.createNewBoard(App._Email, App._VerificationKey, "");

            //var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Data/Timmy's Boards.txt"));

            //using (var inputStream = await file.OpenReadAsync())
            //using (var classicStream = inputStream.AsStreamForRead())
            //using (var streamReader = new StreamReader(classicStream))
            //{
            //    data = streamReader.ReadToEnd();
            //}
            //var test = JsonConvert.DeserializeObject(data);

            //lbx_boards.ItemsSource = test;

            ////lbx_boards.DisplayMemberPath = "[0][0]";

        }

        private async void abb_add_Click(object sender, RoutedEventArgs e)
        {
            lbx_boards.ItemsSource = await BoardRequests.createNewBoard(App._Email, App._VerificationKey, "NewBoard");
        }

        private void abb_select_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(P_BoardView));
        }
    }
}
