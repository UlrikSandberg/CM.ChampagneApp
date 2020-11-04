using System;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
namespace CM.ChampagneApp.UI.Dependency
{
    public interface IMicroInterActionsQue
    {
		bool IsInteractionInProgress(Guid contextId);

		bool EndInteraction(Guid contextId);

		bool StartInteraction(Guid contextId);

		void QueInteraction(Guid contextId, bool interactionState);

		QueState IsInteractionInQue(Guid contextId);

		void ClearInteractionManagerFromId(Guid contextId);
    }   
}
