namespace GuitarLearning_Essentials
{
    /// <summary>
    /// Data-Model that is used by the API as well as the mobile application.
    /// It provides storage for raw and analysed audio data aswell as a timestamp.
    /// </summary>
    public class EssentiaModel
    {
        /// <summary>
        /// Storage for raw audio data.
        /// </summary>
        /// <value>Gets/Sets the audioData float[] field.</value>
        public float[] audioData { get; set; }
        /// <summary>
        /// Storage for analysed data.
        /// </summary>
        /// <value>Gets/Sets the chordData string field.</value>
        public string chordData { get; set; }
        /// <summary>
        /// Storage for the timestamp (time since start in milliseconds).
        /// </summary>
        /// <value>Gets/Sets the time double field.</value>
        public double time { get; set; }

        /// <summary>
        /// Constructor
        /// <para>An empty constructor is needed for xml (de-)serialisation.</para>
        /// </summary>
        public EssentiaModel()
        {
            this.audioData = null;
            this.chordData = string.Empty;
            this.time = 0.0;
        }
        /// <summary>
        /// Constructor
        /// <para>Used to set only the raw audio and the timestamp (used by the mobile application).</para>
        /// </summary>
        /// <param name="audioData">raw audio data</param>
        /// <param name="time">time of recording</param>
        public EssentiaModel(float[] audioData, double time)
        {
            this.audioData = audioData;
            this.chordData = string.Empty;
            this.time = time;
        }
        /// <summary>
        /// Constructor
        /// <para>Used to set only the analysed data (used by the API).</para>
        /// </summary>
        /// <param name="chordData">analysed audio data</param>
        public EssentiaModel(string chordData)
        {
            this.audioData = null;
            this.chordData = chordData;
            this.time = 0.0;
        }


    }
}
