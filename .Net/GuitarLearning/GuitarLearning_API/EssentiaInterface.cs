using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GuitarLearning_API
{
    public static class EssentiaInterface
    {
        /// <summary>
        /// Imported
        /// <para>Method that uses the anlgorithms implemented in the essentia library to analyse the recieved audio data.</para>
        /// </summary>
        /// <param name="audioInput">Raw audio data</param>
        /// <param name="audioInputSize">Size of the array</param>
        /// <returns></returns>
        [DllImport("Extern\\guitar_api.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        private static extern string CalculateChords([In, Out]float[] audioInput, int audioInputSize);

        /// <summary>
        /// Obsolete
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static float[] ParseToFloatArray(string inputData)
        {
            string[] splittedString = inputData.Split(";");
            float[] audioData = new float[splittedString.Length];

            for(int i = 0; i < audioData.Length; i++)
            {
                audioData[i] = Convert.ToSingle(splittedString[i]);
            }
            if(Logger.DEBUGFLAG) Logger.Log("InputString - " + inputData);
            return audioData;
        }
        /// <summary>
        /// Analyses the audio data and finds musical chords.
        /// </summary>
        /// <param name="audioData">Raw audio data</param>
        /// <returns></returns>
        public static string CalculateChordsFrom(float[] audioData)
        {
            string chords = CalculateChords(audioData, audioData.Length);
            if(Logger.DEBUGFLAG) Logger.Log("ChordString - " + chords);
            return chords;
        }
    }
}
