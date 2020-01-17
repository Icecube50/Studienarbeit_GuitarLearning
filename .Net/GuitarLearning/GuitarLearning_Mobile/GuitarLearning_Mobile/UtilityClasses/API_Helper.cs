using GuitarLearning_Essentials;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.UtilityClasses
{
    public class API_Helper
    {
        private readonly object _lock = new object();
        private string _apiAddress { get; set; } = string.Empty;
        private AudioBuffer _audioBuffer { get; set; } = null;
        private bool DoProcessFlag { get; set; } = false;

        private event EventHandler DoProcess;

        public API_Helper(AudioBuffer audioBuffer, string apiAddress = "https://guitarlearningapi.azurewebsites.net/api/Essentia")
        {
            _audioBuffer = audioBuffer;
            _apiAddress = apiAddress;

            _audioBuffer.StartProcessing += _audioBuffer_StartProcessing;
            _audioBuffer.StopProcessing += _audioBuffer_StopProcessing;

            DoProcess += async (s, e) =>
            {
                await ProcessingCycle();
            };
        }

        private void _audioBuffer_StopProcessing(object sender, EventArgs e)
        {
            if (!DoProcessFlag)
                return;
            DoProcessFlag = false;
        }

        private void _audioBuffer_StartProcessing(object sender, EventArgs e)
        {
            if (DoProcessFlag)
                return;
            DoProcessFlag = true;
            DoProcess?.Invoke(this, new EventArgs());
        }

        private async Task ProcessingCycle()
        {
            using(HttpClient httpClient = new HttpClient())
            {
                httpClient.Timeout = new TimeSpan(0, 0, 12);

                while (DoProcessFlag)
                {
                    if (_audioBuffer.Count() > 0)
                    {
                        EssentiaModel essentiaModel = new EssentiaModel(_audioBuffer.Get());
                        var chordData = await DoAPICall(essentiaModel, httpClient);
#if DEBUG
                        DoLog(chordData.chordData);
#endif
                    }
                }
            }
        }

        private async Task<EssentiaModel> DoAPICall(EssentiaModel currentModel, HttpClient myClient)
        {
            EssentiaModel apiModel = null;
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.RequestUri = new Uri(_apiAddress);
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");
                var requestContent = JsonConvert.SerializeObject(currentModel);
                request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await myClient.SendAsync(request);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContent content = response.Content;
                    string json = await content.ReadAsStringAsync();
                    apiModel = JsonConvert.DeserializeObject<EssentiaModel>(json);
                }             
            }
            return apiModel;
        }

        private void DoLog(string chordData)
        {
            string msg = "Failed";
            if (chordData != null)
                msg = chordData;
            Logger.Log("Data: " + msg);
        }
    }
}
