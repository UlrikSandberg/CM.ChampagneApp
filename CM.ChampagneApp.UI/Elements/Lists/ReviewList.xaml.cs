using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using CM.ChampagneApp.UI.Elements.Cards;
using CM.ChampagneApp.UI.Resources;
using CM.ChampagneApp.UI.Elements.Assets;
using CM.ChampagneApp.UI.CustomEventArgs;
using Xamarin.Forms;
using CM.ChampagneApp.UI.UIFacade.Models.UITastingModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Elements.Lists
{
    public partial class ReviewList : ContentView
    {
        public delegate void ReviewChosen(object sender, ReviewClickedEventArgs args);
        public event ReviewChosen OnReviewChosen;

		public delegate void ProfileNameClicked(object sender, ProfileNameClickedEventArgs args);
        public event ProfileNameClicked OnProfileNameClicked;

        public delegate void LikeBtnClicked(object sender, ReviewClickedEventArgs args);
        public event LikeBtnClicked OnLikeBtnClicked;

        public delegate void CommentBtnClicked(object sender, ReviewClickedEventArgs args);
        public event CommentBtnClicked OnCommentBtnClicked;

        public ReviewList()
        {
            InitializeComponent();
        }

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }


        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(ReviewList), propertyChanged: ItemSourceChanged);


        private static void ItemSourceChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var reviewList = (ReviewList)bindable;

            if(reviewList != null)
            {
                reviewList.UpdateList();
            }
        }


        private void UpdateList()
        {
            if(ItemSource != null)
            {
				ReviewPresenter.Children.Clear();
                var ItemNumber = 1;
				var ItemSourceLength = (ObservableCollection<UITasting>)ItemSource;
                foreach(Object Review in ItemSource)
                {

                    var review = new ReviewCard();
                    review.BindingContext = Review;
					review.OnInspectReview += Handle_OnInspectReview;
					review.OnProfileNameClicked += Handle_OnProfileNameClicked;
					review.OnLikeBtnClicked += Handle_OnLikeBtnClicked;
					review.OnCommentBtnClicked += Handle_OnCommentBtnClicked;
                    ReviewPresenter.Children.Add(review);               

                    if(ItemNumber != ItemSourceLength.Count)
                    {
                        ReviewPresenter.Children.Add(new ListSeperator
                        {                     
                            Margin = new Thickness(0,0,-25,0),                     
                        });

                    }               
                    ItemNumber += 1;               
                }            
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("No review has been given");
            }
        }

        private void Handle_OnInspectReview(object sender, ReviewClickedEventArgs args)
        {
			if (OnReviewChosen != null)
			{
				OnReviewChosen(sender, args);
			}
        }

		private void Handle_OnProfileNameClicked(object sender, ProfileNameClickedEventArgs args)
		{
			if(OnProfileNameClicked != null)
			{
				OnProfileNameClicked(sender, args);
			}
		}

		private void Handle_OnLikeBtnClicked(object sender, ReviewClickedEventArgs args)
		{
			if(OnLikeBtnClicked != null)
			{
				OnLikeBtnClicked(sender, args);
			}
		}

		private void Handle_OnCommentBtnClicked(object sender, ReviewClickedEventArgs args)
		{
			if(OnCommentBtnClicked != null)
			{
				OnCommentBtnClicked(sender, args);
			}
		}
    }
}
