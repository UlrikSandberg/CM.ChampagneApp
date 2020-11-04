using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using System.Runtime.CompilerServices;
using CM.ChampagneApp.DTO.Models;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class ReviewCard : ContentView
    {
        public delegate void InspectReview(object sender, ReviewClickedEventArgs args);
        public event InspectReview OnInspectReview;
      
		public delegate void ProfileNameClicked(object sender, ProfileNameClickedEventArgs args);
        public event ProfileNameClicked OnProfileNameClicked;

        public delegate void LikeBtnClicked(object sender, ReviewClickedEventArgs args);
        public event LikeBtnClicked OnLikeBtnClicked;

        public delegate void CommentBtnClicked(object sender, ReviewClickedEventArgs args);
        public event CommentBtnClicked OnCommentBtnClicked;

        public ReviewCard()
        {
            InitializeComponent();
            
        }

        void Handle_ArrowClicked(object sender, System.EventArgs e)
        {
			System.Diagnostics.Debug.WriteLine("Inspect tasting");

            if(OnInspectReview != null)
            {
				var tasting = (UITasting)this.BindingContext;
				var args = new ReviewClickedEventArgs(tasting);
                OnInspectReview(sender, args);
            }
        }



        public bool InspectEnabled
        {
            get { return (bool)GetValue(InspectEnabledProperty); }
            set { SetValue(InspectEnabledProperty, value); }
        }

        public static BindableProperty InspectEnabledProperty =
            BindableProperty.Create(nameof(InspectEnabled), typeof(bool), typeof(ReviewCard), true);

        public ReviewModel SingleReviewModel
        {
            get { return (ReviewModel)GetValue(SingleReviewModelProperty); }
            set { SetValue(SingleReviewModelProperty, value); }
        }

        public static BindableProperty SingleReviewModelProperty =
            BindableProperty.Create(nameof(SingleReviewModel), typeof(ReviewModel), typeof(ReviewCard), propertyChanged: SingleReviewModelChanged);
        
        private static void SingleReviewModelChanged(BindableObject bindable, object newValue, object oldValue)
        {

            var element = (ReviewCard)bindable;

            if(element != null)
            {
                element.UpdateReview();
            }

        }

        private void UpdateReview()
        {
            System.Diagnostics.Debug.WriteLine("Should update");
            if(SingleReviewModel != null)
            {
                System.Diagnostics.Debug.WriteLine("Updating review");
                this.BindingContext = SingleReviewModel;
                Username.Text = SingleReviewModel.Username;
                ReviewBody.Text = SingleReviewModel.ReviewContent;
            }
        }

		void Handle_ClickedProfileName(object sender, System.EventArgs e)
		{
			if(OnProfileNameClicked != null)
			{
				var uiTasting = (UITasting)this.BindingContext;
				ProfileNameClickedEventArgs args = new ProfileNameClickedEventArgs(uiTasting.AuthorId);
				OnProfileNameClicked(sender, args);
			}
		}

		void Handle_ClickedLikeBtn(object sender, System.EventArgs e)
		{
			if(OnLikeBtnClicked != null)
			{
				var uiTasting = (UITasting)this.BindingContext;
				ReviewClickedEventArgs args = new ReviewClickedEventArgs(uiTasting);
				OnLikeBtnClicked(this, args);
			}
		}

		void Handle_ClickedCommentBtn(object sender, System.EventArgs e)
		{
			if(OnCommentBtnClicked != null)
			{
				var uiTasting = (UITasting)this.BindingContext;
				ReviewClickedEventArgs args = new ReviewClickedEventArgs(uiTasting);
				OnCommentBtnClicked(sender, args);
			}
		}


        void Handle_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("Height") || e.PropertyName.Equals("Width"))
            {
				if (ProfileImage.Height > 0)
				{
					ProfileImageContainer.CornerRadius = (float)ProfileImage.Height / 2;
				}
            }
		}
	}
}
