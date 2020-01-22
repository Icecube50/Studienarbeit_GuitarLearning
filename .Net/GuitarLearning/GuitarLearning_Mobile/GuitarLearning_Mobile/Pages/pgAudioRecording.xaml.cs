using GuitarLearning_Essentials;
using GuitarLearning_Mobile.UtilityClasses;
using Plugin.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuitarLearning_Mobile.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class pgAudioRecording : ContentPage
    {
        private static readonly BindableProperty IsRecordingProperty =
            BindableProperty.Create("IsRecording", typeof(bool), typeof(pgAudioRecording));

        private event EventHandler IsRecordingChanged;

        private bool IsRecording
        {
            get { return (bool)GetValue(IsRecordingProperty); }
            set { SetValue(IsRecordingProperty, value); }
        }

        private AudioRecording audioRecording { get; set; } = null;
        public pgAudioRecording()
        {
            InitializeComponent();

            IsRecordingChanged += OnIsRecordingChanged;
            audioRecording = new AudioRecording();
            audioRecording.GetHelper().UpdateUI += async (s, e) =>
            {
                await UpdateChordLabel(s);
            };

            btnRecording.Clicked += async (s, e) =>
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Plugin.Permissions.Abstractions.Permission.Microphone);
                if(status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    if(await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Plugin.Permissions.Abstractions.Permission.Microphone))
                    {
                        await DisplayAlert("Permission Request", "In order for the app to record audio, permission must first be granted", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new Plugin.Permissions.Abstractions.Permission[] { Plugin.Permissions.Abstractions.Permission.Microphone });
                    status = results[Plugin.Permissions.Abstractions.Permission.Microphone];
                }

                if(status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    ChangeIsRecording(!IsRecording);
                }
                else
                {
                    await DisplayAlert("Microphone Denied", "Can not continue, try again.", "OK");
                }
            };
        }

        private void OnIsRecordingChanged(object sender, EventArgs e)
        {
            ChangeLabels();
            if (IsRecording)
            {
                btnRecording.Text = "Stop";
                audioRecording.StartRecording();
            }
            else 
            {
                btnRecording.Text = "Start";
                audioRecording.StopRecording();
                lbCurrentChord.Text = "No Chord Detected";
            }
        }

        private void ChangeIsRecording(bool value)
        {
            IsRecording = value;
            IsRecordingChanged?.Invoke(this, new EventArgs());
        }

        private void ChangeLabels()
        {
            if (IsRecording)
            {
                lbRecordingState.Text = "Recording = ON";
            }
            else
            {
                lbRecordingState.Text = "Recording = OFF";
            }
        }

        private void OnLeave(object sender, EventArgs e)
        {
            audioRecording.CleanUp();
            audioRecording = null;
        }

        private async Task UpdateChordLabel(object model)
        {
            if(model is EssentiaModel)
            {
                EssentiaModel essentiaModel = model as EssentiaModel;
                string[] chords = essentiaModel.chordData.Split(';');

                var occurence = GetOccurence(chords);
                lbCurrentChord.Text = GetChordFromOccurence(occurence);
                await Task.Delay(1);
            }
        }

        private Dictionary<string, int> GetOccurence(string[] chords)
        {
            Dictionary<string, int> ChordOccurence = new Dictionary<string, int>();
            foreach (string chord in chords)
            {
                if (!ChordOccurence.ContainsKey(chord))
                {
                    ChordOccurence.Add(chord, 1);
                }
                else
                {
                    ChordOccurence[chord]++;
                }
            }
            return ChordOccurence;
        }

        private string GetChordFromOccurence(Dictionary<string, int> occurence)
        {
            KeyValuePair<string, int> max;
            bool first = true;
            foreach(var kvp in occurence)
            {
                if (first)
                {
                    max = kvp;
                    first = !first;
                    continue;
                }

                if (kvp.Value > max.Value)
                    max = kvp;
            }
            return max.Key;
        }
    }
}