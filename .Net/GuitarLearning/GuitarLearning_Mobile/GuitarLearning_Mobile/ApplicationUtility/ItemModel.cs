using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GuitarLearning_Mobile.ApplicationUtility
{
    /// <summary>
    /// Model that is used in the list view to render all page links
    /// </summary>
    public class ItemModel
    {
        string _name;
        ContentPage _page;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name that is displayed on screen</param>
        /// <param name="contentPage">Page that will be pushed onto the navigation stack on the tap-event</param>
        public ItemModel(string name, ContentPage contentPage)
        {
            _name = name;
            _page = contentPage;
        }
        /// <summary>
        /// Gets the _name string field value
        /// </summary>
        /// <returns>Name of this object</returns>
        public override string ToString()
        {
            return _name;
        }
        /// <summary>
        /// Gets the _page ContentPage field value
        /// </summary>
        /// <returns>Page of this object</returns>
        public ContentPage GetPage()
        {
            return _page;
        }
    }
}
