using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuitarLearning_API.Models;
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
            return new string[] { "Essentia", "Ist Toll" };
        }

        // GET: api/Essentia/5
        [HttpGet("{inputString}", Name = "Get")]
        public string Get(string inputString)
        {
            //float[] audioData = EssentiaInterface.ParseToFloatArray(inputString);
            //string chordData = EssentiaInterface.CalculateChordsFrom(audioData);
            //return "%" + chordData + "%";
            return inputString;
        }

        // GET: api/Essentia/5
        [HttpPost]
        public ChordItem Post(ChordItem input)
        {
            float[] audioData = EssentiaInterface.ParseToFloatArray(input.Chords);
            string chordData = EssentiaInterface.CalculateChordsFrom(audioData);
            return new ChordItem() { Chords = "%" + chordData + "%" };
        }

        [HttpPost]
        public Temp Post(Temp input)
        {
            string chordData = EssentiaInterface.CalculateChordsFrom(input.audioData);
            return new Temp() { audioData = new float[1], chordData = chordData };
        }
    }
}
