using System;
using CarouselView.FormsPlugin.Abstractions;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Elements.Lists.InfiniteListView
{
    public class GlobalSearchCarousel : CarouselViewControl
    {
        public GlobalSearchCarousel()
        {
            ItemTemplate = new InfinityListViewTemplateSelector(GetItemTemplate);
        }

        public DataTemplate ChampagneListViewTemplate { get; set; }
        public DataTemplate BrandListViewTemplate { get; set; }
        public DataTemplate UserListViewTemplate { get; set; }

        private DataTemplate GetItemTemplate(object item, BindableObject bindableObject)
        {
            var entry = (GlobalSearchCarouselModel)item;

            switch(entry.DataTemplate)
            {
                case GlobalSearchCarouselTemplate.ChampagneListTemplate:
                    return ChampagneListViewTemplate;
                case GlobalSearchCarouselTemplate.BrandListTemplate:
                    return BrandListViewTemplate;
                case GlobalSearchCarouselTemplate.UserListTemplate:
                    return UserListViewTemplate;

                default:
                    return new DataTemplate(() =>
                    {
                        return new StackLayout();
                    });
            }
        }
    }
}
