﻿using Android.Content.Res;
using GuitarLearning_Mobile.Pages;
using NAudio.Wave;
using Plugin.Connectivity;
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

            var tapGestureRecognizerTestSong1 = new TapGestureRecognizer();
            tapGestureRecognizerTestSong1.Tapped += OnTestSong1_Tapped;
            grdTestSong1.GestureRecognizers.Add(tapGestureRecognizerTestSong1);

            var tapGestureRecognizerTestSong2 = new TapGestureRecognizer();
            tapGestureRecognizerTestSong2.Tapped += OnTestSong2_Tapped;
            grdTestSong2.GestureRecognizers.Add(tapGestureRecognizerTestSong2);

            Logger.Log("########### New Startup ############");
        }

        private void OnTestSong1_Tapped(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected) DisplayAlert("Fehler", "Stellen Sie sicher das Ihr Gerät mit dem Internet verbunden ist.", "OK");
            else Navigation.PushAsync(new pgSongPage_Template("Test1"));
        }

        private void OnTestSong2_Tapped(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected) DisplayAlert("Fehler", "Stellen Sie sicher das Ihr Gerät mit dem Internet verbunden ist.", "OK");
            else Navigation.PushAsync(new pgSongPage_Template("Test2"));
        }

        private void OnNetworkContainer_Tapped(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected) DisplayAlert("Fehler", "Stellen Sie sicher das Ihr Gerät mit dem Internet verbunden ist.", "OK");
            else Navigation.PushAsync(new pgNetworkTesting());
        }

        private void OnAudioContainer_Tapped(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected) DisplayAlert("Fehler", "Stellen Sie sicher das Ihr Gerät mit dem Internet verbunden ist.", "OK");
            else Navigation.PushAsync(new pgAudioRecording());
        }
    }

    public static class AssetStorage
    {
        public static AssetManager Manager { get; set; }
    }
}
