using Android.Media;
using GuitarLearning_Mobile.DeveloperSupport;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.UtilityClasses
{
    /// <summary>
    /// Implements the methods that are used to record new audio data.
    /// </summary>
    public class AudioRecording
    {
        /// <summary>
        /// Android.AudioRecord, implements the access to the microphone of an android device. The AudioRecorder property stores the AudioRecord once it's configured.
        /// </summary>
        /// <value>Gets/Sets the AudioRecorder property. Default value is <code>null</code></value>
        private AudioRecord AudioRecorder { get; set; } = null;
        /// <summary>
        /// Temporary buffer, used to store the raw audio data during recording.
        /// </summary>
        /// <value>Gets/Sets the InputBuffer float[] field.</value>
        private float[] InputBuffer { get; set; } = null;
        /// <summary>
        /// Instance of the thread-safe <see cref="UtilityClasses.AudioBuffer"/>, stores the audio data until it is processed./>
        /// </summary>
        /// <value>Gets/Sets the AudioBuffer AudioBuffer field.</value>
        private AudioBuffer AudioBuffer { get; set; } = null;
        /// <summary>
        /// Token that can be used to cancel all running tasks.
        /// </summary>
        CancellationTokenSource cts;
        /// <summary>
        /// Constructor
        /// <para>Initialises the <see cref="AudioRecorder"/> and sets its parameters, also initialises the <see cref="InputBuffer"/> and the <see cref="cts"/>.</para>
        /// </summary>
        /// <param name="audioBuffer">Instance of the <see cref="UtilityClasses.AudioBuffer"/> to use.</param>
        public AudioRecording(AudioBuffer audioBuffer)
        {
            InputBuffer = new float[AudioBuffer.BUFFER_SIZE];
            AudioRecorder = new AudioRecord(
                AudioSource.Mic,
                44100,
                ChannelIn.Mono,
                Android.Media.Encoding.PcmFloat,
                InputBuffer.Length
                );

            AudioBuffer = audioBuffer;
            cts = new CancellationTokenSource();
        }
        /// <summary>
        /// Start the asynchronous recording.
        /// </summary>
        public void StartRecording()
        {
            Task.Run(async () =>
            {
                try
                {
                    if (DevFlags.LoggingEnabled) Logger.RecordingLog("Start Recording");
                    await ReadDataToBuffer(cts.Token);
                }
                catch (OperationCanceledException e)
                {
                    //Ignore, just return
                    if (DevFlags.LoggingEnabled) Logger.RecordingLog("Recording was cancelled");
                }
                catch (Exception e)
                {
                    Logger.Log("Recorder - Error: " + e.Message);
                }
            });
        }
        /// <summary>
        /// Set the state of <see cref="cts"/> to cancel, which will stop all running tasks.
        /// </summary>
        public void StopRecording()
        {
            if (!cts.IsCancellationRequested)
            {
                cts.Cancel();
            }
        }
        /// <summary>
        /// Frees the memory used by the <see cref="AudioRecorder"/> and empties the buffer.
        /// </summary>
        public void CleanUp()
        {
            SafeGetRecorder()?.Release();
            AudioBuffer?.Clean();
        }

        /// <summary>
        /// Mutext, just in case the access to the <see cref="AudioRecorder"/> is multitasked.
        /// </summary>
        private static readonly object _lock = new object();
        private AudioRecord SafeGetRecorder()
        {
            lock(_lock)
            {
                return this.AudioRecorder;
            }
        }
        /// <summary>
        /// Records audio data until the temporary buffer is full. The data is then added to the <see cref="AudioBuffer"/>/>
        /// </summary>
        /// <param name="ct">Token that throws an OperationCancelledException when the token state is set to cancel. Used to stop the task.</param>
        /// <returns>Task, so the method can be run asynchronously</returns>
        private async Task ReadDataToBuffer(CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            SafeGetRecorder().StartRecording();
            while (!ct.IsCancellationRequested)
            {
                if (DevFlags.LoggingEnabled) Logger.RecordingLog("Looping");
                await SafeGetRecorder().ReadAsync(InputBuffer, 0, InputBuffer.Length, 0);
                AudioBuffer?.Add(new AudioData(InputBuffer, TimeHelper.GetElapsedTime()));
                InputBuffer = new float[InputBuffer.Length];
            }
            SafeGetRecorder()?.Stop();
            ct.ThrowIfCancellationRequested();
        }

    }
}
