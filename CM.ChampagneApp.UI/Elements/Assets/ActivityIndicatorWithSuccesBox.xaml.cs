using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Threading.Tasks;
using Lottie.Forms;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.Elements.Assets
{
    public partial class ActivityIndicatorWithSuccesBox : ContentView
    {

		public delegate void DidDisappear(object sender, System.EventArgs e);
		public event DidDisappear OnDidDisappear;

        public ActivityIndicatorWithSuccesBox()
        {
            InitializeComponent();
        }

        public string Text
		{
			get { return (string)GetValue(TextProperty); }
			set { SetValue(TextProperty, value); }
		}

		public static BindableProperty TextProperty =
			BindableProperty.Create(nameof(Text), typeof(string), typeof(ActivityIndicatorWithSuccesBox));

		public async void Handle_OnSuccesFinish(object sender, System.EventArgs e)
		{
			await Task.Delay(500);
            await contentFrame.FadeTo(0, 200);
			CheckMarkAnimation.IsVisible = false;
            contentFrame.IsVisible = false;
			IsLoading = false;
			IsDoneUploadingWithError = false;
			IsDoneUploadingWithSucces = false;
			contentFrame.Opacity = 1;



            if(OnDidDisappear != null)
            {
                OnDidDisappear?.Invoke(this, new System.EventArgs());
            }

            if (DissappearCommand != null && DissappearCommand.CanExecute(null))
            {
                DissappearCommand.Execute(null);
            }
        }

        public ICommand DissappearCommand
		{
			get { return (ICommand)GetValue(DissappearCommandProperty); }
			set { SetValue(DissappearCommandProperty, value); }
		}

		public static BindableProperty DissappearCommandProperty =
			BindableProperty.Create(nameof(DissappearCommand), typeof(ICommand), typeof(ActivityIndicatorWithSuccesBox));

        public bool IsLoading
		{
			get { return (bool)GetValue(IsLoadingProperty); }
			set { SetValue(IsLoadingProperty, value); }
		}

		public static BindableProperty IsLoadingProperty =
			BindableProperty.Create(nameof(IsLoading), typeof(bool), typeof(ActivityIndicatorWithSuccesBox), false, propertyChanged: IsLoadingChanged);

		private static void IsLoadingChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ActivityIndicatorWithSuccesBox)bindable;

			if(control != null)
			{
				if ((bool)newValue)
				{
					control.IsDoneUploadingWithError = false;
					control.IsDoneUploadingWithSucces = false;
					control.LoadingIndicator.IsVisible = true;
					control.contentFrame.Opacity = 0;
					control.contentFrame.IsVisible = true;
					control.contentFrame.FadeTo(1, 200);
				}
			}
		}

		public bool IsDoneUploadingWithSucces
		{
			get { return (bool)GetValue(IsDoneUploadingWithSuccesProperty); }
			set { SetValue(IsDoneUploadingWithSuccesProperty, value); }
		}

		public static BindableProperty IsDoneUploadingWithSuccesProperty =
			BindableProperty.Create(nameof(IsDoneUploadingWithSucces), typeof(bool), typeof(ActivityIndicatorWithSuccesBox), false, propertyChanged: IsDoneUploadingWithSuccesChanged);
    
        public bool IsDoneUploadingWithError
		{
			get { return (bool)GetValue(IsDoneUploadingWithErrorProperty); }
			set { SetValue(IsDoneUploadingWithErrorProperty, value); }
		}

		public static BindableProperty IsDoneUploadingWithErrorProperty =
			BindableProperty.Create(nameof(IsDoneUploadingWithError), typeof(bool), typeof(ActivityIndicatorWithSuccesBox), false, propertyChanged: IsDoneUploadingWithErrorChanged);

		private static void IsDoneUploadingWithErrorChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ActivityIndicatorWithSuccesBox)bindable;

			if(control != null)
			{
				if ((bool)newValue)
				{
					control.AnimateError();
				}
			}
		}

        private async Task AnimateError()
		{
			contentFrame.Opacity = 0;
			contentFrame.IsVisible = false;
			CheckMarkAnimation.IsVisible = false;

            if (OnDidDisappear != null)
            {
                OnDidDisappear?.Invoke(this, new EventArgs());
            }

            if (DissappearCommand != null && DissappearCommand.CanExecute(null))
            {
                DissappearCommand.Execute(null);
            }
        }

        private static void IsDoneUploadingWithSuccesChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (ActivityIndicatorWithSuccesBox)bindable;

			if(control != null)
			{
				if ((bool)newValue)
				{
					control.AnimateSucces();
				}
			}
		}

        private void AnimateSucces()
		{
			LoadingIndicator.IsVisible = false;
			CheckMarkAnimation.IsVisible = true;
			CheckMarkAnimation.Play();
		}
	}
}
