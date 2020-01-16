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
    public static class API_Helper
    {
        private static string _apiAddress { get; set; } = string.Empty;

        public static void API_HelperSetup(string apiAddress)
        {
            _apiAddress = apiAddress;

            AudioBuffer.BufferUpdated += async (s, e) =>
            {
                EssentiaModel currentModel = new EssentiaModel()
                {
                    audioData = AudioBuffer.Get(),
                    chordData = string.Empty
                };
                var chordData = await DoAPICall(currentModel);
                //TODO UpdateUI, per event?
            };
        }

        private static async Task<EssentiaModel> DoAPICall(EssentiaModel currentModel)
        {
            EssentiaModel apiModel = null;
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.RequestUri = new Uri(_apiAddress);
                request.Method = HttpMethod.Post;
                request.Headers.Add("Accept", "application/json");
                var requestContent = JsonConvert.SerializeObject(currentModel);
                request.Content = new StringContent(requestContent, Encoding.UTF8, "application/json");

                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.Timeout = new TimeSpan(0, 0, 0, 5);
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        HttpContent content = response.Content;
                        string json = await content.ReadAsStringAsync();
                        apiModel = JsonConvert.DeserializeObject<EssentiaModel>(json);
                    }
                }
            }
            return apiModel;
        }
    }
}
