using NAudio.Wave;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        [DllImport("D:\\10-Unix-Subsystem\\essentia-master\\src\\guitar_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int Double(int Number);

        [DllImport("D:\\10-Unix-Subsystem\\essentia-master\\src\\guitar_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern void HelloWorld();

        [DllImport("D:\\10-Unix-Subsystem\\essentia-master\\src\\guitar_api.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern float TestFloatArray([In, Out] float[] Buffer, int Size);

        [DllImport("D:\\10-Unix-Subsystem\\essentia-master\\src\\guitar_api.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern int TestIntArray([In, Out] int[] Buffer, int Size);

        [DllImport("D:\\10-Unix-Subsystem\\essentia-master\\src\\guitar_api.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string TestString();

        [DllImport("D:\\10-Unix-Subsystem\\essentia-master\\src\\guitar_api.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string TestFloatArrayAndString([In, Out]float[] Buffer, int Size);

        [DllImport("D:\\10-Unix-Subsystem\\essentia-master\\src\\guitar_api.dll", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.LPStr)]
        public static extern string CalculateChords([In, Out]float[] audioInput, int audioInputSize/*, [In, Out]int DEBUG_MODE/*, int sampleRate, int frameSize, int hopSize*/);

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine(Double(2));

            HelloWorld();

            Console.WriteLine(Double(10));

            string Path = "chordtest_128_1.wav";

            if (File.Exists(Path))
            {
                int outsideRead = 0;
                float[] outsideBuffer = null;
                using (AudioFileReader reader = new AudioFileReader(Path))
                {
                    float[] buffer = new float[reader.Length];
                    outsideRead = reader.Read(buffer, 0, buffer.Length);
                    outsideBuffer = buffer;
                }

                StringBuilder sb = new StringBuilder(outsideBuffer.Length * 6);
                foreach(float f in outsideBuffer)
                {
                    if(f == 0)continue;
                    sb.Append(f.ToString() + ";");
                }
                System.IO.File.WriteAllText(@"WavAsString.txt", sb.ToString());
                Console.WriteLine("Success");


                /*for(int i = 0; i < buffer.Length; i = i + 256)
                 {
                     string s = string.Empty;
                     for(int j = 0; j < 256; j++)
                     {
                         s += Convert.ToString(buffer[j + i]);
                     }
                     Console.WriteLine(s);
                 }*/

                //int counter = 0;
                //do
                //{
                //    Console.WriteLine("---- Attempt: " + counter + " ----");
                //    float sum = 0.0f;
                //    for (int i = 0; i < outsideBuffer.Length; i++)
                //    {
                //        sum += outsideBuffer[i];
                //    }
                //    float externSum = TestFloatArray(outsideBuffer, outsideBuffer.Length);
                //    Console.WriteLine("Intern: " + sum + " -- " + "Extern: " + externSum);

                //    int[] array = new int[101];
                //    int intSume = 0;
                //    for (int i = 0; i < array.Length; i++)
                //    {
                //        array[i] = i;
                //        intSume += i;
                //    }
                //    int externIntSum = TestIntArray(array, array.Length);
                //    Console.WriteLine("Intern: " + intSume + " -- " + "Extern: " + externIntSum);

                //    counter++;
                //}
                //while (counter < 1);

                string s = TestFloatArrayAndString(outsideBuffer, outsideBuffer.Length);
                Console.WriteLine(s);

                string chords = CalculateChords(outsideBuffer, outsideBuffer.Length/*, 1, 44100, 2048, 1024*/);

                Console.WriteLine(chords);
            }
        }
    }
}
