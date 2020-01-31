using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using GuitarLearning_Essentials;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GuitarLearning_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EssentiaController : ControllerBase
    {
        // GET: api/Essentia
        /// <summary>
        /// HTTP GET
        /// <para>Used to check if the a connection to the server can be established.</para>
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            try
            {
                return new string[] { "GuitarLearning_ServerOperational" };
            }
            catch(Exception e)
            {
                Logger.Log("Error - " + e.Message + "\nStack: " + e.StackTrace);
                return null;
            }
        }

        // GET: api/Essentia/5
        /// <summary>
        /// Obsolete
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        [HttpGet("{inputString}", Name = "Get")]
        public string Get(string inputString)
        {
            //float[] audioData = EssentiaInterface.ParseToFloatArray(inputString);
            //string chordData = EssentiaInterface.CalculateChordsFrom(audioData);
            //return "%" + chordData + "%";
            return Directory.GetCurrentDirectory();
        }

        // GET: api/Essentia/5
        /*[HttpPost]
        public ChordItem Post(ChordItem input)
        {
            float[] audioData = EssentiaInterface.ParseToFloatArray(input.Chords);
            string chordData = EssentiaInterface.CalculateChordsFrom(audioData);
            return new ChordItem() { Chords = "%" + chordData + "%" };
        }*/

        /// <summary>
        /// HTTP POST
        /// <para>API function which accepts a <see cref="EssentiaModel"/> containing the raw audio data. The model is analysed and then returned to the client.</para>
        /// </summary>
        /// <param name="input">Input model containing the raw audio data.</param>
        /// <returns>Output model containing a string of chords which were calculated from the data.</returns>
        [HttpPost]
        public EssentiaModel Post(EssentiaModel input)
        {
            try
            {
                //Logger.Log("IncomingData: " + input.chordData + " - " + input.audioData.Length + " - " + input.audioData.ToString());
                string chordData = EssentiaInterface.CalculateChordsFrom(input.audioData);
                if (chordData == "") { chordData = "X,0.0;"; }
                return new EssentiaModel() { audioData = new float[1], chordData = chordData, time = input.time };
            }
            catch(Exception e)
            {
                Logger.Log("Error - " + e.Message + "\nStack: " + e.StackTrace);
                return null;
            }
        }
    }
}
