using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists.InfiniteListView
{
    public class InfinityListViewTemplateSelector : DataTemplateSelector
    {
        private readonly Func<object, BindableObject, DataTemplate> _getDataTemplate;

        public InfinityListViewTemplateSelector(Func<object, BindableObject, DataTemplate> getDataTemplate)
        {
            _getDataTemplate = getDataTemplate;
        }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return _getDataTemplate(item, container);
        }
    }
}
