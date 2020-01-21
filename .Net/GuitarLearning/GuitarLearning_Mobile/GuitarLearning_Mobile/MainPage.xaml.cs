﻿using Android.Content.Res;
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

            var tapGestureRecognizerAudio = new TapGestureRecognizer();
            tapGestureRecognizerAudio.Tapped += OnAudioContainer_Tapped;
            grdAudio.GestureRecognizers.Add(tapGestureRecognizerAudio);

            var tapGestureRecognizerNetwork = new TapGestureRecognizer();
            tapGestureRecognizerNetwork.Tapped += OnNetworkContainer_Tapped;
            grdNetwork.GestureRecognizers.Add(tapGestureRecognizerNetwork);

            var tapGestureRecognizerSong = new TapGestureRecognizer();
            tapGestureRecognizerSong.Tapped += OnSongContainer_Tapped;
            grdSong.GestureRecognizers.Add(tapGestureRecognizerSong);
        }

        private void OnSongContainer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new pgSongPage_Template());
        }

        private void OnNetworkContainer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new pgNetworkTesting());
        }

        private void OnAudioContainer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new pgAudioRecording());
        }
    }

    public static class AssetStorage
    {
        public static AssetManager Manager { get; set; }
    }
}
