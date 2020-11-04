using System;
using System.Collections;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace CM.ChampagneApp.UI.Elements.Sections
{
    public partial class ChampagneProfileReviewSection : ContentView
    {
        public delegate void ShowAllReviewsAndRatings(object sender, System.EventArgs e);
        public event ShowAllReviewsAndRatings OnShowAllreviewsAndRatings;

        public delegate void InspectReview(object sender, ReviewClickedEventArgs args);
        public event InspectReview OnInspectReview;

		public delegate void ProfileNameClicked(object sender, ProfileNameClickedEventArgs args);
        public event ProfileNameClicked OnProfileNameClicked;

        public delegate void LikeBtnClicked(object sender, ReviewClickedEventArgs args);
        public event LikeBtnClicked OnLikeBtnClicked;

        public delegate void CommentBtnClicked(object sender, ReviewClickedEventArgs args);
		public event CommentBtnClicked OnCommentBtnClicked;

        public static BindableProperty SeeAllCommandProperty =
            BindableProperty.Create(nameof(SeeAllCommand), typeof(ICommand), typeof(ChampagneProfileReviewSection));

        public ChampagneProfileReviewSection()
        {
            InitializeComponent();
        }

        public ICommand SeeAllCommand
        {
            get { return (ICommand)GetValue(SeeAllCommandProperty); }
            set { SetValue(SeeAllCommandProperty, value); }
        }

        public bool IsDownloadingTastings
		{
			get { return (bool)GetValue(IsDownloadingTastingsProperty); }
			set { SetValue(IsDownloadingTastingsProperty, value); }
		}

		public static BindableProperty IsDownloadingTastingsProperty =
			BindableProperty.Create(nameof(IsDownloadingTastings), typeof(bool), typeof(ChampagneProfileReviewSection), false, propertyChanged: IsDownloadingTastingsChanged);

		private static void IsDownloadingTastingsChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var reviewRatingSection = (ChampagneProfileReviewSection)bindable;
		
			if(reviewRatingSection != null)
			{
				if (newValue != null)
				{
					reviewRatingSection.loadingIndicator.IsRunning = (bool)newValue;
					reviewRatingSection.loadingIndicator.IsVisible = (bool)newValue;
				}
			}
		}      

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }

        }

        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(ChampagneProfileReviewSection), propertyChanged: ItemSourceChanged);


        private static void ItemSourceChanged(BindableObject bindable, object newValue, object oldValue)
        {         
            var ReviewRatingSection = (ChampagneProfileReviewSection)bindable;
         
            if(ReviewRatingSection != null)
            {
                ReviewRatingSection.UpdateView();
            }
        }

        private void UpdateView()
        {
			if (ItemSource != null)
			{
				var ItemSourceLength = (ObservableCollection<UITasting>)ItemSource;

				if (ItemSourceLength.Count == 0 || ItemSource == null)
				{
					Reviews.IsVisible = false;
					ReviewsBtn.IsVisible = false;
					NoReviewText.IsVisible = true;
				}
				else
				{
					Reviews.IsVisible = true;
					ReviewsBtn.IsVisible = true;
					NoReviewText.IsVisible = false;
				}
			}
        }

        void Handle_OnClickedSeeAllTastings(object sender, System.EventArgs e)
        {
            if(OnShowAllreviewsAndRatings != null)
            {
                OnShowAllreviewsAndRatings(sender, e);
            }
            if(SeeAllCommand != null)
            {
                if(SeeAllCommand.CanExecute(null))
                {
                    SeeAllCommand.Execute(null);
                }
            }
        }

        void Handle_OnReviewChosen(object sender, ReviewClickedEventArgs args)
        {
            if(OnInspectReview != null)
            {
                OnInspectReview(sender, args);
            }
        }

		void Handle_OnProfileNameClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ProfileNameClickedEventArgs args)
		{
			if(OnProfileNameClicked != null)
			{
				OnProfileNameClicked(sender, args);
			}
		}

		void Handle_OnLikeBtnClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
		{
			if(OnLikeBtnClicked != null)
			{
				OnLikeBtnClicked(sender, args);
			}
		}

		void Handle_OnCommentBtnClicked(object sender, CM.ChampagneApp.UI.CustomEventArgs.ReviewClickedEventArgs args)
		{
			if(OnCommentBtnClicked != null)
			{
				OnCommentBtnClicked(sender, args);
			}
		}
    }
}
