using GuitarLearning_Essentials.SongModel;
using GuitarLearning_Mobile.ApplicationUtility;
using GuitarLearning_Mobile.UtilityClasses;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuitarLearning_Mobile.Pages
{
    /// <summary>
    /// Application page which implements the user interface for the "GuitarLearning" feature.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pgSongPage_Template : ContentPage
    {
        /// <summary>
        /// Musical sheet which is rendered by this instance of the <see cref="pgSongPage_Template"/> class.
        /// </summary>
        /// <value>Gets/Sets Song which is serialised from the "SongName.xml". Default value is <code>null</code>.</value>
        private Song CurrentSong { get; set; } = null;
        /// <summary>
        /// Instance of the UtilityHelper that will handle all the processing.
        /// </summary>
        /// <value>Gets/Sets the UtilityHelper <see cref="UtilityClasses.UtilityHelper"/> field. Default value is <code>null</code>.</value>
        private UtilityHelper UtilityHelper { get; set; } = null;
        /// <summary>
        /// Determines whether the application is currently processing or not.
        /// </summary>
        /// <value>Gets/Sets the IsInUse bool field. Default value is false.</value>
        private bool IsInUse { get; set; } = false;
        /// <summary>
        /// Name of the song to load
        /// </summary>
        /// <value>Gets/Sets the SongName string field</value>
        private string SongName { get; set; }
        /// <summary>
        /// Event that is invoked when <see cref="IsInUse"/> is changed. 
        /// </summary>
        private event EventHandler IsInUseChanged;
        /// <summary>
        /// Constructor
        /// <para>Loads the "SongName.html" from the application's assets.
        /// Loads the "SongName.xml" from the application's assets and serialises it.
        /// Initialises all event bindings and fields.</para>
        /// </summary>
        /// <param name="SongName">Name of the song this instance of <see cref="pgSongPage_Template"/> will use.</param>
        public pgSongPage_Template(string SongName)
        {
            InitializeComponent();

            this.SongName = SongName;

            //Init events
            IsInUseChanged += async (s, arg) => { await AnimationAsync(s, arg); };
            IsInUseChanged += ChangeProcessState;
            IsInUseChanged += ChangeButtonLabel;
        }
        /// <summary>
        /// Subscribed to <see cref="IsInUseChanged"/>. Changes the text of the "Start/Stop"-button.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Eventarguments</param>
        private void ChangeButtonLabel(object sender, EventArgs e)
        {
            if (IsInUse)
            {
                btnProcessState.Text = "Stop";
            }
            else
            {
                btnProcessState.Text = "Start";
                pbProgress.Progress = 0;
            }
        }

        /// <summary>
        /// Subscribed to <see cref="IsInUseChanged"/>. Starts or stops the processing, depending on the value of <see cref="IsInUse"/>.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Eventarguments</param>
        private void ChangeProcessState(object sender, EventArgs e)
        {
            if (IsInUse)
            {
                UtilityHelper.Start(wvTabContainer);
            } 
            else 
            {
                UtilityHelper.Stop();
                Reset();
            }
        }
        /// <summary>
        /// Subscribed to <see cref="IsInUseChanged"/>. Runs asynchronously, runs the "Animation"-javascript functions of the WebView.
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Eventarguments</param>
        /// <returns>Task, so the method can be run asynchronously</returns>
        private async Task AnimationAsync(object sender, EventArgs e)
        {
            try
            {
                if (IsInUse)
                {
                    await wvTabContainer.EvaluateJavaScriptAsync($"Animate()");
                }
                else
                {
                    await wvTabContainer.EvaluateJavaScriptAsync($"AnimationStop()");
                }
            }
            catch(OperationCanceledException ex)
            {
                //Ignore
            }
            catch (Exception ex)
            {
                Logger.Log("SongPage - Error: " + ex.Message);
            }
        }
        /// <summary>
        /// Event that is invoked when the "Start/Stop"-button is clicked.
        /// Changes <see cref="IsInUse"/> and invokes <see cref="IsInUseChanged"/>.
        /// </summary>
        /// <param name="sender">Button that invokes the event.</param>
        /// <param name="e">Eventarguments</param>
        private void OnProcessStateChanged(object sender, EventArgs e)
        {
            IsInUse = !IsInUse;
            IsInUseChanged?.Invoke(null, new EventArgs());
        }
        /// <summary>
        /// Event which is invoked when the user leaves this page.
        /// </summary>
        /// <param name="sender">Page that invoked the event.</param>
        /// <param name="e">Eventarguments</param>
        private void OnLeave(object sender, EventArgs e)
        {
            if(IsInUse)
                UtilityHelper?.Stop();
            IsInUse = false;
            IsInUseChanged?.Invoke(null, new EventArgs());
            Reset();
        }
        private void Reset()
        {
            UtilityHelper?.CleanUp();

            //Init Utility
            UtilityHelper = new UtilityHelper(CurrentSong, pbProgress);
        }
        /// <summary>
        /// Called when the page is entered, loads the *.html from the app assets.
        /// </summary>
        /// <param name="sender">Page that sends the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnEnter(object sender, EventArgs e)
        {
            try
            {
                this.Title = SongName;

                //Get Tabulator-Sheet
                string htmlFile = SongName + ".html";
                string html = string.Empty;
                using (var streamReader = new StreamReader(AssetStorage.Manager.Open(htmlFile)))
                {
                    html = streamReader.ReadToEnd();
                    if (html == "") throw new Exception();
                }

                //Get Info-Sheet
                string xmlFile = SongName + ".xml";
                using (var reader = new StreamReader(AssetStorage.Manager.Open(xmlFile)))
                {
                    var serializer = new XmlSerializer(new Song().GetType());
                    CurrentSong = (Song)serializer.Deserialize(reader);
                    if (CurrentSong == null) throw new Exception();
                }

                HtmlWebViewSource htmlSource = new HtmlWebViewSource();
                htmlSource.Html = html;
                wvTabContainer.Source = htmlSource;
                wvTabContainer.IsEnabled = false;

                //Init Utility
                UtilityHelper = new UtilityHelper(CurrentSong, pbProgress);

                InfoContainer.Page = this;
            }
            catch (Exception ex)
            {
                DisplayAlert("Fehler", "Das Notenblatt ist korrumpiert, bitte wählen Sie ein anderes", "OK");
                Logger.Log("Error: " + ex.Message);
                Navigation.PopAsync();
            }
        }
    }
}