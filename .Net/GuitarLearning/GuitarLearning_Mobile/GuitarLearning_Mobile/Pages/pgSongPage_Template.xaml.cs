using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuitarLearning_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pgSongPage_Template : ContentPage
    {
        public pgSongPage_Template()
        {
            InitializeComponent();

            btnStartAnimation.Clicked += async (s, e) =>
            {
                await Animation();
                //synchroner click handler => löst asynchrones event aus
            };

            string html = string.Empty;
            using (var streamReader = new StreamReader(AssetStorage.Manager.Open("test.html")))
            {
                html = streamReader.ReadToEnd();
            }
            HtmlWebViewSource htmlSource = new HtmlWebViewSource();
            htmlSource.Html = html;
            wvTabContainer.Source = htmlSource;
            wvTabContainer.IsEnabled = false;

            //Laden des Notenblatt.xml
            //Deserialisieren und Speichern
        }

        //Handler für Stop

        //Handler für Start
        // + Init Recorder, Buffer, Helper
        // + Start Recording
        // + Start Animation

        //Handler für NewChords
        // + Häufigkeitsanalyse
        // + Vergleich mit Notenblatt
        // + Markieren?


        private async Task Animation()
        {
            //await ViewExtensions.TranslateTo(wvTabContainer, -2000, wvTabContainer.Y, 6000);
            btnStartAnimation.Text = await wvTabContainer.EvaluateJavaScriptAsync($"Animate()");
        }
    }
}