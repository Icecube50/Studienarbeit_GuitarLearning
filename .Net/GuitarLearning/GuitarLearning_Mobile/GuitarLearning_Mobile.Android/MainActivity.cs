﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using GuitarLearning_Mobile.ApplicationUtility;
using static Android.OS.PowerManager;
using Android.Content;

namespace GuitarLearning_Mobile.Droid
{
    [Activity(Label = "GuitarLearning_Mobile", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.FullSensor)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            AssetStorage.Manager = this.Assets;

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }
        private WakeLock wakeLock;
        protected override void OnPause()
        {
            base.OnPause();
            wakeLock?.Release();
        }

        protected override void OnResume()
        {
            base.OnResume();
            PowerManager powerManager = (PowerManager)this.GetSystemService(Context.PowerService);
            wakeLock = powerManager.NewWakeLock(WakeLockFlags.ScreenBright, "MyLock");
            wakeLock.Acquire();
        }

        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Log("Error:" + sender.ToString());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}