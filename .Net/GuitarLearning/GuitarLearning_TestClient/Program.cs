using NAudio.Wave;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TestClient
{
    public class Temp
    {
        [JsonProperty("audioData")]
        public float[] audioData { get; set; }

        [JsonProperty("chordData")]
        public string chordData { get; set; }
    }

    class Program
    {
        static HttpClient httpClient = new HttpClient();
        static async Task Main(string[] args)
        {
            Console.WriteLine("Input URI:");
            string url = Console.ReadLine();

            await RunAsync(url);

            await TestWavFile("chordtest_128_1.wav");
        }

        static async Task TestWavFile(string path)
        {
            if (!File.Exists(path)) return;

            float[] buffer = null;
            using (AudioFileReader reader = new AudioFileReader(path))
            {
                buffer = new float[reader.Length];
                reader.Read(buffer, 0, buffer.Length);
            }

            int index = 0;
            do
            {
                if (index > buffer.Length) { break; }
                Logger.Log("Parsing Index " + index.ToString() + " - " + (index + 10000).ToString());
                float[] partOfBuffer = new float[10000];
                for(int i = 0; i < partOfBuffer.Length; i++)
                {
                    if(index + i >= buffer.Length) 
                    {
                        break; 
                    }
                    partOfBuffer[i] = buffer[index + i];
                }
                index += 10000;
                //Logger.Log(BufferToDataString(partOfBuffer, 0, 1000));
                try
                {
                    var payload = new Temp() { audioData = partOfBuffer, chordData = string.Empty };
                    string json = JsonConvert.SerializeObject(payload);

                    var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await httpClient.PostAsync("api/Essentia", httpContent);
                    if (response.Content != null)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var truckload = JsonConvert.DeserializeObject<Temp>(responseContent);
                        if (truckload is Temp)
                        {
                            Temp t = truckload as Temp;
                            Logger.Log("Chord: " + t.chordData);
                        }
                    }
                }
                catch(Exception e)
                {
                    Logger.Log(e.Message);
                    ; 
                }
            }
            while (true);
            return;
        }

        static float[] ReadFromFile(string path)
        {
            float[] buffer = null;
            using (AudioFileReader reader = new AudioFileReader(path))
            {
                buffer = new float[reader.Length];
                reader.Read(buffer, 0, buffer.Length);
            }
            return buffer;
        }

        static float[] ReadFromFile(string path,int start, int length)
        {
            float[] buffer = null;
            using (AudioFileReader reader = new AudioFileReader(path))
            {
                buffer = new float[length];
                reader.Read(buffer, start, length);
            }
            return buffer;
        }

        static string BufferToDataString(float[] buffer, int startIndex, int count)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (startIndex + count > buffer.Length) count = buffer.Length - startIndex;
            if (startIndex > buffer.Length) return string.Empty;

            for(int i = startIndex; i < startIndex + count; i++)
            {
                stringBuilder.Append(Convert.ToString(buffer[i]) + ";");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            return stringBuilder.ToString();
        }


        static string BuildRequestAddress(string apiAddress, string content)
        {
            if (apiAddress[apiAddress.Length - 1] != '/') apiAddress += '/';

            string completeAdress = apiAddress + content;
            return completeAdress;
        }

        static async Task RunAsync(string url)
        {
            httpClient.BaseAddress = new Uri(url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
        }

        static async Task<string> GET_ChordRequest(string address)
        {
            string chordString = string.Empty;

            HttpContent httpContent = new StringContent(chordString);
            HttpResponseMessage response = await httpClient.PostAsync(address, httpContent);
            if (response.IsSuccessStatusCode)
            {
                string msg = await response.Content.ReadAsStringAsync();
                msg = CleanString(msg, 0);
                chordString = msg;
            }
            return chordString;
        }

        static async Task<int> GetValueTimes5(string path)
        {
            int calculatedValue = -1;
            HttpResponseMessage response = await httpClient.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                string msg = await response.Content.ReadAsStringAsync();
                msg = CleanString(msg, 0);
                calculatedValue = Convert.ToInt32(msg);
            }
            return calculatedValue;
        }

        static string CleanString(string corrupted, int index)
        {
            if (index > corrupted.Length) return string.Empty;
            if(corrupted[index] == '%')
            {
                StringBuilder stringBuilder = new StringBuilder();
                for(int i = index + 1; i < corrupted.Length; i++)
                {
                    if (corrupted[i] == '%') break;
                    stringBuilder.Append(corrupted[i]);
                }
                return stringBuilder.ToString();
            }
            return CleanString(corrupted, ++index);
        }
    }
}
