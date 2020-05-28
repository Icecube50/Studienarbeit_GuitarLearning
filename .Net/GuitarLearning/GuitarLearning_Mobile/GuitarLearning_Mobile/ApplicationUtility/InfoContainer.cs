using GuitarLearning_Mobile.Pages;
using GuitarLearning_Mobile.UtilityClasses;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace GuitarLearning_Mobile.ApplicationUtility
{
    /// <summary>
    /// Class handling the action of displaying the analysis result to the user
    /// </summary>
    public static class InfoContainer
    {
        private static int CorrectNotes = 0;
        /// <summary>
        /// Page on which the Message will be displayed.
        /// </summary>
        /// <value>Gets/Sets the Page field</value>
        public static pgSongPage_Template Page { private get; set; }

        /// <summary>
        /// Create a result string and show it in an Alert.
        /// </summary>
        public static void DisplayResults()
        {
            var elapsed = TimeHelper.GetElapsedTime_Analysis();
            string infoMsg = "Sie haben " + CorrectNotes + " von " + SongHelper.NumberOfNotes + " Noten getroffen!";
            infoMsg += "\r\nDie Analyse hat " + elapsed + " ms gedauert.";

            MainThread.BeginInvokeOnMainThread(() =>
            {
               Page.DisplayAlert("Ergebnis:", infoMsg, "OK");
            });

            CorrectNotes = 0;
        }

        /// <summary>
        /// Increase the correct note counter by one. 
        /// </summary>
        public static void HitNote()
        {
            CorrectNotes++;
        }
    }
}
