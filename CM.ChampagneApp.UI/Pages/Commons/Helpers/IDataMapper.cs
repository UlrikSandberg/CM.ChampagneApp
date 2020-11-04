using System;
namespace CM.ChampagneApp.UI.Pages.Commons.Helpers
{
    public interface IDataMapper<TData, TSource>
    {
        TData Map(TSource data);
    }
}
