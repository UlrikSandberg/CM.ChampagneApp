using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages
{
    public partial class BaseContentPage : ContentPage
    {
        /* Pseudo thoughts
         * 
         * With this new base class, we could start implementing a better messaging system in the app when erros occur.
         * 
         * Instead of outOfService the basePageModel could featuer a ResolveErrorMethod which took a an enum generated when asking for data
         * Depending on the error type, we could feature different error screens in the App.
         * 
         * Fx, we could keep some kind of globalAppState, and each time a pageModel view appears, depending on a authorized attribute, it could ask
         * the globalAppState if the currentUser is authorized/log in, if no the basePageModel would redirect the user to the login screen, or prompt login.        
         * 
         * If the basePageModel resolves an error, to be bad connection, server timeout, show a error screen appropriate to the content.
         * 
         * Likewise, popup could be customized as well as navigation bars, loaders and more...
         * 
         */

        private BasePageModel _viewModel;

        public static BindableProperty HasFloatingNavigationBarProperty =
            BindableProperty.Create(nameof(HasFloatingNavigationBar), typeof(bool), typeof(BaseContentPage), false);

        public static BindableProperty HasNavigationBarProperty =
            BindableProperty.Create(nameof(HasNavigationBar), typeof(bool), typeof(BaseContentPage));

        public static BindableProperty NavigationBackgroundColorProperty =
            BindableProperty.Create(nameof(NavigationBackgroudColor), typeof(Color), typeof(BaseContentPage));

        public static BindableProperty NavigationTitleProperty =
            BindableProperty.Create(nameof(NavigationTitle), typeof(string), typeof(BaseContentPage));

        public static BindableProperty NavigationSubTitleProperty =
            BindableProperty.Create(nameof(NavigationSubTitle), typeof(string), typeof(BaseContentPage));

        public static BindableProperty HasCustomLeftNavigationIconProperty =
            BindableProperty.Create(nameof(HasCustomLeftNavigationIcon), typeof(bool), typeof(BaseContentPage), false);

        public static BindableProperty LeftNavigationIconProperty =
            BindableProperty.Create(nameof(LeftNavigationIcon), typeof(string), typeof(BaseContentPage));

        public static BindableProperty RightNavigationIconProperty =
            BindableProperty.Create(nameof(RightNavigationIcon), typeof(string), typeof(BaseContentPage));

        public static BindableProperty IsGrapeBackgroundVisibleProperty =
            BindableProperty.Create(nameof(IsGrapeBackgroundVisible), typeof(bool), typeof(BaseContentPage), false);

        public static BindableProperty IsContentLoadingAnimationEnabledProperty =
            BindableProperty.Create(nameof(IsContentLoadingAnimationEnabled), typeof(bool), typeof(BaseContentPage), false);

        public static BindableProperty IsContentLoadingAnimationVisibleProperty =
            BindableProperty.Create(nameof(IsContentLoadingAnimationVisible), typeof(bool), typeof(BaseContentPage), false);

        public static BindableProperty IsNavigationbarScrollResponsiveProperty =
            BindableProperty.Create(nameof(IsNavigationbarScrollResponsive), typeof(bool), typeof(BaseContentPage));

        public static BindableProperty ScrolledNavigationTitleProperty =
            BindableProperty.Create(nameof(ScrolledNavigationTitle), typeof(string), typeof(BaseContentPage));

        public static BindableProperty ScrolledNavigationSubtitleProperty =
            BindableProperty.Create(nameof(ScrolledNavigationSubtitle), typeof(string), typeof(BaseContentPage));

        public static BindableProperty ScrolledNavigationBackgroundColorProperty =
            BindableProperty.Create(nameof(ScrolledNavigationBackgroundColor), typeof(Color), typeof(BaseContentPage));

        public static BindableProperty RightNavigationIconCommandProperty =
            BindableProperty.Create(nameof(RightNavigationIconCommand), typeof(ICommand), typeof(BaseContentPage));

        public static BindableProperty LeftNavigationIconCommandProperty =
            BindableProperty.Create(nameof(LeftNavigationIconCommand), typeof(ICommand), typeof(BaseContentPage));

        public static BindableProperty IsOutOfServiceProperty =
            BindableProperty.Create(nameof(IsOutOfService), typeof(bool), typeof(BaseContentPage), false);

        public static BindableProperty ReconnectCommandProperty =
            BindableProperty.Create(nameof(ReconnectCommand), typeof(ICommand), typeof(BaseContentPage));

        public static BindableProperty HasBackButtonProperty =
            BindableProperty.Create(nameof(HasBackButton), typeof(bool), typeof(BaseContentPage), false, propertyChanged: HasBackButtonChanged);

        public static BindableProperty TitleClickedCommandProperty =
            BindableProperty.Create(nameof(TitleClickedCommand), typeof(ICommand), typeof(BaseContentPage));

        public delegate void RightIConClicked(object sender, System.EventArgs e);
        public event RightIConClicked OnRightIconClicked;

        public BaseContentPage()
        {
            InitializeComponent();

            NavigationPage.SetHasNavigationBar(this, false);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as BasePageModel;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CM.ChampagneApp.UI.Pages.BaseContentPage"/> is
        /// content loading animation enabled.
        /// </summary>
        /// <value><c>true</c> if is content loading animation enabled; otherwise, <c>false</c>.</value>
        public bool IsContentLoadingAnimationEnabled
        {
            get { return (bool)GetValue(IsContentLoadingAnimationEnabledProperty); }
            set { SetValue(IsContentLoadingAnimationEnabledProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CM.ChampagneApp.UI.Pages.BaseContentPage"/> is
        /// content loading animation visible.
        /// </summary>
        /// <value><c>true</c> if is content loading animation visible; otherwise, <c>false</c>.</value>
        public bool IsContentLoadingAnimationVisible
        {
            get
            {
                return IsContentLoadingAnimationEnabled ? (bool)GetValue(IsContentLoadingAnimationVisibleProperty) : false;
            }
            set { SetValue(IsContentLoadingAnimationVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CM.ChampagneApp.UI.Pages.BaseContentPage"/> has a
        /// visible navigationbar.
        /// </summary>
        /// <value><c>true</c> if has navigation bar; otherwise, <c>false</c>.</value>
        public bool HasNavigationBar
        {
            get { return (bool)GetValue(HasNavigationBarProperty); }
            set { SetValue(HasNavigationBarProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CM.ChampagneApp.UI.Pages.BaseContentPage"/> has
        /// a floating navigation bar. If true this contentPage will feature a navigation bar that will float above 
        /// content. If false the navigation bar will take up space and content will be below the navigation bar
        /// </summary>
        /// <value><c>true</c> if has floating navigation bar; otherwise, <c>false</c>.</value>
        public bool HasFloatingNavigationBar
        {
            get
            {
                return HasNavigationBar ? (bool)GetValue(HasFloatingNavigationBarProperty) : false;
            }
            set { SetValue(HasFloatingNavigationBarProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the navigation backgroud.
        /// </summary>
        /// <value>The color of the navigation backgroud.</value>
        public Color NavigationBackgroudColor
        {
            get { return (Color)GetValue(NavigationBackgroundColorProperty); }
            set { SetValue(NavigationBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets the navigation title.
        /// </summary>
        /// <value>The navigation title.</value>
        public string NavigationTitle
        {
            get { return (string)GetValue(NavigationTitleProperty); }
            set { SetValue(NavigationTitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the navigation sub title.
        /// </summary>
        /// <value>The navigation sub title.</value>
        public string NavigationSubTitle
        {
            get { return (string)GetValue(NavigationSubTitleProperty); }
            set { SetValue(NavigationSubTitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the scrolled navigation title.
        /// </summary>
        /// <value>The scrolled navigation title.</value>
        public string ScrolledNavigationTitle
        {
            get { return (string)GetValue(ScrolledNavigationTitleProperty); }
            set { SetValue(ScrolledNavigationTitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the scrolled navigation sub title.
        /// </summary>
        /// <value>The scrolled navigation sub title.</value>
        public string ScrolledNavigationSubtitle
        {
            get { return (string)GetValue(ScrolledNavigationSubtitleProperty); }
            set { SetValue(ScrolledNavigationSubtitleProperty, value); }
        }

        /// <summary>
        /// Gets or sets the color of the scrolled navigation background.
        /// </summary>
        /// <value>The color of the scrolled navigation background.</value>
        public Color ScrolledNavigationBackgroundColor
        {
            get { return (Color)GetValue(ScrolledNavigationBackgroundColorProperty); }
            set { SetValue(ScrolledNavigationBackgroundColorProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CM.ChampagneApp.UI.Pages.BaseContentPage"/> has
        /// custom left navigation icon.
        /// </summary>
        /// <value><c>true</c> if has custom left navigation icon; otherwise, <c>false</c>.</value>
        public bool HasCustomLeftNavigationIcon
        {
            get { return (bool)GetValue(HasCustomLeftNavigationIconProperty); }
            set { SetValue(HasCustomLeftNavigationIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the left navigation icon.
        /// </summary>
        /// <value>The left navigation icon.</value>
        public string LeftNavigationIcon
        {
            get { return (string)GetValue(LeftNavigationIconProperty); }
            set { SetValue(LeftNavigationIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets the right navigation icon.
        /// </summary>
        /// <value>The right navigation icon.</value>
        public string RightNavigationIcon
        {
            get { return (string)GetValue(RightNavigationIconProperty); }
            set { SetValue(RightNavigationIconProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CM.ChampagneApp.UI.Pages.BaseContentPage"/> is
        /// grape background visible.
        /// </summary>
        /// <value><c>true</c> if is grape background visible; otherwise, <c>false</c>.</value>
        public bool IsGrapeBackgroundVisible
        {
            get { return (bool)GetValue(IsGrapeBackgroundVisibleProperty); }
            set { SetValue(IsGrapeBackgroundVisibleProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this the navigationbar is scroll responsive.
        /// </summary>
        /// <value><c>true</c> if is navigationbar scroll responsive; otherwise, <c>false</c>.</value>
        public bool IsNavigationbarScrollResponsive
        {
            get { return (bool)GetValue(IsNavigationbarScrollResponsiveProperty); }
            set { SetValue(IsNavigationbarScrollResponsiveProperty, value); }
        }

        /// <summary>
        /// Gets or sets the right navigation icon command.
        /// </summary>
        /// <value>The right navigation icon command.</value>
        public ICommand RightNavigationIconCommand
        {
            get { return (ICommand)GetValue(RightNavigationIconCommandProperty); }
            set { SetValue(RightNavigationIconCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets the left navigation icon command.
        /// </summary>
        /// <value>The left navigation icon command.</value>
        public ICommand LeftNavigationIconCommand
        {
            get { return (ICommand)GetValue(LeftNavigationIconCommandProperty); }
            set { SetValue(LeftNavigationIconCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CM.ChampagneApp.UI.Pages.BaseContentPage"/> is out
        /// of service.
        /// </summary>
        /// <value><c>true</c> if is out of service; otherwise, <c>false</c>.</value>
        public bool IsOutOfService
        {
            get { return (bool)GetValue(IsOutOfServiceProperty); }
            set { SetValue(IsOutOfServiceProperty, value); }
        }

        /// <summary>
        /// Gets or sets the reconnect command.
        /// </summary>
        /// <value>The reconnect command.</value>
        public ICommand ReconnectCommand
        {
            get { return (ICommand)GetValue(ReconnectCommandProperty); }
            set { SetValue(ReconnectCommandProperty, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:CM.ChampagneApp.UI.Pages.BaseContentPage"/> has
        /// back button.
        /// </summary>
        /// <value><c>true</c> if has back button; otherwise, <c>false</c>.</value>
        public bool HasBackButton
        {
            get { return (bool)GetValue(HasBackButtonProperty); }
            set { SetValue(HasBackButtonProperty, value); }
        }

        public ICommand TitleClickedCommand
        {
            get { return (ICommand)GetValue(TitleClickedCommandProperty); }
            set { SetValue(TitleClickedCommandProperty, value); }
        }

        /// <summary>
        /// Handles the scrolled.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        public void Handle_Scrolled(object sender, Xamarin.Forms.ScrolledEventArgs e)
        {
            if (!IsNavigationbarScrollResponsive)
                return;

            if (e.ScrollY > (App.DisplaySettings.Width * 0.6) - (App.DisplaySettings.Width / 6) - 20)
            {
                //Set Title and sub title during scroll
                FloatingNavigationbar.Title = string.IsNullOrEmpty(ScrolledNavigationTitle) ? NavigationTitle : ScrolledNavigationTitle;
                FloatingNavigationbar.SubTitle = string.IsNullOrEmpty(ScrolledNavigationSubtitle) ? NavigationSubTitle : ScrolledNavigationSubtitle;

                //Set background color
                FloatingNavigationbar.BackgroundColor = ScrolledNavigationBackgroundColor;
            }
            else
            {
                FloatingNavigationbar.Title = NavigationTitle;
                FloatingNavigationbar.SubTitle = NavigationSubTitle;

                FloatingNavigationbar.BackgroundColor = NavigationBackgroudColor;
            }
        }

        private static void HasBackButtonChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as BaseContentPage;

            if(control != null)
            {
                control.LeftNavigationIcon = "BackArrowIcon";
            }
        }

        void Handle_OnNavTitleClicked(object sender, System.EventArgs e)
        {
            if(TitleClickedCommand != null)
            {
                if (TitleClickedCommand.CanExecute(null))
                    TitleClickedCommand.Execute(null);
            }
        }

        void Handle_OnRightNavIconClicked(object sender, System.EventArgs e)
        {
            OnRightIconClicked?.Invoke(sender, e);
        }

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            //If has left navigationCommand
            if(HasCustomLeftNavigationIcon)
            {
                if(LeftNavigationIconCommand != null)
                {
                    if(LeftNavigationIconCommand.CanExecute(null))
                    {
                        LeftNavigationIconCommand.Execute(null);
                    }
                }
            }
            else
            {
                if(!string.IsNullOrEmpty(LeftNavigationIcon))
                {
                    if (LeftNavigationIcon.Equals("BackArrowIcon"))
                    {
                        if (_viewModel != null)
                        {
                            if (_viewModel.NavigateBack.CanExecute(null))
                            {
                                _viewModel.NavigateBack.Execute(null);
                            }
                        }
                    }
                }
            }
        }

        public View PageContent
        {
            set
            {
                MainContent.Children.Add((View)value);
            }
        }
    }
}
