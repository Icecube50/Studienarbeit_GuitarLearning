using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator
{
    public enum GuitarStrings
    {
        E,
        A,
        D,
        G,
        B,
        e
    };

    public enum Notes
    {
        Ganze,
        Halbe,
        Viertel,
        Achtel
    };

    public static class SetValues
    {
        public static uint QuarterDuration { get; set; } = 0;
        public static string Title { get; set; } = string.Empty;
        public static uint BPM { get; set; } = 0;
        public static uint SizeHeader { get; set; } = 0;
        public static uint SizeChords { get; set; } = 0;
        public static string WholeNotePicture { get; set; } = "ganze.png";
        public static string HalfNotePicture { get; set; } = "halbe.png";
        public static string QuarterNotePicture { get; set; } = "viertel.png";
        public static string EighthNotePicture { get; set; } = "achtel.png";

        public static uint GetMaxWidth()
        {
            uint width = 0;
            foreach(Note n in NoteContainer.Song)
            {
                width += n.GetDuration();
            }
            return width;
        }

    }
}
