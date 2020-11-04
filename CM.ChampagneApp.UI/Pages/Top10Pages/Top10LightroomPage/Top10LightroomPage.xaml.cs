using System.Threading.Tasks;
using CarouselView.FormsPlugin.Abstractions;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.Top10Pages.Top10LightroomPage
{
    public partial class Top10LightroomPage : BaseContentPage
    {
        private Top10LightroomPageModel _viewModel;

        public Top10LightroomPage()
        {
            InitializeComponent();
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            _viewModel = (Top10LightroomPageModel)this.BindingContext;
        }

        void Handle_Scrolled(object sender, CarouselView.FormsPlugin.Abstractions.ScrolledEventArgs e)
        {
            if(e.Direction == ScrollDirection.Right)
            {
                if(_viewModel.ScrollRight.CanExecute(e.NewValue))
                    _viewModel.ScrollRight.Execute(e.NewValue);
            }
            else if(e.Direction == ScrollDirection.Left)
            {
                if(_viewModel.ScrollLeft.CanExecute(e.NewValue))
                    _viewModel.ScrollLeft.Execute(e.NewValue);
            }
        }

        void Handle_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            if(_viewModel.PositionSelected.CanExecute(e.NewValue))
                _viewModel.PositionSelected.Execute(e.NewValue);
        }

        void Handle_OnSegmentSelected(object sender, Xamarin.Forms.ValueChangedEventArgs e)
        {
            if (_viewModel.SegmentIndexChanged.CanExecute(e.NewValue))
                _viewModel.SegmentIndexChanged.Execute(e.NewValue);
        }


        void Handle_OnRightIconClicked(object sender, System.EventArgs e)
        {
            AnimatePopUp();
        }

        void Handle_PopUpClicked(object sender, System.EventArgs e)
        {
            AnimatePopUp();
        }

        private async Task AnimatePopUp()
        {
            if(PopUp.IsVisible) //Fade away
            {
                await PopUp.FadeTo(0, 200);
                PopUp.IsVisible = false;
            }
            else //Animate
            {
                PopUp.IsVisible = true;
                await PopUp.FadeTo(1, 200);
            }
        }
    }
}
