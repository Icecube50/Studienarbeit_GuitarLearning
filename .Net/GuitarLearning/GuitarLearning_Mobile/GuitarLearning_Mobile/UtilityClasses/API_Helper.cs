using GuitarLearning_Essentials;
using GuitarLearning_Mobile.DeveloperSupport;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.UtilityClasses
{
    /// <summary>
    /// Contains all the methods that are needed for the communication with the API.
    /// </summary>
    public class API_Helper
    {
        /// <summary>
        /// The APIAddress property holds the url that refers to the server.
        /// </summary>
        /// <value>Gets/Sets the APIAddress propety string field. Default value is <code>string.Empty</code>.</value>
        private string ApiAddress { get; set; } = string.Empty;
        /// <summary>
        /// The AudioBuffer property contains an instance of the buffer, where the raw audio data is stored <see cref="UtilityClasses.AudioBuffer"/>./>
        /// </summary>
        /// <value>Gets/Sets the AudioBuffer property field. Default value is <code>null</code>.</value>
        private AudioBuffer AudioBuffer { get; set; } = null;
        /// <summary>
        /// The ResulBuffer property contains an instance of the buffer, where the data recieved by the API is stored <see cref="UtilityClasses.ResultBuffer"/>./>
        /// </summary>
        /// <value>Gets/Sets the ResultBuffer property field. Default valus is <code>null</code>.</value>
        private ResultBuffer ResultBuffer { get; set; } = null;
        /// <summary>
        /// The cancellationToken field contains the token that can be used to end all asynchronous running tasks.
        /// </summary>
        private CancellationTokenSource cancellationToken;
        /// <summary>
        /// The Amount of responses recieved from the server
        /// </summary>
        /// <value>Gets/Sets the ResponsesRecieved int field.</value>
        private int ResponsesRecieved { get; set; } = 0;
        private int ErrorResponses { get; set; } = 0;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="audioBuffer">Instance of the AudioBuffer that shall be used.</param>
        /// <param name="resultBuffer">Instance of the ResultBuffer that shall be used.</param>
        /// <param name="apiAddress">[Optional] API-Address, default value is "https://guitarlearningapi.azurewebsites.net/api/Essentia"</param>
        public API_Helper(AudioBuffer audioBuffer, ResultBuffer resultBuffer, string apiAddress = "https://guitarlearningapi.azurewebsites.net/api/Essentia")
        {
            AudioBuffer = audioBuffer;
            ResultBuffer = resultBuffer;
            ApiAddress = apiAddress;
            cancellationToken = new CancellationTokenSource();
        }

        /// <summary>
        /// Uses the <see cref="cancellationToken"/> to cancel all tasks.
        /// </summary>
        public void StopAPI()
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                if (DevFlags.LoggingEnabled) Logger.APILog("Cancelling Tasks");
                cancellationToken.Cancel();
            }
        }

        /// <summary>
        /// Start the asynchronous communication with the API.
        /// </summary>
        public void StartAPI()
        {
            Task.Run(async () =>
            {
                try
                {
                    if (DevFlags.LoggingEnabled) Logger.APILog("Start processing");
                    await ProcessingCycle(cancellationToken.Token);
                }
                catch (OperationCanceledException e)
                {
                    //Ignore, just return 
                    if (DevFlags.LoggingEnabled) Logger.APILog("Processing task was cancelled");
                    Logger.APILog("Recieved " + ResponsesRecieved + " responses");
                    Logger.APILog("Recieved " + ErrorResponses + " errors");
                }
                catch (Exception e)
                {
                    Logger.Log("API_Helper - Error: " + e.Message);
                }
            });
        }

        /// <summary>
        /// Loop that reads the first entrance of the <see cref="AudioBuffer"/> and makes the API call.
        /// The data that is recieved is pushed into the <see cref="ResultBuffer"/>./>
        /// </summary>
        /// <param name="ct">Token that throws an OperationCancelledException when the token state is set to cancel. Used to stop the task.</param>
        /// <returns>Task, so the method can be run asynchronously.</returns>
        private async Task ProcessingCycle(CancellationToken ct)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 0, 12);
                while (true)
                {
                    //if (DevFlags.LoggingEnabled) Logger.APILog("Looping");
                    ct.ThrowIfCancellationRequested();
                    if (AudioBuffer?.Peek() != null)
                    {
                        var data = AudioBuffer?.Get();
                        EssentiaModel essentiaModel = new EssentiaModel(data.Data, data.Time);
                        var chordData = await DoAPICall(essentiaModel, httpClient, ct);
                        ResultBuffer.Add(chordData);
                    }
                }
            }
        }

        /// <summary>
        /// Implementation of the actual communication with the API used by <see cref="ProcessingCycle(CancellationToken)"/>.
        /// </summary>
        /// <param name="currentModel">Data that will be sent to the API.</param>
        /// <param name="myClient">HttpClient that is used for the communication.</param>
        /// <param name="ct">Token that throws an OperationCancelledException when the token state is set to cancel. Used to stop the task.</param>
        /// <returns>Task[EssentiaModel], method can be run asynchronously. Actual return data is the EssentiaModel</returns>
        private async Task<EssentiaModel> DoAPICall(EssentiaModel currentModel, HttpClient myClient, CancellationToken ct)
        {
            ct.ThrowIfCancellationRequested();
            EssentiaModel apiModel = null;
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.RequestUri = new Uri(ApiAddress);
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");
                var requestContent = JsonConvert.SerializeObject(currentModel);
                request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");

                ct.ThrowIfCancellationRequested();
                HttpResponseMessage response = await myClient.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ResponsesRecieved++;
                    HttpContent content = response.Content;
                    string json = await content.ReadAsStringAsync();
                    apiModel = JsonConvert.DeserializeObject<EssentiaModel>(json);
                    if (DevFlags.LoggingEnabled) Logger.APILog("Response == OK -" + apiModel.chordData);
                }
                else if (DevFlags.LoggingEnabled) { Logger.APILog("Response != OK"); ErrorResponses++; }
            }
            ct.ThrowIfCancellationRequested();
            return apiModel;
        }
    }
}
