using GuitarLearning_Essentials.SongModel;
using GuitarLearning_Mobile.DeveloperSupport;
using System.Threading;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GuitarLearning_Mobile.UtilityClasses
{
    /// <summary>
    /// Manages <see cref="UtilityClasses.AudioRecording"/>, <see cref="UtilityClasses.AudioBuffer"/>, <see cref="UtilityClasses.API_Helper"/>,
    /// <see cref="UtilityClasses.ResultBuffer"/> and <see cref="UtilityClasses.DataAnalyzer"/>.
    /// Provides a simple interface with which the whole recording -> api -> analysing process can be controlled.
    /// </summary>
    public class UtilityHelper
    {
        /// <summary>
        /// Instance of the AudioRecording to be used.
        /// </summary>
        /// <value>Gets/Sets the AudioRecording <see cref="UtilityClasses.AudioRecording"/> field. Default value is <code>null</code>.</value>
        private AudioRecording AudioRecording { get; set; } = null;
        /// <summary>
        /// Instance of the AudioBuffer to be used.
        /// </summary>
        /// <value>Gets/Sets the AudioBuffer <see cref="UtilityClasses.AudioBuffer"/> field. Default value is <code>null</code>.</value>
        private AudioBuffer AudioBuffer { get; set; } = null;
        /// <summary>
        /// Instance of the API_Helper to be used.
        /// </summary>
        /// <value>Gets/Sets the API_Helper <see cref="UtilityClasses.API_Helper"/> field. Default value is <code>null</code>.</value>
        private API_Helper API_Helper { get; set; } = null;
        /// <summary>
        /// Instance of the ResultBuffer to be used.
        /// </summary>
        /// <value>Gets/Sets the ResultBuffer <see cref="UtilityClasses.ResultBuffer"/> field. Default value is <code>null</code>.</value>
        private ResultBuffer ResultBuffer { get; set; } = null;
        /// <summary>
        /// Instance of the DataAnalyzer to be used.
        /// </summary>
        /// <value>Gets/Sets the DataAnalyzer <see cref="UtilityClasses.DataAnalyzer"/> field. Default value is <code>null</code>.</value>
        private DataAnalyzer DataAnalyzer { get; set; } = null;
        /// <summary>
        /// Token that is used to cancel all running tasks.
        /// </summary>
        private CancellationTokenSource cts;
        /// <summary>
        /// Progress Bar which will display the current progress
        /// </summary>
        private ProgressBar _progressBar;
        /// <summary>
        /// Constructor
        /// <para>Initialises all fields.</para>
        /// </summary>
        /// <param name="song">Song that was serialised from the "SongName.xml".</param>
        public UtilityHelper(Song song, ProgressBar progressBar)
        {
            //Init Buffer
            AudioBuffer = new AudioBuffer();
            ResultBuffer = new ResultBuffer();

            //Init Classes
            AudioRecording = new AudioRecording(AudioBuffer);
            API_Helper = new API_Helper(AudioBuffer, ResultBuffer);
            DataAnalyzer = new DataAnalyzer(ResultBuffer);

            SongHelper.InitHelper(song);
            SongHelper.ProgressMade += OnProgressMade;

            _progressBar = progressBar;

            cts = new CancellationTokenSource();
        }

        /// <summary>
        /// Update the current progress
        /// </summary>
        /// <param name="sender">string, new progress information</param>
        /// <param name="e">Eventarguments</param>
        private void OnProgressMade(object sender, System.EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                _progressBar.Progress = (float)sender;
            });
        }

        /// <summary>
        /// Start the all processes.
        /// </summary>
        /// <param name="webView">WebView which renders the music sheet.</param>
        public void Start(WebView webView)
        {
            if (DevFlags.LoggingEnabled) Logger.Log("Starting now");
            TimeHelper.SetStartTime();

            AudioRecording.StartRecording();
            API_Helper.StartAPI();
            DataAnalyzer.AnalyseAsync(cts.Token, webView);
        }
        /// <summary>
        /// Stop all processes.
        /// </summary>
        public void Stop()
        {
            AudioRecording.StopRecording();
            API_Helper.StopAPI();
            cts.Cancel();

            EmptyBuffer();
        }
        /// <summary>
        /// Clean both buffers.
        /// </summary>
        private void EmptyBuffer()
        {
            AudioBuffer.Clean();
            ResultBuffer.Clean();
        }
        /// <summary>
        /// Generic Cleanup
        /// </summary>
        public void CleanUp()
        {
            AudioRecording.CleanUp();
        }
    }
}
