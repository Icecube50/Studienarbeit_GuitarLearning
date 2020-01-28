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

                //Init events
                IsInUseChanged += async (s, e) => { await AnimationAsync(s, e); };
                IsInUseChanged += ChangeProcessState;
                IsInUseChanged += ChangeButtonLabel;

                //Init Utility
                UtilityHelper = new UtilityHelper(CurrentSong);
            }
            catch (Exception e)
            {
                DisplayAlert("Fehler", "Das Notenblatt ist korrumpiert, bitte wählen Sie ein anderes", "OK");
                Logger.Log("Error: " + e.Message);
                Navigation.PopAsync();
            }
        }

        private void ChangeButtonLabel(object sender, EventArgs e)
        {
            if (IsInUse)
            {
                btnProcessState.Text = "Stop";
            }
            else { btnProcessState.Text = "Start"; }
        }

        private void ChangeProcessState(object sender, EventArgs e)
        {
            if (IsInUse)
            {
                UtilityHelper.Start(wvTabContainer);
            } 
            else 
            {
                UtilityHelper.Stop();
            }
        }

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

        private void OnProcessStateChanged(object sender, EventArgs e)
        {
            IsInUse = !IsInUse;
            IsInUseChanged?.Invoke(null, new EventArgs());
        }

        private void OnLeave(object sender, EventArgs e)
        {
            if(IsInUse)
                UtilityHelper.Stop();
            UtilityHelper.CleanUp();
        }
    }
}