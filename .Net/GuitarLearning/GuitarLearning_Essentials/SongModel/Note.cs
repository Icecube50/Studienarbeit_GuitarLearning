using System;

namespace GuitarLearning_Essentials.SongModel
{
    /// <summary>
    /// XML ELEMENT
    /// <para>Stores all information needed for a note</para>
    /// </summary>
    [Serializable]
    public class Note
    {
        /// <summary>
        /// Musical name of the note.
        /// </summary>
        /// <value>Gets/Sets the Name string field.</value>
        public string Name { get; set; }
        /// <summary>
        /// ID that is used in the html sheet and refers to this note.
        /// </summary>
        /// <value>Gets/Sets the ID string field.</value>
        public string ID { get; set; }
        /// <summary>
        /// Number of the beat this note is in.
        /// </summary>
        /// <value>Gets/Sets the BeatNumber int field.</value>
        public int BeatNumber { get; set; }
        /// <summary>
        /// Number of the stroke this note is on.
        /// </summary>
        /// <value>Gets/Sets the StrokeNumber double field.</value>
        public double StrokeNumber { get; set; }
        /// <summary>
        /// Duration of this note in milliseconds.
        /// </summary>
        /// <value>Gets/Sets the Duration double field.</value>
        public double Duration { get; set; }
        /// <summary>
        /// Constructor
        /// <para>Only needed for xml (de-)serialisation.</para>
        /// </summary>
        public Note()
        {

        }
    }
}
