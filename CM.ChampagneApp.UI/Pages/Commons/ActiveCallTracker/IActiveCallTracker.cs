using System;
namespace CM.ChampagneApp.UI.Pages.Commons.ActiveCallTracker
{
    public interface IActiveCallTracker
    {
        void AddCall(string contextName, Guid callId);
        bool IsMostRecentCall(string contextName, Guid callId);
        void DisposeActiveCall(string contextName, Guid callid);
        void ClearAllContextCalls(string contextName);
    }
}
