using System;
using System.Collections.Generic;
using CM.ChampagneApp.DTO.Models;
using CM.ChampagneApp.UI.CustomEventArgs;
using CM.ChampagneApp.UI.UIFacade.Models;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Cards
{
    public partial class ArticleInfoCard : ContentView
    {

        public delegate void ChosenPageLink(object sender, LinkPageEventArgs args);
        public event ChosenPageLink OnChosenPageLink;

        public ArticleInfoCard()
        {
            InitializeComponent();
        }




        void Handle_Clicked(object sender, System.EventArgs e)
        {
            var articleOverview = (UIArticleOverview)this.BindingContext;
            LinkPageEventArgs args = new LinkPageEventArgs(articleOverview.ArticleURLPath);
            System.Diagnostics.Debug.WriteLine("Clicked");
            if (OnChosenPageLink != null)
                OnChosenPageLink(this, args);
        }
    }
}
