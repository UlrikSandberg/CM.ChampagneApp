using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.Top10Pages.Top10ListPage
{
    public partial class Top10ListPage : BaseContentPage
    {
        private Top10ListPageModel _viewModel;

        public Top10ListPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            _viewModel = this.BindingContext as Top10ListPageModel;
        }

        void Handle_OnSegmentSelected(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if (_viewModel.SegmentSelected.CanExecute(e.NewValue))
                _viewModel.SegmentSelected.Execute(e.NewValue);
        }

        void Handle_OnItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            if (_viewModel.ItemClicked.CanExecute(e.Item))
                _viewModel.ItemClicked.Execute(e.Item);
        }
    }
}
