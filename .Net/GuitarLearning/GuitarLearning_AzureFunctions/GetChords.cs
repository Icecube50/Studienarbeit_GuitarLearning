using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Runtime.InteropServices;

namespace GuitarLearning_AzureFunctions
{
    public static class GetChords
    {
        [DllImport("..\\bin\\guitar_api.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string CalculateChords([In, Out]float[] audioInput, int audioInputSize);

        [FunctionName("GetChords")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string wav = req.Query["wav"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            wav = wav ?? data?.wav;

            string chords = string.Empty;
            if (wav != null)
            {
                string[] wavSplitted = wav.Split(";");
                float[] buffer = new float[wavSplitted.Length];
                for (int i = 0; i < wavSplitted.Length; i++)
                {
                    buffer[i] = Convert.ToSingle(wavSplitted[i]);
                }

                chords = CalculateChords(buffer, buffer.Length);
            }

            return wav != null
                ? (ActionResult)new OkObjectResult($"{chords}")
                : new BadRequestObjectResult("Please pass a name on the query string or in the request body");
        }
    }
}
