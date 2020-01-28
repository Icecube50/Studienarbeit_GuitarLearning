using GuitarLearning_Essentials;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class API_Helper
    {
        private string ApiAddress { get; set; } = string.Empty;
        private AudioBuffer AudioBuffer { get; set; } = null;
        private ResultBuffer ResultBuffer { get; set; } = null;
        private CancellationTokenSource cancellationToken;

        public API_Helper(AudioBuffer audioBuffer, ResultBuffer resultBuffer, string apiAddress = "https://guitarlearningapi.azurewebsites.net/api/Essentia")
        {
            AudioBuffer = audioBuffer;
            ResultBuffer = resultBuffer;
            ApiAddress = apiAddress;
            cancellationToken = new CancellationTokenSource();
        }

        public void StopAPI()
        {
            if (!cancellationToken.IsCancellationRequested)
            {
                cancellationToken.Cancel();
            }
        }

        public void StartAPI()
        {
            Task.Run(async () =>
            {
                try
                {
                    await ProcessingCycle(cancellationToken.Token);
                }
                catch (OperationCanceledException e)
                {
                    //Ignore, just return 
                }
                catch (Exception e)
                {
                    Logger.Log("API_Helper - Error: " + e.Message);
                }
            });
        }

        private async Task ProcessingCycle(CancellationToken ct)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 0, 12);
                while (true)
                {
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
                    HttpContent content = response.Content;
                    string json = await content.ReadAsStringAsync();
                    apiModel = JsonConvert.DeserializeObject<EssentiaModel>(json);
                }             
            }
            ct.ThrowIfCancellationRequested();
            return apiModel;
        }
    }
}
