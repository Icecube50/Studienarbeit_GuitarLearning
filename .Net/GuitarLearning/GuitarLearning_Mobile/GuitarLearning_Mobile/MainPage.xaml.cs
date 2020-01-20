using Android.Content.Res;
using GuitarLearning_Mobile.Pages;
using NAudio.Wave;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuitarLearning_Mobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            optAudioRecording.PageLink = new pgAudioRecording();
            optAudioRecording.OptionName = "Audio Recorder";

            optNetworkTesting.PageLink = new pgNetworkTesting();
            optNetworkTesting.OptionName = "Network Testing";

            optSongTemplate.PageLink = new pgSongPage_Template();
            optSongTemplate.OptionName = "Song Template";
        }
    }

    public static class AssetStorage
    {
        public static AssetManager Manager { get; set; }
    }
}
