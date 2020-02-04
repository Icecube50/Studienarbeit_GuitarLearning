using Android.Content.Res;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuitarLearning_Mobile.ApplicationUtility
{
    /// <summary>
    /// Container class that is used to store a reference to the application's assets so that they can be used later.
    /// </summary>
    public static class AssetStorage
    {
        /// <summary>
        /// AssetManager that points to the application's assets.
        /// </summary>
        /// <value>The Manager property gets/sets the AssetManager field</value>
        public static AssetManager Manager { get; set; }
    }
}
