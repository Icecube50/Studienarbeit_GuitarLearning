using GuitarLearning_Essentials.SongModel;
using GuitarLearning_Mobile.DeveloperSupport;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GuitarLearning_Mobile.UtilityClasses
{
    /// <summary>
    /// Prepare the static data given by the music sheet, by calculating the expected timings. 
    /// </summary>
    public static class SongHelper
    {
        public static event EventHandler ProgressMade;
        /// <summary>
        /// Duration of the whole song
        /// </summary>
        /// <value>Gets/Sets the SongDuration double field.</value>
        public static double SongDuration { get; set; }
        /// <summary>
        /// Number of notes in the song
        /// </summary>
        /// <value>Gets/Sets the NumberOfNotes int field</value>
        public static int NumberOfNotes { get; set; }
        /// <summary>
        /// Song object that is serialised from the "SongName.xml". Contains all generic song data.
        /// </summary>
        /// <value>Gets/Sets the Song <see cref="GuitarLearning_Essentials.SongModel.Song"/> field.</value>
        private static Song Song { get; set; }
        /// <summary>
        /// Index of the array that matches the current (next) note to be analysed.
        /// </summary>
        /// <valu>Gets/Sets the SongIndex int field.</valu>
        private static int SongIndex { get; set; }
        /// <summary>
        /// Property that stores the "beats per second" of the song, is needed for further calculations.
        /// </summary>
        /// <value>Gets/Sets the BPS double field.</value>
        private static double BPS { get; set; }
        /// <summary>
        /// Array of <see cref="INote"/> => can contain <see cref="Note"/> and <see cref="Chord"/> and can be accessed via index.
        /// </summary>
        /// <value>Gets/Sets the IndexedSong INote[] field.</value>
        private static INote[] IndexedSong { get; set; }
        /// <summary>
        /// Mutex, used to restrict access on the song.
        /// </summary>
        private static readonly object _locker = new object();
        /// <summary>
        /// Initialise the properties of this class.
        /// </summary>
        /// <param name="song">Song object, serialised through the "SongName.xml"</param>
        public static void InitHelper(Song song)
        {
            Song = new Song(song);
            SongIndex = 0;
            if (Song.BPM > 0) BPS = ((60.0 / Song.BPM) * 1000.0);
            else BPS = 0;
            SongDuration = Song.SongDuration;

            IndexedSong = new INote[Song.Notation.Count];
            Song.Notation.CopyTo(IndexedSong);

            if (SongIndex >= IndexedSong.Length) throw new Exception("Invalid Index");
            NumberOfNotes = IndexedSong.Length;
        }
        /// <summary>
        /// Checks whether enough time has passed to determine a new index or to keep the old one.
        /// </summary>
        /// <param name="elapsed">Time in milliseconds that has elapsed since the start of the song.</param>
        /// <param name="obj">Last SongObject that has been analysed.</param>
        /// <returns></returns>
        public static bool IncreaseIndex(double elapsed, SongObject obj)
        {
            lock (_locker)
            {
                if (DevFlags.LoggingEnabled) Logger.AnalyzerLog((obj.TimePosition + obj.Duration) + "/" + elapsed);
                if ((obj.TimePosition + obj.Duration) <= elapsed)
                {
                    SongIndex++;
                }

                float progress = Convert.ToSingle(elapsed / SongDuration);
                if (progress > 1) progress = 1;
                ProgressMade?.Invoke(progress, new EventArgs());

                if (SongIndex >= IndexedSong.Length)
                {
                    return true;
                }
                else return false;
            }
        }
        /// <summary>
        /// Get the SongObject that is stored at the current index.
        /// </summary>
        /// <returns><see cref="SongObject"/></returns>
        public static SongObject GetNext()
        {
            lock (_locker)
            {
                var obj = IndexedSong[SongIndex];
                if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Index: " + SongIndex + "/" + IndexedSong.Length);
                //if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("From Xml: " + obj.ToString());
                if (obj.IsNote) return GetNext_Note(obj.Note);
                else return GetNext_Chord(obj.Chord);
            }
        }
        /// <summary>
        /// Initialises a new SongObject and set the <see cref="SongObject.Type"/> property to <see cref="Highlight.Note"/>
        /// </summary>
        /// <param name="note"><see cref="Note"/> from which a new SongObject shall be created.</param>
        /// <returns<see cref="SongObject"/></returns>
        private static SongObject GetNext_Note(Note note)
        {
            //if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Name_Before: " + note.Name);
            var songObj = new SongObject();
            songObj.Name = note.Name;
            songObj.Type = Highlight.Note;
            songObj.WebId = note.ID;
            songObj.TimePosition = CalculatePosition(note.StrokeNumber, note.BeatNumber);
            songObj.Duration = BPS * note.Duration;
            //if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Name_After: " + songObj.Name);
            return songObj;
        }
        /// <summary>
        /// Initialises a new SongObject and set the <see cref="SongObject.Type"/> property to <see cref="Highlight.Chord"/>
        /// </summary>
        /// <param name="chord"><see cref="Chord"/> from which a new SongObject shall be created.</param>
        /// <returns><see cref="SongObject"/></returns>
        private static SongObject GetNext_Chord(Chord chord)
        {
            //if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Name_Before: " + chord.Name);
            var songObj = new SongObject();
            songObj.Name = chord.Name;
            songObj.Type = Highlight.Chord;
            songObj.WebId = chord.ID;
            songObj.TimePosition = CalculatePosition(chord.StrokeNumber, chord.BeatNumber);
            songObj.Duration = BPS * chord.Duration;
            //if (DevFlags.LoggingEnabled) Logger.AnalyzerLog("Name_After: " + songObj.Name);
            return songObj;
        }
        /// <summary>
        /// Calculates the point of time on which the note is expected.
        /// </summary>
        /// <param name="StrokeNumber">Number of strokes inside the current beat.</param>
        /// <param name="BeatNumber">Number of beats.</param>
        /// <returns>int, time in milliseconds since start of the song.</returns>
        private static int CalculatePosition(double StrokeNumber, double BeatNumber)
        {
            double timePerBeat = 4 * BPS;
            double positionTime = timePerBeat * (BeatNumber + 0.5);
            positionTime += (BPS * StrokeNumber);
            positionTime = Math.Round(positionTime);
            return Convert.ToInt32(positionTime);
        }
    }

    /// <summary>
    /// Class that stores all important information about a chord or note. Is needed for the analysation.
    /// </summary>
    public class SongObject
    {
        /// <summary>
        /// Point in time the note is expected.
        /// </summary>
        /// <value>Gets/Sets the TimePosition int field in milliseconds since start of the song.</value>
        public int TimePosition { get; set; }
        /// <summary>
        /// Name of the note
        /// </summary>
        /// <value>Gets/Sets the Name string field.</value>
        public string Name { get; set; }
        /// <summary>
        /// Type that is needed to determin which highlighting-method has to be called.
        /// </summary>
        /// <value>Gets/Sets the Type <see cref="Highlight"/> field.</value>
        public Highlight Type { get; set; }
        /// <summary>
        /// The ID, that is used in the html document, which refers to the note.
        /// </summary>
        /// <value>Gets/Sets the WebId string field.</value>
        public string WebId { get; set; }
        /// <summary>
        /// Duration of the note.
        /// </summary>
        /// <value>Gets/Sets the Duration double field, in milliseconds.</value>
        public double Duration { get; set; }
    }
}
