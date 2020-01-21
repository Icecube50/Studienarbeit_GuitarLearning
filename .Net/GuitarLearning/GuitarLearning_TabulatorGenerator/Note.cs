using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuitarLearning_TabulatorGenerator
{
    public abstract class Note
    {
        public abstract uint GetDuration();
        public abstract GuitarStrings GetString();
        public abstract uint GetValue();
        public abstract string ToHTML();

        public abstract string ToCss(int left);
    }

    public class WholeNote : Note
    {
        private GuitarStrings GitString;
        private uint NoteDuration;
        private uint NoteValue;
        private string ID;
        public WholeNote(GuitarStrings gString, uint value, string id)
        {
            GitString = gString;
            NoteValue = value;
            NoteDuration = SetValues.QuarterDuration * 4;
            ID = "Whole_" + id;
        }

        public override uint GetDuration()
        {
            return NoteDuration;
        }

        public override GuitarStrings GetString()
        {
            return String;
        }

        public override uint GetValue()
        {
            return NoteValue;
        }

        public override string ToHTML()
        {
            return "<div id=\"Whole_" + ID + "\">" + NoteValue + "</div>\n";
        }

        public override string ToCss(int left)
        {
            return "#" + ID + "{\n" +
                "position: absolute;\n" +
                "top: " + GetTop(GitString) + "px;\n" +
                "left: " + left + "px;\n" +
                "}\n";
        }
    }

    public class HalfNote : Note
    {
        private GuitarStrings String;
        private uint NoteDuration;
        private uint NoteValue;
        private string ID;
        public HalfNote(GuitarStrings gString, uint value, string id)
        {
            String = gString;
            NoteValue = value;
            NoteDuration = SetValues.QuarterDuration * 2;
            ID = id;
        }

        public override uint GetDuration()
        {
            return NoteDuration;
        }

        public override GuitarStrings GetString()
        {
            return String;
        }

        public override uint GetValue()
        {
            return NoteValue;
        }
        public override string ToHTML()
        {
            return "<div id=\"" + ID + "\">" + NoteValue + "</div>\n";
        }
    }

    public class QuarterNote : Note
    {
        private GuitarStrings String;
        private uint NoteDuration;
        private uint NoteValue;
        private string ID;
        public QuarterNote(GuitarStrings gString, uint value, string id)
        {
            String = gString;
            NoteValue = value;
            NoteDuration = SetValues.QuarterDuration;
            ID = id;
        }

        public override uint GetDuration()
        {
            return NoteDuration;
        }

        public override GuitarStrings GetString()
        {
            return String;
        }

        public override uint GetValue()
        {
            return NoteValue;
        }
        public override string ToHTML()
        {
            return "<div id=\"" + ID + "\">" + NoteValue + "</div>\n";
        }
    }

    public class EighthNote : Note
    {
        private GuitarStrings String;
        private uint NoteDuration;
        private uint NoteValue;
        private string ID;
        public EighthNote(GuitarStrings gString, uint value, string id)
        {
            String = gString;
            NoteValue = value;
            NoteDuration = SetValues.QuarterDuration / 2;
            ID = id;
        }

        public override uint GetDuration()
        {
            return NoteDuration;
        }

        public override GuitarStrings GetString()
        {
            return String;
        }

        public override uint GetValue()
        {
            return NoteValue;
        }
        public override string ToHTML()
        {
            return "<div id=\"" + ID + "\">" + NoteValue + "</div>\n";
        }
    }
}
