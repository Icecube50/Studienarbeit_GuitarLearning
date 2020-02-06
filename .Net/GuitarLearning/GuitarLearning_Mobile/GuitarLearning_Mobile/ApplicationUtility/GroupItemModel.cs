using GuitarLearning_Mobile.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GuitarLearning_Mobile.ApplicationUtility
{
    /// <summary>
    /// Model-Class for the ListView groups
    /// </summary>
    public class GroupItemModel : List<ItemModel>
    {
        /// <summary>
        /// Group title, which will be displayed in the ListView
        /// </summary>
        public string Title { get; set; }
        private GroupItemModel(string title)
        {
            Title = title;
        }
        /// <summary>
        /// Complete list of all items
        /// </summary>
        public static IList<GroupItemModel> All { get; private set; }

        static GroupItemModel()
        {
            List<GroupItemModel> Groups = new List<GroupItemModel>()
            {
                new GroupItemModel("Funktionen")
                {
                    new ItemModel("Network Testing", new pgNetworkTesting()),
                    new ItemModel("Audio Recording", new pgAudioRecording())
                },
                new GroupItemModel("Lieder")
                {
                    new ItemModel("Note Test", new pgSongPage_Template("Test1")),
                    new ItemModel("Chord Test", new pgSongPage_Template("ChordTest")),
                    new ItemModel("Trumpet Test", new pgSongPage_Template("TrumpetTest")),
                    new ItemModel("Static Test", new pgSongPage_Template("StaticTest"))
                }
            };
            All = Groups;
        }
    }
}
