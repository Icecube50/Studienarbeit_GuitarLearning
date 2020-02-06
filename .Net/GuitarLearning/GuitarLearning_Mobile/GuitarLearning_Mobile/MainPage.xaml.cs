using Android.Content.Res;
using GuitarLearning_Mobile.ApplicationUtility;
using GuitarLearning_Mobile.DeveloperSupport;
using GuitarLearning_Mobile.Pages;
using Plugin.Connectivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuitarLearning_Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    /// <summary>
    /// Main Page that is shown after the start of the apllication. Contains menut to
    /// navigate to the other pages of the app.
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        /// <summary>
        /// Constructor
        /// <para>Initialises TapGestureRecognizers and binds them to the menu points.</para>
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            var listOfSongs = new List<ItemModel>();

            listOfSongs.Add(new ItemModel("Network Testing", new pgNetworkTesting()));
            listOfSongs.Add(new ItemModel("Audio Recording", new pgAudioRecording()));
            listOfSongs.Add(new ItemModel("Note Test", new pgSongPage_Template("Test1")));
            listOfSongs.Add(new ItemModel("Chord Test", new pgSongPage_Template("ChordTest")));
            listOfSongs.Add(new ItemModel("Trumpet Test", new pgSongPage_Template("TrumpetTest")));
            lvContainer.ItemsSource = listOfSongs;

            Logger.Log("########### New Startup ############");
        }

        /// <summary>
        /// Event that is invoked when the user clicks the settings-button.
        /// Pushes the settings-page onto the navigation stack.
        /// </summary>
        /// <param name="sender">Button that invokes the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnSettingsClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new pgDeveloperSettings());
        }

        /// <summary>
        /// Ask the user for permissions
        /// </summary>
        /// <param name="sender">Page that invoked the event</param>
        /// <param name="e">Eventarguments</param>
        private void AfterLoad(object sender, EventArgs e)
        {
            PermissionHelper.AskForAllPermissions();
        }
        /// <summary>
        /// Check whether all prerequisites are met. 
        /// </summary>
        /// <returns>true: when everything is alright and the next page can be loaded, otherwise false.</returns>
        private bool IsAllowedToLoadSong()
        {
            if (!ConnectionChecker.HasConnectionToNetwork())
            {
                NotifyUser("Stellen Sie sicher das Ihr Gerät mit dem Internet verbunden ist.");
                return false;
            }
            var result = Task.Run(async () =>
            {
                return await ConnectionChecker.CanReachApiAt();
            });
            result.Wait();
            if (!result.Result)
            {
                NotifyUser("Der Server ist zurzeit nicht erreichbar. Versuchen Sie es später erneut.");
                return false;
            }

            return true;
        }
        /// <summary>
        /// Notify the user about the application state by showing a DisplayAlert containing the message.
        /// </summary>
        /// <param name="msg">Message that is shown to the user.</param>
        private void NotifyUser(string msg)
        {
            DisplayAlert("Fehler", msg, "OK");
        }
        /// <summary>
        /// Is called when an item in the ListView is tapped. Gets the corresponding page to the item and updates the navigation stack.
        /// </summary>
        /// <param name="sender">ListView that invoked the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (IsAllowedToLoadSong())
            {
                if(e.Item is ItemModel)
                {
                    var item = e.Item as ItemModel;
                    Navigation.PushAsync(item.GetPage());
                }
            }
        }
    }
}
