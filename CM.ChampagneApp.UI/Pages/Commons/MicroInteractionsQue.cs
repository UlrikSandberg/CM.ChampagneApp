using System;
using Xamarin.Forms;
using CM.ChampagneApp.UI.Dependency;
using System.Collections.Generic;

[assembly: Dependency(typeof(CM.ChampagneApp.UI.FreshMvvmHelpers.MicroInteractionsQue))]
namespace CM.ChampagneApp.UI.FreshMvvmHelpers
{
	public class MicroInteractionsQue : IMicroInterActionsQue
	{
		private HashSet<Guid> microInteractionsInProgress;
		private Dictionary<Guid, bool> microInteractionsInQue;

		public MicroInteractionsQue()
		{
			microInteractionsInProgress = new HashSet<Guid>();
			microInteractionsInQue = new Dictionary<Guid, bool>();
		}

		public bool IsInteractionInProgress(Guid contextId)
		{
			return microInteractionsInProgress.Contains(contextId);
		}

		public bool EndInteraction(Guid contextId)
		{
			return microInteractionsInProgress.Remove(contextId);
		}

		public bool StartInteraction(Guid contextId)
		{
			microInteractionsInQue.Remove(contextId);
			return microInteractionsInProgress.Add(contextId);
		}

		public void QueInteraction(Guid contextId, bool interactionState)
		{
			if (microInteractionsInQue.ContainsKey(contextId))
			{
				microInteractionsInQue[contextId] = interactionState;
			}
			else
			{
				microInteractionsInQue.Add(contextId, interactionState);
			}
		}

		public void ClearInteractionManagerFromId(Guid contextId)
		{
			microInteractionsInProgress.Remove(contextId);
			microInteractionsInQue.Remove(contextId);
		}


		public QueState IsInteractionInQue(Guid contextId)
		{
			if(microInteractionsInQue.ContainsKey(contextId))
			{
				return new QueState(contextId, microInteractionsInQue[contextId]);
			}
			else
			{
				return null;
			}
		}
    }

    public class QueState
	{
		public Guid Id { get; private set; }
		public bool State { get; private set; }

		public QueState(Guid id, bool state)
        {
			State = state;
			Id = id;
		}
	}
}
