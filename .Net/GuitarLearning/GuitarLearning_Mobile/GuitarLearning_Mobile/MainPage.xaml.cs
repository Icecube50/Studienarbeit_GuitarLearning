using Android.Content.Res;
using GuitarLearning_Mobile.DeveloperSupport;
using GuitarLearning_Mobile.Pages;
using Plugin.Connectivity;
using System;
using System.ComponentModel;
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

            var tapGestureRecognizerAudio = new TapGestureRecognizer();
            tapGestureRecognizerAudio.Tapped += OnAudioContainer_Tapped;
            grdAudio.GestureRecognizers.Add(tapGestureRecognizerAudio);

            var tapGestureRecognizerNetwork = new TapGestureRecognizer();
            tapGestureRecognizerNetwork.Tapped += OnNetworkContainer_Tapped;
            grdNetwork.GestureRecognizers.Add(tapGestureRecognizerNetwork);

            var tapGestureRecognizerTestSong1 = new TapGestureRecognizer();
            tapGestureRecognizerTestSong1.Tapped += OnTestSong1_Tapped;
            grdTestSong1.GestureRecognizers.Add(tapGestureRecognizerTestSong1);

            var tapGestureRecognizerTestSong2 = new TapGestureRecognizer();
            tapGestureRecognizerTestSong2.Tapped += OnTestSong2_Tapped;
            grdTestSong2.GestureRecognizers.Add(tapGestureRecognizerTestSong2);

            Logger.Log("########### New Startup ############");
        }

        /// <summary>
        /// Event that is invoked when the user tapps the menu point: TestSong1.
        /// The internet connection is checked and if the device is connected to the internet
        /// the new page is pushed onto the navigation stack.
        /// </summary>
        /// <param name="sender">Grid that invokes the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnTestSong1_Tapped(object sender, EventArgs e)
        {
            if (IsAllowedToLoadSong())
                Navigation.PushAsync(new pgSongPage_Template("Test1"));
        }

        /// <summary>
        /// Event that is invoked when the user tapps the menu point: TestSong2.
        /// The internet connection is checked and if the device is connected to the internet
        /// the new page is pushed onto the navigation stack.
        /// </summary>
        /// <param name="sender">Grid that invokes the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnTestSong2_Tapped(object sender, EventArgs e)
        {
            if (IsAllowedToLoadSong())
                Navigation.PushAsync(new pgSongPage_Template("Test2"));
        }

        /// <summary>
        /// Event that is invoked when the user tapps the menu point: Network Testing.
        /// The internet connection is checked and if the device is connected to the internet
        /// the new page is pushed onto the navigation stack.
        /// </summary>
        /// <param name="sender">Grid that invokes the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnNetworkContainer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new pgNetworkTesting());
        }

        /// <summary>
        /// Event that is invoked when the user tapps the menu point: Audio Recording.
        /// The internet connection is checked and if the device is connected to the internet
        /// the new page is pushed onto the navigation stack.
        /// </summary>
        /// <param name="sender">Grid that invokes the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnAudioContainer_Tapped(object sender, EventArgs e)
        {
            if (IsAllowedToLoadSong())
                Navigation.PushAsync(new pgAudioRecording());
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
            if (!ConnectionChecker.CanReachApiAt())
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
    }

    /// <summary>
    /// Container class that is used to store a reference to the application's assets so that they can be used later.
    /// </summary>
    public static class AssetStorage
    {
        /// <summary>
        /// AssetManager that points to the application's assets.
        /// </summary>
        /// <value>The Manager property gets/sets the AssetManager field</value>
        public static AssetManager Manager { get; set; }
    }
}
