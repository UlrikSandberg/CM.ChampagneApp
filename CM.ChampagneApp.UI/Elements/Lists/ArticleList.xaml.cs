using System;
using System.Collections;
using System.Collections.Generic;
using CM.ChampagneApp.UI.Elements.Cards;
using Xamarin.Forms;
using CM.ChampagneApp.UI.CustomEventArgs;

namespace CM.ChampagneApp.UI.Elements.Lists
{
    public partial class ArticleList : ContentView
    {

        public delegate void ItemClicked(object sender, LinkPageEventArgs  args);
        public event ItemClicked OnItemClicked;


        public ArticleList()
        {
            InitializeComponent();
        }

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }


        public static BindableProperty ItemSourceProperty =
            BindableProperty.Create(nameof(ItemSource), typeof(IEnumerable), typeof(ArticleList), propertyChanged: ItemSourceChanged);


        private static void ItemSourceChanged(BindableObject bindable, object newValue, object oldValue)
        {
            var articleList = (ArticleList)bindable;

            if(articleList != null)
            {
                articleList.UpdateList(); 
            }
        }

        private void UpdateList()
        {
            if(ItemSource != null)
            {
                foreach(Object ArticleOverView in ItemSource)
                {

                    var ArticleInfoCard = new ArticleInfoCard();
                    ArticleInfoCard.BindingContext = ArticleOverView;
                    ArticleInfoCard.OnChosenPageLink += new ArticleInfoCard.ChosenPageLink(OnChosenPageLink);
                    ContentPresenter.Children.Add(ArticleInfoCard);
                    
                }
            }
        }

        private void OnChosenPageLink(object sender, LinkPageEventArgs args)
        {
            OnItemClicked(sender, args);
        }

    }
}
