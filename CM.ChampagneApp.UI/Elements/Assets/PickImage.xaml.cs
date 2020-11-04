using System;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.Dependency;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class PickImage : ContentView
    {
        public delegate void ImageChosen(object sender, ImageChosenEventArgs args);
        public event ImageChosen OnImageChosen;

        public PickImage()
        {
            InitializeComponent();
        }

        async void Handle_Clicked(object sender, EventArgs e)
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
            BindableProperty.Create(nameof(Source), typeof(string), typeof(CircleImageButton), "PlaceholderCoverImg", propertyChanged: SourceChanged);

        private static void SourceChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var Control = (PickImage)bindable;
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
