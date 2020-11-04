using System;
using System.Collections.Generic;
using Xamarin.Forms;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.UIFacade.Models.UICommentModels;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class CommentCard : ContentView
    {      
		public delegate void LikeComment(object sender, ModelClickedEventArgs<UICommentModel> args);
		public event LikeComment OnLikeComment;

		public delegate void ProfileNameClicked(object sender, ProfileNameClickedEventArgs args);
		public event ProfileNameClicked OnProfileNameClicked;

        public CommentCard()
        {
            InitializeComponent();
        }      

		void Handle_LikeComment(object sender, System.EventArgs e)
		{
			if(OnLikeComment != null)
			{
				OnLikeComment(sender, new ModelClickedEventArgs<UICommentModel>((UICommentModel)this.BindingContext));
			}
		}

		void Handle_ProfileNameClicked(object sender, System.EventArgs e)
		{
			if(OnProfileNameClicked != null)
			{
				if (this.BindingContext != null)
				{
					var comment = (UICommentModel)this.BindingContext;
					OnProfileNameClicked(sender, new ProfileNameClickedEventArgs(comment.AuthorId));
				}
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

		void Handle_PropertyChangedLikeButton(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{        
			if(e.PropertyName.Equals("Image"))
			{
				System.Diagnostics.Debug.WriteLine(LikeBtn.Image);
				LikeBtn.HeightRequest = 15;
				LikeBtn.WidthRequest = 15;
			}         
		}
    }
}
