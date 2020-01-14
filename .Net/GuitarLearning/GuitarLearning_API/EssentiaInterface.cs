using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GuitarLearning_API
{
    public static class EssentiaInterface
    {
        [DllImport("Extern\\guitar_api.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        private static extern string CalculateChords([In, Out]float[] audioInput, int audioInputSize);

        public static float[] ParseToFloatArray(string inputData)
        {
            string[] splittedString = inputData.Split(";");
            float[] audioData = new float[splittedString.Length];

            for(int i = 0; i < audioData.Length; i++)
            {
                audioData[i] = Convert.ToSingle(splittedString[i]);
            }
            Logger.Log("InputString - " + inputData);
            return audioData;
        }

        public static string CalculateChordsFrom(float[] audioData)
        {
            string chords = CalculateChords(audioData, audioData.Length);
            Logger.Log("ChordString - " + chords);
            return chords;
        }
    }
}
