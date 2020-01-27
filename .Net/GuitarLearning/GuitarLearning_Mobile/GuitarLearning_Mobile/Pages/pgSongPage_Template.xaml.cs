using GuitarLearning_Essentials.SongModel;
using GuitarLearning_Mobile.UtilityClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuitarLearning_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pgSongPage_Template : ContentPage
    {
        private Song CurrentSong { get; set; } = null;
        private UtilityHelper UtilityHelper { get; set; } = null;
        private bool IsInUse { get; set; } = false;

        private event EventHandler IsInUseChanged;
        public pgSongPage_Template(string SongName)
        {
            InitializeComponent();

            this.Title = SongName;
            try
            {
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
                    var serializer = new XmlSerializer(typeof(Song));
                    CurrentSong = (Song)serializer.Deserialize(reader);
                    if (CurrentSong == null) throw new Exception();
                }

                HtmlWebViewSource htmlSource = new HtmlWebViewSource();
                htmlSource.Html = html;
                wvTabContainer.Source = htmlSource;
                wvTabContainer.IsEnabled = false;
            }
            catch (Exception e)
            {
                DisplayAlert("Fehler", "Das Notenblatt ist korrumpiert, bitte wählen Sie ein anderes", "OK");
                Navigation.PopAsync();
            }

            //Init events
            IsInUseChanged += async (s, e) => { await Animation(); };
            IsInUseChanged += ChangeProcessState;

            //Init Utility
            UtilityHelper = new UtilityHelper(CurrentSong);
            UtilityHelper.JavaScript_HighlightChord += async (s, e) => { await HighlightChord(s); };
            UtilityHelper.JavaScript_HighlightNote += async (s, e) => { await HighlightNote(s); };
        }

        private void ChangeProcessState(object sender, EventArgs e)
        {
            if (IsInUse) UtilityHelper.Start();
            else UtilityHelper.Stop();
        }

        private async Task Animation()
        {
            if (IsInUse) await wvTabContainer.EvaluateJavaScriptAsync($"Animate()");
            else await wvTabContainer.EvaluateJavaScriptAsync($"AnimationStop()");
        }

        private async Task HighlightChord(object model)
        {
            var songObj = model as SongObject;
            string scriptName = $"HighlightCorrectChord('" + songObj.WebId +"')";
            await wvTabContainer.EvaluateJavaScriptAsync(scriptName);
        }

        private async Task HighlightNote(object model)
        {
            var songObj = model as SongObject;
            string scriptName = $"HighlightCorrectNote('" + songObj.WebId + "')";
            await wvTabContainer.EvaluateJavaScriptAsync(scriptName);
        }

        private void OnProcessStateChanged(object sender, EventArgs e)
        {
            IsInUse = !IsInUse;
            if (IsInUse)
            {
                btnProcessState.Text = "Stop";
            }
            else { btnProcessState.Text = "Start"; }
            IsInUseChanged?.Invoke(null, new EventArgs());
        }

        private void OnLeave(object sender, EventArgs e)
        {
            UtilityHelper.CleanUp();
        }
    }
}