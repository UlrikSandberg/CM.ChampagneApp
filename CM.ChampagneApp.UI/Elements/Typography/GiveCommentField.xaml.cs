using System;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Windows.Input;
using CM.ChampagneApp.UI.CustomEventArgs;
using Microsoft.AppCenter.Crashes;
using System.Runtime.InteropServices.ComTypes;
using CM.ChampagneApp.UI.UIFacade.Models.UICommentModels;
using System.Collections;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;
using System.Threading.Tasks;

namespace CM.ChampagneApp.UI.Elements.Typography
{
    public partial class GiveCommentField : ContentView
    {
        private string PlaceHolderText { get; set; }

		public ICommand KeyboardRaised { get; set; }

		public delegate void KeyboardRaisedEvent(object sender, KeyboardRaisedEventArgs args);
		public event KeyboardRaisedEvent OnKeyboardRaisedEvent;

		public delegate void KeyboardUnfocused(object sender, System.EventArgs e);
		public event KeyboardUnfocused OnKeyboardUnfocused;

		public delegate void PostComment(object sender, PostCommentEventArgs args);
		public event PostComment OnPostComment;

		public delegate void EditCommentCancel(object sender, System.EventArgs e);
		public event EditCommentCancel OnEditCommentCancel;

		public delegate void UpdateComment(object sender, UpdateCommentEventArgs args);
		public event UpdateComment OnUpdateComment;

		private string previousComment = null;

        public GiveCommentField()
        {
            InitializeComponent();

            PlaceHolderText = "Add comment or a question";
            TextArea.Text = PlaceHolderText;
            TextArea.TextColor = Color.LightGray;
			KeyboardRaised = new Command<KeyboardRaisedEventArgs>(async (x) => await OnKeyboardRaised(x));
        }

		void Handle_OnInvalidateMeasure(object sender, System.EventArgs e)
		{
            //
		}
              
		public SuccesStateEnum SuccesState
		{
			get { return (SuccesStateEnum)GetValue(SuccesStateProperty); }
			set { SetValue(SuccesStateProperty, value); }
		}

		public static BindableProperty SuccesStateProperty =
			BindableProperty.Create(nameof(SuccesState), typeof(SuccesStateEnum), typeof(GiveCommentField), SuccesStateEnum.Default, propertyChanged: SuccesStateChanged);
                
		private static void SuccesStateChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (GiveCommentField)bindable;
            
			if(control != null)
			{
				control.OnSuccesStateChanged((SuccesStateEnum)newValue);
			}            
		}

		private void OnSuccesStateChanged(SuccesStateEnum succesState)
		{
			if(succesState.Equals(SuccesStateEnum.Error))
			{
				TextArea.Text = previousComment;
			}         
		}

		public UICommentModel EditModel
		{
			get { return (UICommentModel)GetValue(EditModelProperty); }
			set { SetValue(EditModelProperty, value); }
		}

		public static BindableProperty EditModelProperty =
			BindableProperty.Create(nameof(EditModel), typeof(UICommentModel), typeof(GiveCommentField), propertyChanged: EditModelChanged);

		private bool IsEditing { get; set; } = false;
		private static void EditModelChanged(BindableObject bindable, object oldValue, object newValue)
		{
			var control = (GiveCommentField)bindable;

			if(control != null)
			{
				if (newValue != null)
				{
					var commentModel = (UICommentModel)newValue;
					control.EditTextArea.Text = commentModel.Comment;
					control.IsEditing = true;
					control.EditView.IsVisible = true;
					control.ChatGrid.IsVisible = false;
					control.TextArea.Focus();
				}
			}
		}

		void Handle_CancelEdit(object sender, System.EventArgs e)
		{
			EditTextArea.Text = "";
			IsEditing = false;
			EditView.IsVisible = false;
			ChatGrid.IsVisible = true;
			if(OnEditCommentCancel != null)
			{
				OnEditCommentCancel(sender, e);
			}
		}

		void Handle_UpdateComment(object sender, System.EventArgs e)
		{
            
			if(OnUpdateComment != null)
			{
				if (EditModel != null)
				{
					OnUpdateComment(sender, new UpdateCommentEventArgs(EditModel, EditTextArea.Text));
				}
			}
			EditTextArea.Text = "";
            IsEditing = false;
            EditView.IsVisible = false;
            ChatGrid.IsVisible = true;
		}

		private async Task OnKeyboardRaised(KeyboardRaisedEventArgs args)
		{
			if(OnKeyboardRaisedEvent != null)
			{
				OnKeyboardRaisedEvent(this, args);
			}

			if (args.KeyboardHeight > 0)
			{
				await this.TranslateTo(0, -args.KeyboardHeight, 200);
			}
			else
			{
				await this.TranslateTo(0, 0, 200);
			}
		}

        void Handle_TextChanged(object sender, Xamarin.Forms.TextChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Text Changes");
        }

        void Handle_Completed(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Completed");         
			if(OnKeyboardUnfocused != null && !TextArea.Text.Equals(PlaceHolderText))
			{
				OnKeyboardUnfocused(sender, e);
			}
        }

        void Handle_Focused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            if (TextArea.Text.Equals(PlaceHolderText))
            {
                TextArea.Text = "";
                TextArea.TextColor = Color.White;
            }
        }

        void Handle_Unfocused(object sender, Xamarin.Forms.FocusEventArgs e)
        {
            var trimmedText = TextArea.Text.Trim();
			if (trimmedText.Equals("") || trimmedText.Equals(PlaceHolderText))
            {
                TextArea.Text = PlaceHolderText;
                TextArea.TextColor = Color.LightGray;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Comment entered");
				if(IsEditing)
				{
					IsEditing = false;
					TextArea.Text = PlaceHolderText;
                    TextArea.TextColor = Color.LightGray; 
				}
            }
        }

		void Handle_PostComment(object sender, System.EventArgs e)
		{

			if(TextArea.Text.Equals(PlaceHolderText))
			{
				return;
			}         
			if(OnPostComment != null)
			{
				OnPostComment(sender, new PostCommentEventArgs(TextArea.Text));
				previousComment = TextArea.Text;
				TextArea.Text = PlaceHolderText;
			}
		}
    }
}
