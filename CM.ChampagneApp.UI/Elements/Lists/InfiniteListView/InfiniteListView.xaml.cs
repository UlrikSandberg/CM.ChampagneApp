using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using CM.ChampagneApp.UI.Elements.Helpers.ElementEnum;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.UIFacade.Models.UIUser;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists.InfiniteListView
{
    public partial class InfiniteListView : ContentView
    {
        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IList), typeof(InfiniteListView), propertyChanged: ItemSourceChanged);

        public static BindableProperty IsBackgroundVisibleProperty =
            BindableProperty.Create(nameof(IsBackgroundVisible), typeof(bool), typeof(InfiniteListView), true);

        public static BindableProperty RequestNextPageCommandProperty =
            BindableProperty.Create(nameof(RequestNextPageCommand), typeof(ICommand), typeof(InfiniteListView));

        public static BindableProperty InitLoadingIndicatorIsVisibleProperty =
            BindableProperty.Create(nameof(InitLoadingIndicatorIsVisible), typeof(bool), typeof(InfiniteListView), false);

        public static BindableProperty ActivityStatusProperty =
            BindableProperty.Create(nameof(ActivityStatus), typeof(InfiniteListViewActivityStatus), typeof(InfiniteListView), InfiniteListViewActivityStatus.Inactive, propertyChanged: ActivityStatusChanged);

        public static BindableProperty OutOfServiceTextProperty =
            BindableProperty.Create(nameof(OutOfServiceText), typeof(string), typeof(InfiniteListView), "Out of service... Try reconnecting");

        public static BindableProperty IsPullToRefreshEnabledProperty =
            BindableProperty.Create(nameof(IsPullToRefreshEnabled), typeof(bool), typeof(InfiniteListView));

        public static BindableProperty IsRefreshingProperty =
            BindableProperty.Create(nameof(IsRefreshing), typeof(bool), typeof(InfiniteListView));

        public static BindableProperty IsRefreshingActivityStatusProperty =
            BindableProperty.Create(nameof(IsRefreshingActivityStatus), typeof(InfiniteListViewActivityStatus), typeof(InfiniteListView), InfiniteListViewActivityStatus.Inactive, propertyChanged: IsRefreshingActivityStatusChanged);

        public static BindableProperty RefreshCommandProperty =
            BindableProperty.Create(nameof(RefreshCommand), typeof(ICommand), typeof(InfiniteListView));

        public static BindableProperty UsePullToRefreshAnimationLoaderAsDefaultProperty =
            BindableProperty.Create(nameof(UsePullToRefreshAnimationLoaderAsDefault), typeof(bool), typeof(InfiniteListView));

        public static BindableProperty UseInitAnimationLoaderAsDefaultProperty =
            BindableProperty.Create(nameof(UseInitAnimationLoaderAsDefault), typeof(bool), typeof(InfiniteListView));

        public static BindableProperty UseFooterAnimationLoaderAsDefaultProperty =
            BindableProperty.Create(nameof(UseFooterAnimationLoaderAsDefault), typeof(bool), typeof(InfiniteListView));

        public static BindableProperty ReconnectCommandProperty =
            BindableProperty.Create(nameof(ReconnectCommand), typeof(ICommand), typeof(InfiniteListView));

        public static BindableProperty UseDefaultPreContentViewProperty =
            BindableProperty.Create(nameof(UseDefaultPreContentView), typeof(bool), typeof(InfiniteListView), false);

        public static BindableProperty DefaultPreContentTextProperty =
            BindableProperty.Create(nameof(DefaultPreContentText), typeof(string), typeof(InfiniteListView), "Start Typing to Search...");

        public static BindableProperty UseCustomPreContentViewProperty =
            BindableProperty.Create(nameof(UseCustomPreContentView), typeof(bool), typeof(InfiniteListView));

        public static BindableProperty AddCustomPreContentProperty =
            BindableProperty.Create(nameof(AddCustomPreContent), typeof(View), typeof(InfiniteListView), propertyChanged: AddCustomPreContentChanged);

        public static BindableProperty SeparatorVisibilityProperty =
            BindableProperty.Create(nameof(SeparatorVisibility), typeof(SeparatorVisibility), typeof(InfiniteListView), SeparatorVisibility.None);

        public static BindableProperty SeparatorColorProperty =
            BindableProperty.Create(nameof(SeparatorColor), typeof(Color), typeof(InfiniteListView), Color.Transparent);

        public static BindableProperty ScrollToEntryProperty =
            BindableProperty.Create(nameof(ScrollToEntry), typeof(object), typeof(InfiniteListView));

        public static BindableProperty ShouldScrollToEntryProperty =
            BindableProperty.Create(nameof(ShouldScrollToEntry), typeof(bool), typeof(InfiniteListView), propertyChanged: ShouldScrollToEntryChanged);

        public static BindableProperty ClickedCommandProperty =
            BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(InfiniteListView));

        public static BindableProperty UseCustomEmptyStateContentProperty =
            BindableProperty.Create(nameof(UseCustomEmptyStateContent), typeof(bool), typeof(InfiniteListView));

        public static BindableProperty AddCustomEmptyStateContentProperty =
            BindableProperty.Create(nameof(AddCustomEmptyStateContent), typeof(View), typeof(InfiniteListView), propertyChanged: AddCustomEmptyStateContentChanged);

        public static BindableProperty CustomHeaderProperty =
            BindableProperty.Create(nameof(CustomHeader), typeof(View), typeof(InfiniteListView), propertyChanged: AddCustomHeader);

        public delegate void ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e);
        public event ItemTapped OnItemTapped;

        public delegate void RequestNextPage(object sender, System.EventArgs e);
        public event RequestNextPage OnRequestNextPage;

        public delegate void ItemDissappeared(object sender, ItemVisibilityEventArgs e);
        public event ItemDissappeared OnItemDissappeared;

        public delegate void ItemAppeared(object sender, ItemVisibilityEventArgs e);
        public event ItemAppeared OnItemAppeared;

        public delegate void CurrentRenderedObjects(object sender, PherificalObjects visibleObjects);
        public event CurrentRenderedObjects OnCurrentRenderedObjectChanged;

        public delegate void Reconnect(object sender, System.EventArgs e);
        public event Reconnect OnReconnect;

        public PherificalObjects pherificalObjects = new PherificalObjects();

        private StackLayout headerContent;
        private bool HasBeenRefreshed = false;

        public InfiniteListView()
        {
            InitializeComponent();

            HideLoadingIndicators();

            if (AddCustomPreContent != null)
            {
                InitLoadingAnimation.TranslationY += -100;
            }

            FooterLoadingAnimation.TranslationY += -100;

            headerContent = HeaderContentView;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            Console.WriteLine("BindingContext changed to: " + this.BindingContext + " for -->");
        }

        public bool IsBackgroundVisible
        {
            get { return (bool)GetValue(IsBackgroundVisibleProperty); }
            set { SetValue(IsBackgroundVisibleProperty, value); }
        }

        public IList ItemSource
        {
            get { return (IList)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public ICommand RequestNextPageCommand
        {
            get { return (ICommand)GetValue(RequestNextPageCommandProperty); }
            set { SetValue(RequestNextPageCommandProperty, value); }
        }

        public bool InitLoadingIndicatorIsVisible
        {
            get { return (bool)GetValue(InitLoadingIndicatorIsVisibleProperty); }
            set { SetValue(InitLoadingIndicatorIsVisibleProperty, value); }
        }

        public InfiniteListViewActivityStatus ActivityStatus
        {
            get { return (InfiniteListViewActivityStatus)GetValue(ActivityStatusProperty); }
            set { SetValue(ActivityStatusProperty, value); }
        }

        public InfiniteListViewActivityStatus IsRefreshingActivityStatus
        {
            get { return (InfiniteListViewActivityStatus)GetValue(IsRefreshingActivityStatusProperty); }
            set { SetValue(IsRefreshingActivityStatusProperty, value); }
        }

        public string OutOfServiceText
        {
            get { return (string)GetValue(OutOfServiceTextProperty); }
            set { SetValue(OutOfServiceTextProperty, value); }
        }

        public bool IsPullToRefreshEnabled
        {
            get { return (bool)GetValue(IsPullToRefreshEnabledProperty); }
            set { SetValue(IsPullToRefreshEnabledProperty, value); }
        }

        public bool IsRefreshing
        {
            get { return (bool)GetValue(IsRefreshingProperty); }
            set { SetValue(IsRefreshingProperty, value); }
        }

        public ICommand RefreshCommand
        {
            get { return (ICommand)GetValue(RefreshCommandProperty); }
            set { SetValue(RefreshCommandProperty, value); }
        }

        public bool UsePullToRefreshAnimationLoaderAsDefault
        {
            get { return (bool)GetValue(UsePullToRefreshAnimationLoaderAsDefaultProperty); }
            set { SetValue(UsePullToRefreshAnimationLoaderAsDefaultProperty, value); }
        }

        public bool UseInitAnimationLoaderAsDefault
        {
            get { return (bool)GetValue(UseInitAnimationLoaderAsDefaultProperty); }
            set { SetValue(UseInitAnimationLoaderAsDefaultProperty, value); }
        }

        public bool UseFooterAnimationLoaderAsDefault
        {
            get { return (bool)GetValue(UseFooterAnimationLoaderAsDefaultProperty); }
            set { SetValue(UseFooterAnimationLoaderAsDefaultProperty, value); }
        }

        public ICommand ReconnectCommand
        {
            get { return (ICommand)GetValue(ReconnectCommandProperty); }
            set { SetValue(ReconnectCommandProperty, value); }
        }

        public bool UseDefaultPreContentView
        {
            get { return (bool)GetValue(UseDefaultPreContentViewProperty); }
            set { SetValue(UseDefaultPreContentViewProperty, value); }
        }

        public bool UseCustomPreContentView
        {
            get { return (bool)GetValue(UseCustomPreContentViewProperty); }
            set { SetValue(UseCustomPreContentViewProperty, value); }
        }

        public View AddCustomPreContent
        {
            get { return (View)GetValue(AddCustomPreContentProperty); }
            set { SetValue(AddCustomPreContentProperty, value); }
        }

        public View AddCustomEmptyStateContent
        {
            get { return (View)GetValue(AddCustomEmptyStateContentProperty); }
            set { SetValue(AddCustomEmptyStateContentProperty, value); }
        }

        public bool UseCustomEmptyStateContent
        {
            get { return (bool)GetValue(UseCustomEmptyStateContentProperty); }
            set { SetValue(UseCustomEmptyStateContentProperty, value); }
        }

        public string DefaultPreContentText
        {
            get { return (string)GetValue(DefaultPreContentTextProperty); }
            set { SetValue(DefaultPreContentTextProperty, value); }
        }

        public SeparatorVisibility SeparatorVisibility
        {
            get { return (SeparatorVisibility)GetValue(SeparatorVisibilityProperty); }
            set { SetValue(SeparatorVisibilityProperty, value); }
        }

        public Color SeparatorColor
        {
            get { return (Color)GetValue(SeparatorColorProperty); }
            set { SetValue(SeparatorColorProperty, value); }
        }

        public object ScrollToEntry
        {
            get { return (object)GetValue(ScrollToEntryProperty); }
            set { SetValue(ScrollToEntryProperty, value); }
        }

        public bool ShouldScrollToEntry
        {
            get { return (bool)GetValue(ShouldScrollToEntryProperty); }
            set { SetValue(ShouldScrollToEntryProperty, value); }
        }

        public ICommand ClickedCommand
        {
            get { return (ICommand)GetValue(ClickedCommandProperty); }
            set { SetValue(ClickedCommandProperty, value); }
        }

        public View CustomHeader
        {
            get { return (View)GetValue(CustomHeaderProperty); }
            set { SetValue(CustomHeaderProperty, value); }
        }

        void Handle_Refreshing(object sender, System.EventArgs e)
        {
            if (RefreshCommand != null)
            {
                if (RefreshCommand.CanExecute(null))
                {
                    RefreshCommand.Execute(null);
                }
            }
        }

        void Handle_OnReconnectClicked(object sender, System.EventArgs e)
        {
            if (OnReconnect != null)
            {
                OnReconnect(sender, e);
            }
            if (ReconnectCommand != null)
            {
                if (ReconnectCommand.CanExecute(null))
                {
                    ReconnectCommand.Execute(null);
                }
            }
        }

        //Forward the dataTemplate to the underlying listView;
        private DataTemplate listItemTemplate;
        public DataTemplate ListItemTemplate
        {
            get
            {
                return listItemTemplate;
            }
            set
            {
                InfinityListView.ListItemTemplate = value;
                listItemTemplate = value;
            }
        }

        private ContentView customFooter;
        public ContentView CustomFooter
        {
            get
            {
                return customFooter;
            }
            set
            {
                CustomFooterContentView.Children.Add(value);
                customFooter = value;
            }
        }

        private static void AddCustomHeader(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as InfiniteListView;

            if(control != null)
            {
                control.Handle_CustomHeaderAdded((View)newValue);
            }
        }

        private static void AddCustomEmptyStateContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = bindable as InfiniteListView;

            if(control != null)
            {
                control.CustomEmptyStateContent.Children.Add((View)newValue);
            }
        }

        private static void AddCustomPreContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfiniteListView)bindable;

            if (control != null)
            {
                control.CustomPreContent.Children.Add((View)newValue);
            }
        }

        private static void ActivityStatusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfiniteListView)bindable;

            if (control != null)
            {
                var oValue = (InfiniteListViewActivityStatus)oldValue;
                var nValue = (InfiniteListViewActivityStatus)newValue;

                control.Handle_ActivityStatusChanged(oValue, nValue);
            }
        }

        private static void IsRefreshingActivityStatusChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfiniteListView)bindable;

            if (control != null)
            {
                var oValue = (InfiniteListViewActivityStatus)oldValue;
                var nValue = (InfiniteListViewActivityStatus)newValue;

                control.Handle_IsRefreshingActivityStatusChanged(oValue, nValue);
            }
        }

        private static void ShouldScrollToEntryChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfiniteListView)bindable;

            if (control != null)
            {
                var oValue = (bool)oldValue;
                var nValue = (bool)newValue;

                if (nValue)
                {
                    if (control.ScrollToEntry != null)
                    {
                        //control.Handle_ScrollToEntry(control.ScrollToEntry);
                    }
                }
            }
        }

        private static void ItemSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfiniteListView)bindable;

            if (control != null)
            {
                var itemSource = (IList)newValue;
                control.InfinityListView.ItemsSource = itemSource;
                control.Handle_ScrollToEntry();
            }
        }

        //If the DataResolver changed this means that the infinityListView inside
        private static void DataResolverChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (InfiniteListView)bindable;

            if (control != null)
            {
                //Subcribe the infinityListView to the itemSource inside the IInfinityListViewDataResolver
                var dataResolver = (IInfinityListViewDataResolver)newValue;
                control.InfinityListView.ItemsSource = dataResolver.GetCustomItemSource();
            }
        }

        void Handle_ItemDisappearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            SetLastItemDisappeared(e.Item);

            if (OnItemDissappeared != null)
            {
                OnItemDissappeared(sender, e);
            }
        }

        void Handle_ItemAppearing(object sender, Xamarin.Forms.ItemVisibilityEventArgs e)
        {
            SetLastItemAppeared(e.Item);
            ResolveLastItemAppeared(e.Item);

            if (OnItemAppeared != null)
            {
                OnItemAppeared(sender, e);
            }
        }

        private void Handle_CustomHeaderAdded(View view)
        {
            DefaultHeader.Children.Add(view);
        }

        private void ResolveLastItemAppeared(object entity)
        {
            if (ItemSource != null)
            {
                //Get index of appeared entity
                var indexOfEntity = ItemSource.IndexOf(entity);
                if (indexOfEntity > 0) //If entity is not a part of the itemSource the result will be -1;
                {
                    if (ItemSource.Count - 1 == indexOfEntity) //The index is equal to the lenght of the itemSource, conlusion of this, is that we have reached the end
                    {
                        //Since we reached the last element, request next page of entities
                        RequestNextPageOfEntities();
                    }
                }
            }
        }

        private void RequestNextPageOfEntities()
        {
            if (RequestNextPageCommand != null)
            {
                if (RequestNextPageCommand.CanExecute(null))
                {
                    RequestNextPageCommand.Execute(null);
                }
            }

            if (OnRequestNextPage != null)
            {
                OnRequestNextPage(this, new System.EventArgs());
            }
        }

        private void Handle_IsRefreshingActivityStatusChanged(InfiniteListViewActivityStatus oldValue, InfiniteListViewActivityStatus newValue)
        {
            switch (newValue)
            {
                case InfiniteListViewActivityStatus.IsRefreshing:
                    Handle_IsRefreshing();
                    break;
                case InfiniteListViewActivityStatus.DownloadingFinished:
                    HideLoadingIndicators();
                    break;
            }
        }

        private void Handle_ActivityStatusChanged(InfiniteListViewActivityStatus oldValue, InfiniteListViewActivityStatus newValue)
        {
            switch (newValue)
            {
                case InfiniteListViewActivityStatus.IsDownloading:
                    Handle_IsDownloading();
                    break;
                case InfiniteListViewActivityStatus.DownloadingFinished:
                    HideLoadingIndicators();
                    break;
                case InfiniteListViewActivityStatus.Inactive:
                    break;
                case InfiniteListViewActivityStatus.IsRefreshing:
                    Handle_IsRefreshing();
                    break;
                case InfiniteListViewActivityStatus.OutOfService:
                    Handle_OutOfService(oldValue);
                    break;
            }
        }

        private void Handle_OutOfService(InfiniteListViewActivityStatus oldValue)
        {
            HideLoadingIndicators();
            ShowOutOfService(oldValue);
        }

        private void ShowOutOfService(InfiniteListViewActivityStatus oldValue)
        {
            CustomEmptyStateContent.IsVisible = false;
            if (ItemSource.Count > 0)
            {
                if(oldValue.Equals(InfiniteListViewActivityStatus.IsRefreshing))
                {
                    ReconnectView.IsOutOfService = true;
                }
                else
                {
                    OutOfServiceLabel.IsVisible = true;
                }
            }
            else
            {
                ReconnectView.IsOutOfService = true;
            }
        }

        private void Handle_IsRefreshing()
        {
            HeaderContentView.IsVisible = true;
            HeaderContentView.HeightRequest = 60;
            HeaderContentView.Padding = new Thickness(0, 0, 0, 0);
            OutOfServiceLabel.IsVisible = false;
            if (UsePullToRefreshAnimationLoaderAsDefault)
            {
                RefreshAnimation.IsVisible = true;
            }
            else
            {
                RefreshActivityIndicator.IsVisible = true;
                RefreshActivityIndicator.IsRunning = true;
            }
        }

        private void Handle_IsDownloading()
        {
            CustomEmptyStateContent.IsVisible = false;
            if(IsRefreshingActivityStatus.Equals(InfiniteListViewActivityStatus.IsRefreshing))
            {
                return;
            }

            //If the itemsource is either null or there are no elements the init loading indicator should be shown
            ReconnectView.IsOutOfService = false;
            if (ItemSource == null)
            {
                ShowInitLoadingIndicator();
                return;
            }
            if (ItemSource.Count < 1)
            {
                ShowInitLoadingIndicator();
                return;
            }

            //Else the footerLoadingIndicator should be visible
            ShowFooterLoadingIndicator();
        }

        private void HideLoadingIndicators()
        {
            InitLoadingIndicator.IsRunning = false;
            InitLoadingIndicator.IsVisible = false;
            FooterActivityIndicator.IsRunning = false;
            FooterActivityIndicator.IsVisible = false;
            FooterLoadingAnimation.IsVisible = false;
            RefreshActivityIndicator.IsVisible = false;
            RefreshActivityIndicator.IsRunning = false;
            HeaderContentView.Padding = new Thickness(0, -35, 0, 0);
            HeaderContentView.HeightRequest = 0;
            //HeaderContentView.IsVisible = false;
            InitLoadingAnimation.IsVisible = false;
            IsRefreshing = false;
            //RefreshAnimation.IsVisible = false;
            OutOfServiceLabel.IsVisible = false;
            ReconnectView.IsOutOfService = false;
            if (ItemSource != null)
            {
                if (UseDefaultPreContentView)
                {
                    if (ItemSource.Count == 0)
                    {
                        DefaultPreContent.IsVisible = true;
                    }
                    else
                    {
                        DefaultPreContent.IsVisible = false;
                    }
                }
                else if(UseCustomEmptyStateContent)
                {
                    if(ItemSource.Count == 0)
                    {
                        CustomEmptyStateContent.IsVisible = true;
                    }
                    else
                    {
                        CustomEmptyStateContent.IsVisible = false;
                    }
                }
            }
        }

        void Handle_SizeChanged(object sender, System.EventArgs e)
        {
            Console.WriteLine(e);
        }

        private void ShowInitLoadingIndicator()
        {
            if (UseInitAnimationLoaderAsDefault)
            {
                InitLoadingAnimation.IsVisible = true;
                //DefaultPreContent.IsVisible = true;
            }
            else
            {
                InitLoadingIndicator.IsRunning = true;
                InitLoadingIndicator.IsVisible = true;
            }
        }

        private void ShowFooterLoadingIndicator()
        {
            if (UseFooterAnimationLoaderAsDefault)
            {
                OutOfServiceLabel.IsVisible = false;
                FooterLoadingAnimation.IsVisible = true;
            }
            else
            {
                OutOfServiceLabel.IsVisible = false;
                FooterActivityIndicator.IsRunning = true;
                FooterActivityIndicator.IsVisible = true;
            }
        }

        private void IsOutOfService()
        {
            OutOfServiceLabel.IsVisible = true;
        }

        void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (OnItemTapped != null)
            {
                OnItemTapped(sender, e);
            }

            if (ClickedCommand != null)
            {
                if (ClickedCommand.CanExecute(null))
                {
                    ClickedCommand.Execute(null);
                }
            }
        }

        private void Handle_ScrollToEntry()
        {
            if (ItemSource != null)
            {
                if (ScrollToEntry != null)
                {
                    if (ShouldScrollToEntry)
                    {
                        Console.WriteLine("Binding context was changed scrolling to: " + ScrollToEntry);
                        InfinityListView.ScrollTo(ScrollToEntry, ScrollToPosition.Start, false);
                    }
                    ShouldScrollToEntry = false;
                }
            }
        }

        private void ScrollToBottom()
        {
            if (ItemSource == null)
            {
                return;
            }

            if (ItemSource.Count > 0)
            {
                var last = ItemSource[ItemSource.Count - 1];

                InfinityListView.ScrollTo(last, ScrollToPosition.End, false);
            }
        }

        private void SetLastItemAppeared(object justAppeared)
        {
            if (pherificalObjects.DidAppear == null)//The bucket have not been filled
            {
                pherificalObjects.DidAppear = justAppeared;
            }
            else
            {
                if (pherificalObjects.DidDissappear == null)
                {
                    pherificalObjects.DidAppear = justAppeared;
                }
                //Since neither of the two are null send pherificalObject in event
                //Create new pherifical object and set didAppear
                if (pherificalObjects.DidAppear != null && pherificalObjects.DidDissappear != null)
                {
                    if (OnCurrentRenderedObjectChanged != null)
                    {
                        //Check scroll direction
                        var indexOfAppear = ItemSource.IndexOf(pherificalObjects.DidAppear);
                        var indexOfDisappear = ItemSource.IndexOf(pherificalObjects.DidDissappear);
                        ScrollDirection scrollDirection = ScrollDirection.Still;
                        if (indexOfAppear > indexOfDisappear)
                        {
                            scrollDirection = ScrollDirection.Down;
                        }
                        else
                        {
                            scrollDirection = ScrollDirection.Up;
                        }

                        OnCurrentRenderedObjectChanged(this, new PherificalObjects
                        {
                            DidAppear = pherificalObjects.DidAppear,
                            DidDissappear = pherificalObjects.DidDissappear,
                            ScrollDirection = scrollDirection
                        });
                    }
                }

                pherificalObjects.Clear();
                pherificalObjects.DidAppear = justAppeared;
            }
        }

        private void SetLastItemDisappeared(object justDisappeared)
        {
            if (pherificalObjects.DidDissappear == null)//The bucket have not been filled
            {
                pherificalObjects.DidDissappear = justDisappeared;
            }
            else
            {
                if (pherificalObjects.DidAppear == null)
                {
                    pherificalObjects.DidDissappear = justDisappeared;
                }
                //Since neither of the two are null send pherificalObject in event
                //Create new pherifical object and set didAppear
                if (pherificalObjects.DidAppear != null && pherificalObjects.DidDissappear != null)
                {
                    if (OnCurrentRenderedObjectChanged != null)
                    {
                        //Check scroll direction
                        var indexOfAppear = ItemSource.IndexOf(pherificalObjects.DidAppear);
                        var indexOfDisappear = ItemSource.IndexOf(pherificalObjects.DidDissappear);
                        ScrollDirection scrollDirection = ScrollDirection.Still;
                        if (indexOfAppear > indexOfDisappear)
                        {
                            scrollDirection = ScrollDirection.Down;
                        }
                        else
                        {
                            scrollDirection = ScrollDirection.Up;
                        }

                        OnCurrentRenderedObjectChanged(this, new PherificalObjects
                        {
                            DidAppear = pherificalObjects.DidAppear,
                            DidDissappear = pherificalObjects.DidDissappear,
                            ScrollDirection = scrollDirection
                        });
                    }
                }

                pherificalObjects.Clear();
                pherificalObjects.DidAppear = justDisappeared;
            }
        }
    }
    public enum InfiniteListViewActivityStatus
    {
        IsDownloading,
        IsRefreshing,
        DownloadingFinished,
        Inactive,
        OutOfService
    }

    public class PherificalObjects
    {
        public ScrollDirection ScrollDirection { get; set; }

        public object DidDissappear { get; set; }
        public object DidAppear { get; set; }

        public void Clear()
        {
            DidAppear = null;
            DidDissappear = null;
        }
    }
}
