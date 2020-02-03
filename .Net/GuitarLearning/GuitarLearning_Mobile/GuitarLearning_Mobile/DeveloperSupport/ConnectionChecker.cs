using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_Mobile.DeveloperSupport
{
    /// <summary>
    /// Contains methods to check network connection. Is used to make sure device can connect to the API before starting the actual process.
    /// </summary>
    public static class ConnectionChecker
    {
        /// <summary>
        /// Checks generic network connection
        /// </summary>
        /// <returns>true: when the device has an active connection to the internet, otherwise false</returns>
        public static bool HasConnectionToNetwork()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                return true;
            }

            Logger.Log("No connection to network");
            return false;
        }

        /// <summary>
        /// Checks whether the API can be reached
        /// </summary>
        /// <param name="address">API address</param>
        /// <returns>true: when the server send a response with status "OK", otherwise false</returns>
        public static bool CanReachApiAt(string address = "https://guitarlearningapi.azurewebsites.net/api/Essentia")
        {
            try
            {
                bool success = false;
                Task.Run(async () =>
                {
                    success = await GetRequestAt(address);
                });
                return success;
            }
            catch
            {
                Logger.Log("Cannot reach api");
                return false;
            }
        }

        /// <summary>
        /// Performs a get request
        /// </summary>
        /// <param name="address">Address the get request will be sent to</param>
        /// <returns>true: when the server send a response with status "OK", otherwise false</returns>
        private static async Task<bool> GetRequestAt(string address)
        {
            using (HttpRequestMessage request = new HttpRequestMessage())
            {
                request.RequestUri = new Uri(address);
                request.Method = HttpMethod.Get;
                request.Headers.Add("Accept", "application/json");
                using (HttpClient httpClient = new HttpClient())
                {
                    httpClient.Timeout = new TimeSpan(0, 0, 0, 5);
                    HttpResponseMessage response = await httpClient.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
