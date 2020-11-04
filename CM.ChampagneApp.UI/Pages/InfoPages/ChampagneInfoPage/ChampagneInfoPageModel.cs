using System;
using System.Windows.Input;
using FreshMvvm;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.InfoPages.ChampagneInfoPage
{
    public class ChampagneInfoPageModel : FreshBasePageModel
    {
        public ICommand NavigateBack { get; set; }

        public ChampagneInfoPageModel()
        {
            NavigateBack = new Command(OnNavigateBack);
        }

        private void OnNavigateBack()
        {
            CoreMethods.PopPageModel();
        }
    }
}
