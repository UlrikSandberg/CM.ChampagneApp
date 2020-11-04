using System;
namespace CM.ChampagneApp.UI.Dependency
{
    public interface ITabbarBadgeService
    {
		void SetBadge(int badgeValue);
		void IncrementBadgeValue();
		void IncrementBadgeValue(int incrementBy);
		void ClearBadge();
		int CurrentBadgeValue();
    }
}
