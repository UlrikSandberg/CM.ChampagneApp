using System;
namespace CM.ChampagneApp.UI.Pages.Commons.ActiveCallTracker
{
    public class ActiveCall
    {
        public string ContextName { get; set; }
        public Guid CallId { get; }
        public DateTime InvokedAt { get; }

        public ActiveCall(Guid callId, string contextName)
        {
            ContextName = contextName;
            CallId = callId;
            InvokedAt = DateTime.UtcNow;
        }
    }
}
