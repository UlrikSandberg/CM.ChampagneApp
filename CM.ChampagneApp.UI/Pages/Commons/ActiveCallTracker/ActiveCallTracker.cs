using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

[assembly: Dependency(typeof(CM.ChampagneApp.UI.Pages.Commons.ActiveCallTracker.ActiveCallTracker))]
namespace CM.ChampagneApp.UI.Pages.Commons.ActiveCallTracker
{
    public class ActiveCallTracker : IActiveCallTracker
    {
        private List<ActiveCall> activeCalls = new List<ActiveCall>();

        public void AddCall(string contextName, Guid callId)
        {
            activeCalls.Add(new ActiveCall(callId, contextName));
        }

        public void ClearAllContextCalls(string contextName)
        {
            activeCalls.RemoveAll(a => a.ContextName.Equals(contextName));
        }

        public void DisposeActiveCall(string contextName, Guid callId)
        {
            var callToBeDispose = activeCalls.Where(c => c.ContextName.Equals(contextName) && c.CallId.Equals(callId));

            if(callToBeDispose.Any())
            {
                activeCalls.Remove(callToBeDispose.SingleOrDefault());
            }
        }

        public bool IsMostRecentCall(string contextName, Guid callId)
        {
            var contextCalls = activeCalls.Where(c => c.ContextName.Equals(contextName));

            //There are not calls within this context return false.
            if(!contextCalls.Any())
            {
                return false;
            }

            var callToCheck = contextCalls.Where(c => c.CallId.Equals(callId));

            //The respective calls callId doesn't exists return false
            if (!callToCheck.Any())
            {
                return false;
            }

            //The respective call is present if it is the only return true
            //if not see if it is the most recent or not
            foreach(var call in contextCalls)
            {
                //This means that there was a call which was invoked at a later stage
                //return false
                if(call.InvokedAt.CompareTo(callToCheck.SingleOrDefault().InvokedAt) > 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
