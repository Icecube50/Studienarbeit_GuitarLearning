﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace GuitarLearning_Mobile.ApplicationUtility
{
    /// <summary>
    /// Model-Class of a ListView item
    /// </summary>
    public class ItemModel
    {
        /// <summary>
        /// Page that will be loaded on the tapped event.
        /// </summary>
        private ContentPage _page;
        /// <summary>
        /// Name which the ListView will display
        /// </summary>
        /// <value>Gets/Sets the PageName string field.</value>
        private string _pageName;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pageName">Display name</param>
        /// <param name="page">Link to the page</param>
        public ItemModel(string pageName, ContentPage page)
        {
            _page = page;
            _pageName = pageName;
        }
        /// <summary>
        /// Returns the _page field
        /// </summary>
        /// <returns>Page that can be pushed onto the navigation stack.</returns>
        public ContentPage GetPage()
        {
            return _page;
        }
        public override string ToString()
        {
            return _pageName; 
        }
    }
}
