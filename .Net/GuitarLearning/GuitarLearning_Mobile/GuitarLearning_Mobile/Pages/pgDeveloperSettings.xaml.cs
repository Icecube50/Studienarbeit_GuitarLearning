using GuitarLearning_Mobile.DeveloperSupport;
using GuitarLearning_Mobile.UtilityClasses;
using System;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuitarLearning_Mobile.Pages
{
    /// <summary>
    /// This Xamarin.Forms page is used to provide a GUI with which the developer fields in <see cref="DevFlags"/> can be changed.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pgDeveloperSettings : ContentPage
    {
        /// <summary>
        /// Constructor
        /// <para>Gets the default value from <see cref="DevFlags.Deviation"/> and sets it as
        /// placeholder text to the corresponding entry.</para>
        /// </summary>
        public pgDeveloperSettings()
        {
            InitializeComponent();

            etyDeviation.Placeholder = Convert.ToString(DevFlags.Deviation);
            cckLogging.IsChecked = DevFlags.LoggingEnabled;
            cckForcePermissions.IsChecked = DevFlags.ForcePermissionRequest;
            cckNetworkChecks.IsChecked = DevFlags.SkipNetworkChecks;
            etyDeviation.Text = Convert.ToString(DevFlags.Deviation);
            etyBufferSize.Text = Convert.ToString(AudioBuffer.BUFFER_SIZE);
        }

        /// <summary>
        /// Event that is called when the user changes the checked state of the EnableLogging checkbox.
        /// Updates the flag in <see cref="DevFlags.LoggingEnabled"/>
        /// </summary>
        /// <param name="sender">Checkbox that invokes the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnDoLoggingChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked) DevFlags.LoggingEnabled = true;
            else DevFlags.LoggingEnabled = false;
        }

        /// <summary>
        /// Event that is called when the user confirms the changes made.
        /// Updates the <see cref="DevFlags.Deviation"/> filed with the new value.
        /// When the entered value is not a number, a DisplayAlter is shown to the user.
        /// </summary>
        /// <param name="sender">Button that invokes the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnApllyClicked(object sender, EventArgs e)
        {
            try
            {
                string unsafeNumber = etyDeviation.Text;
                if (Regex.IsMatch(unsafeNumber, "^[0-9]+$"))
                {
                    int safeNumber = Convert.ToInt32(unsafeNumber);
                    DevFlags.Deviation = safeNumber;
                }
                else { DisplayAlert("Error", "Bitte wählen Sie eine ganze Zahl", "OK"); }

                unsafeNumber = etyBufferSize.Text;
                if (Regex.IsMatch(unsafeNumber, "^[0-9]+$"))
                {
                    int safeNumber = Convert.ToInt32(unsafeNumber);
                    AudioBuffer.BUFFER_SIZE = safeNumber;
                }
                else { DisplayAlert("Error", "Bitte wählen Sie eine ganze Zahl", "OK"); }
            }
            catch
            {
                DisplayAlert("Error", "Etwas ist schief gelaufen", "OK");
            }
        }
        /// <summary>
        /// Event that is called when the user clicks the "DeleteLogs"-button. All log-files will be deleted.
        /// </summary>
        /// <param name="sender">Button that invoked the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnDeleteLogsClicked(object sender, EventArgs e)
        {
            Logger.CleanLogs();
        }
        /// <summary>
        /// Event that is called when the user changes the checked state of the SkipNetworkChecks-Checkbox.
        /// The corresponding flag in <see cref="DevFlags"/> will be set.
        /// </summary>
        /// <param name="sender">Checkbox that sends the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnNetworkChecksChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked) DevFlags.SkipNetworkChecks = true;
            else DevFlags.SkipNetworkChecks = false;
        }
        /// <summary>
        /// Event that is called when the user changes the state of the ForcePermissions-Checkbox.
        /// The corresponding flag in <see cref="DevFlags"/> will be set.
        /// </summary>
        /// <param name="sender">Checkbox that sends the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnForcePermissionsChanged(object sender, CheckedChangedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (checkBox.IsChecked) DevFlags.ForcePermissionRequest = true;
            else DevFlags.ForcePermissionRequest = false;
        }
        /// <summary>
        /// Event that is called when the user clicks the "GetPermissions"-Button. Forces a new request for all permissions.
        /// </summary>
        /// <param name="sender">Button that invoked the event</param>
        /// <param name="e">Eventarguments</param>
        private void OnGetPermissionsClicked(object sender, EventArgs e)
        {
            PermissionHelper.AskForAllPermissions();
        }
    }
}