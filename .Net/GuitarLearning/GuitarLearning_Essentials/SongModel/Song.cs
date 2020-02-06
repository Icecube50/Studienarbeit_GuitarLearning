using System;
using System.Collections.Generic;

namespace GuitarLearning_Essentials.SongModel
{
    /// <summary>
    /// XML ELEMENT
    /// <para>Contains all generic information about the song.</para>
    /// </summary>
    [Serializable]
    public class Song
    {
        /// <summary>
        /// Title of the song.
        /// </summary>
        /// <value>Gets/Sets the SongTitle string field.</value>
        public string SongTitle { get; set; }
        /// <summary>
        /// Beats per minute of the song.
        /// </summary>
        /// <value>Gets/Sets the BPM int field.</value>
        public int BPM { get; set; }
        /// <summary>
        /// Duration of the song (in milliseconds).
        /// </summary>
        /// <value>Gets/Sets the SongDuration double field.</value>
        public double SongDuration { get; set; }
        /// <summary>
        /// List that contains all notes and chords of the song converted to an <see cref="INote"/>.
        /// </summary>
        /// <value>Gets/Sets the Notation property. The List has to be initialised with a default value or else the xml serialisation fails.</value>
        public List<INote> Notation { get; set; } = new List<INote>();
        /// <summary>
        /// Constructor
        /// <para>An empty constructor is needed for xml (de-)serialisation.</para>
        /// </summary>
        public Song()
        {

        }
        /// <summary>
        /// Constructor
        /// <para>Copy-Constructor</para>
        /// </summary>
        /// <param name="song">Song to copy</param>
        public Song(Song song)
        {
            SongTitle = song.SongTitle;
            BPM = song.BPM;
            SongDuration = song.SongDuration;

            Notation = new List<INote>();
            foreach(INote n in song.Notation)
            {
                Notation.Add(new INote(n));
            }
        }
    }

    /// <summary>
    /// XML ELEMENT
    /// <para>Contains either a note or a chord.</para>
    /// </summary>
    [Serializable]
    public class INote
    {
        /// <summary>
        /// Note that is stored in this INote.
        /// </summary>
        /// <value>Gets/Sets the note. A default value has to be initialised or else the xml serialisation fails.</value>
        public Note Note { get; set; } = new Note();
        /// <summary>
        /// Chord that is stored in this INote.
        /// </summary>
        /// <value>Gets/Sets the chord. A default value has to be initialised or else the xml serialisation fails.</value>
        public Chord Chord { get; set; } = new Chord();
        /// <summary>
        /// Set flag when the content of this INote is an actual Note
        /// </summary>
        /// <value>Gets/Sets the IsNote bool field</value>
        public bool IsNote { get; set; }
        /// <summary>
        /// Constructor
        /// <para>Use if you want to store a <see cref="SongModel.Note"/>.</para>
        /// </summary>
        /// <param name="note">Note that will be stored in this INote instance.</param>
        public INote(Note note)
        {
            Note = note;
            Chord = null;
            IsNote = true;
        }
        /// <summary>
        /// Constructor
        /// <para>Use if you want to store a <see cref="SongModel.Chord"/>.</para>
        /// </summary>
        /// <param name="chord">Chord that will be stored in this INote instance.</param>
        public INote(Chord chord)
        {
            Chord = chord;
            Note = null;
            IsNote = false;
        }
        /// <summary>
        /// Constructor
        /// <para>Copy-Constructor</para>
        /// </summary>
        /// <param name="note">INote to copy</param>
        public INote(INote note)
        {
            if(note.IsNote == false)
            {
                Note = null;
                Chord = new Chord(note.Chord);
                IsNote = false;
            }
            else
            {
                Note = new Note(note.Note);
                Chord = null;
                IsNote = true;
            }
        }
        /// <summary>
        /// Constructor
        /// <para>DO NOT USE!
        /// only needed for xml (de-)serialisation!</para>
        /// </summary>
        public INote()
        {

        }
    }
}
