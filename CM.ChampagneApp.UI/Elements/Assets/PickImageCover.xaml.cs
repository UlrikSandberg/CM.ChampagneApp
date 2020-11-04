using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Dependency;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class PickImageCover : ContentView
    {
        public delegate void ImageChosen(object sender, ImageChosenEventArgs args);
        public event ImageChosen OnImageChosen;

        public PickImageCover()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var pic = DependencyService.Get<IPicturePicker>();
            using (var stream = await pic.GetImageStreamAsync())
            {
                if (stream != null)
                {
                    ImagePresenter.Source = ImageSource.FromStream(() => stream);
                    await Task.Delay(1000);
                    await SendImage();
                }
            }
        }

        private async Task SendImage()
        {

            var jpgImage = await ImagePresenter.GetImageAsJpgAsync(90);

            OnImageChosen?.Invoke(this, new ImageChosenEventArgs(jpgImage));
        }

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(PickImageCover), "PlaceholderCoverImg", propertyChanged: SourceChanged);

        private static void SourceChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var Control = (PickImageCover)bindable;
            if (Control != null)
            {
                Control.SetCorrectImage();
            }
        }

        private void SetCorrectImage()
        {
            if (string.IsNullOrEmpty(Source))
            {
                ImagePresenter.Source = "PlaceholderCoverImg";
            }
        }
    }
}
