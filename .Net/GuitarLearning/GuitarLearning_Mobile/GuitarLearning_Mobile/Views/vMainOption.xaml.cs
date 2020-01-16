using GuitarLearning_Mobile.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GuitarLearning_Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class vMainOption : ContentView
    {
        public static readonly BindableProperty OptionNameProperty =
            BindableProperty.Create("OptionName", typeof(string), typeof(vMainOption));

        public string OptionName
        {
            get { return (string)GetValue(OptionNameProperty); }
            set { SetValue(OptionNameProperty, value); }
        }

        public static readonly BindableProperty PageLinkProperty =
            BindableProperty.Create("PageLink", typeof(Page), typeof(vMainOption));
        
        public Page PageLink
        {
            get { return (Page)GetValue(PageLinkProperty); }
            set { SetValue(PageLinkProperty, value); }
        }
        public vMainOption()
        {
            InitializeComponent();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnOptionContainer_Tapped;
            stkOptionContainer.GestureRecognizers.Add(tapGestureRecognizer);
        }

        private void OnOptionContainer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(PageLink);
        }
    }
}