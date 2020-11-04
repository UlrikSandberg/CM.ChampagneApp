using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists
{
	public class ListviewWithScrollEvent : ListView
    {

		public delegate void DidScrollEvent(object sender, System.EventArgs e);
		public event DidScrollEvent OnDidScrollEvent;

		public ListviewWithScrollEvent(ListViewCachingStrategy cachingStrategy) : base(cachingStrategy)
		{
			DidScroll = new Command(OnDidScroll);
		}

		public ICommand DidScroll { get; set; }     

        private void OnDidScroll()
		{
			if(OnDidScrollEvent != null)
			{
				OnDidScrollEvent(this, new System.EventArgs());
			}
		}
    }
}
