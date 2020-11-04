﻿using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using CoreGraphics;
using CM.ChampagneApp.UI.Elements.Typography;
using CM.ChampagneApp.iOS.Renderers;
using CM.ChampagneApp.UI.CustomEventArgs;

[assembly: ExportRenderer(typeof(CM.ChampagneApp.UI.Elements.Typography.GiveCommentField), typeof(ChatEntryRenderer))]
namespace CM.ChampagneApp.iOS.Renderers
{
    public class ChatEntryRenderer : ViewRenderer //Depending on your situation, you will need to inherit from a different renderer
    {
        NSObject _keyboardShowObserver;
        NSObject _keyboardHideObserver;
        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                RegisterForKeyboardNotifications();
            }

            if (e.OldElement != null)
            {
                UnregisterForKeyboardNotifications();
            }
        }

        void RegisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver == null)
                _keyboardShowObserver = UIKeyboard.Notifications.ObserveWillShow(OnKeyboardShow);
            if (_keyboardHideObserver == null)
                _keyboardHideObserver = UIKeyboard.Notifications.ObserveWillHide(OnKeyboardHide);
        }

        void OnKeyboardShow(object sender, UIKeyboardEventArgs args)
        {

            NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            CGSize keyboardSize = result.RectangleFValue.Size;
            if (Element != null)
            {
				var element = Element as GiveCommentField;

				if(element != null)
				{
					if (element.KeyboardRaised.CanExecute(new KeyboardRaisedEventArgs(keyboardSize.Height - 49)))
					{
						element.KeyboardRaised.Execute(new KeyboardRaisedEventArgs(keyboardSize.Height - 49));
					}
				}

                //Element.Margin = new Thickness(0, 0, 0, keyboardSize.Height); //push the entry up to keyboard height when keyboard is activated
            }
        }

        void OnKeyboardHide(object sender, UIKeyboardEventArgs args)
        {
			NSValue result = (NSValue)args.Notification.UserInfo.ObjectForKey(new NSString(UIKeyboard.FrameEndUserInfoKey));
            CGSize keyboardSize = result.RectangleFValue.Size;
            if (Element != null)
            {
				var element = Element as GiveCommentField;
                if (element != null)
                {
                    if (element.KeyboardRaised.CanExecute(new KeyboardRaisedEventArgs(keyboardSize.Height + 49)))
                    {
                        element.KeyboardRaised.Execute(new KeyboardRaisedEventArgs(- keyboardSize.Height + 49));
                    }
                }
                //Element.Margin = new Thickness(0); //set the margins to zero when keyboard is dismissed
            }

        }


        void UnregisterForKeyboardNotifications()
        {
            if (_keyboardShowObserver != null)
            {
                _keyboardShowObserver.Dispose();
                _keyboardShowObserver = null;
            }

            if (_keyboardHideObserver != null)
            {
                _keyboardHideObserver.Dispose();
                _keyboardHideObserver = null;
            }
        }
    }
}