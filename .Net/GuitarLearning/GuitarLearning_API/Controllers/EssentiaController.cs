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
