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

        public pgAudioRecording()
        {
            InitializeComponent();

            IsRecordingChanged += OnIsRecordingChanged;
        }

        private void OnIsRecordingChanged(object sender, EventArgs e)
        {
            ChangeLabels();
            //StartRecording() || StopRecording()
        }

        private void OnRecordingState_Changed(object sender, EventArgs e)
        {
            if (IsRecording)
            {
                btnRecording.Text = "Start";
            }
            else
            {
                btnRecording.Text = "Stop";
            }
            ChangeIsRecording(!IsRecording);
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
    }
}