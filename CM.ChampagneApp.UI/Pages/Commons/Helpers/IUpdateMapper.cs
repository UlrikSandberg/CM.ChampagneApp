using System;
namespace CM.ChampagneApp.UI.Pages.Commons.Helpers
{
    public interface IUpdateMapper<TData, TUpdate>
    {
        void UpdateData(TData data, TUpdate update);
    }
}
