using Android.Content.Res;
using NAudio.Wave;
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
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            string fileName = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

            string root = Directory.GetCurrentDirectory();
            string[] files = Directory.GetFiles(root);
            string[] directories = Directory.GetDirectories(root);

            OutputLabel.Text = "Done";
        }
    }

    public static class AssetStorage
    {
        public static AssetManager Manager { get; set; }
    }
}
