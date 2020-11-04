using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CM.ChampagneApp.Instrumentation;
using FreshMvvm;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Helpers.FreshMvvmHelpers
{
	public class InstrumentedNavigationContainer : FreshNavigationContainer
    {
        private IEventCollector _eventCollector;

        public InstrumentedNavigationContainer(Xamarin.Forms.Page page, IEventCollector eventCollector) : base(page)
        {
            _eventCollector = eventCollector;
        }

        public InstrumentedNavigationContainer(Xamarin.Forms.Page page, string navigationPageName, IEventCollector eventCollector) : base(page, navigationPageName)
        {
            _eventCollector = eventCollector;
        }

        public override Task PushPage(Page page, FreshBasePageModel model, bool modal = false, bool animate = true)
        {
            return base.PushPage(page, model, modal, animate);
        }

        public override Task PopPage(bool modal = false, bool animate = true)
        {
            return base.PopPage(modal, animate);
        }
    }
}
