using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;
using FFImageLoading.Forms;
using CM.ChampagneApp.UI.Dependency;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class CircleImageButton : ContentView
    {

        public delegate void ImageChosen(object sender, ImageChosenEventArgs args);
        public event ImageChosen OnImageChosen;

        public CircleImageButton()
        {
            InitializeComponent();
        }

        public bool Shadow
        {
            get { return (bool)GetValue(ShadowProperty); }
            set { SetValue(ShadowProperty, value); }
        }

        public static BindableProperty ShadowProperty =
            BindableProperty.Create(nameof(Shadow), typeof(bool), typeof(CircleImageButton), false);

        public bool ChoosePhotoEnabled
        {
            get { return (bool)GetValue(ChoosePhotoEnabledProperty); }
            set { SetValue(ChoosePhotoEnabledProperty, value); }
        }

        public static BindableProperty ChoosePhotoEnabledProperty =
            BindableProperty.Create(nameof(ChoosePhotoEnabled), typeof(bool), typeof(CircleImageButton), false);

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        public static BindableProperty SourceProperty =
            BindableProperty.Create(nameof(Source), typeof(string), typeof(CircleImageButton), "PlaceholderProfileImg", propertyChanged: DiameterChanged);


        public double Diameter
        {
            get { return (double)GetValue(DiameterProperty); }
            set { SetValue(DiameterProperty, value); }
        }

        public static BindableProperty DiameterProperty =
            BindableProperty.Create(nameof(Diameter), typeof(double), typeof(CircleImageButton), 0.0, propertyChanged: DiameterChanged);

        private static void DiameterChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var Control = (CircleImageButton)bindable;

            if (Control != null)
            {
                Control.SetCornerRadius();
            }
        }

        private void SetCornerRadius()
        {
            if (Diameter > 0)
            {
                Container.CornerRadius = (int)Diameter / 2;
            }

        }

        private async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            var pic = DependencyService.Get<IPicturePicker>();
            using (var stream = await pic.GetImageStreamAsync())
            {
                if (stream != null)
                {
                    ImagePresenter.Source = ImageSource.FromStream(() => stream);
                    SetCornerRadius();
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
    }
}
