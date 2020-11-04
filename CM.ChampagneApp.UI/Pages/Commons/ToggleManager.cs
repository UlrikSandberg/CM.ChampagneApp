using System;
using System.Threading.Tasks;
using CM.ChampagneApp.UI.FreshMvvmHelpers;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models.UIFollowStructure;

namespace CM.ChampagneApp.UI.Pages.Commons
{
    public class ToggleManager<T>
    {
        private readonly IMicroInteractionService _microInteractionService;
        private readonly BasePageModel _basePageModel;
        private readonly Action _toggledBehaviour;
        private readonly Action _untoggledBehaviour;

        public BoolToggle<T> BoolToggle { get; set; }
        public BoolToggle<T> CustomBoolToggle { get; }

        public ToggleManager(IMicroInteractionService microInteractionService, BasePageModel basePageModel, T toggledValue, T untoggledValue, Action toggledBehaviour = null, Action untoggleBehaviour = null, bool toggleState = false, BoolToggle<T> customBoolToggle = null)
        {
            _untoggledBehaviour = untoggleBehaviour;
            CustomBoolToggle = customBoolToggle;
            _toggledBehaviour = toggledBehaviour;
            _basePageModel = basePageModel;
            _microInteractionService = microInteractionService;
            BoolToggle = new BoolToggle<T>(toggledValue, untoggledValue, toggleState);
        }

        public async Task Handle_FollowToggle(Guid id, bool isUser = true)
        {
            //Read state from the bool toggle
            if (!App.MicroInteractionsQueManager.IsInteractionInProgress(id))
            {
                var state = BoolToggle.ToggleState;

                if(BoolToggle.ToggleState)
                {
                    BoolToggle.ToggleState = false;
                    UntoggledBehaviour();
                }
                else
                {
                    BoolToggle.ToggleState = true;
                    ToggledBehaviour();
                }

                var response = await _microInteractionService.HandleFollow(id, state, isUser);

                if(!response.IsSuccesfull)
                {
                    await _basePageModel.DisplayAlert(response.Message);
                }
            }
            else
            {
                if(BoolToggle.ToggleState)
                {
                    BoolToggle.ToggleState = false;
                    UntoggledBehaviour();
                    App.MicroInteractionsQueManager.QueInteraction(id, false);
                }
                else
                {
                    BoolToggle.ToggleState = true;
                    ToggledBehaviour();
                    App.MicroInteractionsQueManager.QueInteraction(id, true);
                }
            }
        }

        
        public async Task Handle_FollowToggle(Guid id, bool state, bool isUser = true, Action toggleBehaviour = null, Action untoggleBehaviour = null)
        {
            if (!App.MicroInteractionsQueManager.IsInteractionInProgress(id))
            {
                var prevState = state;

                if (state) //True - unlike / unfollow
                {
                    if (untoggleBehaviour != null)
                    {
                        untoggleBehaviour();
                    }
                }
                else //False - Like / follow
                {
                    if (toggleBehaviour != null)
                    {
                        toggleBehaviour();
                    }
                }

                var response = await _microInteractionService.HandleFollow(id, prevState, isUser);

                if (!response.IsSuccesfull)
                {
                    await _basePageModel.DisplayAlert(response.Message);
                }
            }
            else
            {
                if (state) //True - unlike / unfollow
                {
                    if (untoggleBehaviour != null)
                    {
                        untoggleBehaviour();
                    }
                    App.MicroInteractionsQueManager.QueInteraction(id, false);
                }
                else
                {
                    if (toggleBehaviour != null)
                    {
                        toggleBehaviour();
                    }
                    App.MicroInteractionsQueManager.QueInteraction(id, true);
                }
            }
        }

        public async Task Handle_BookmarkToggle(Guid champagneId)
        {
            if(!App.MicroInteractionsQueManager.IsInteractionInProgress(champagneId))
            {
                var state = BoolToggle.ToggleState;

                if(state) //We should unbookmark
                {
                    BoolToggle.ToggleState = false;
                    UntoggledBehaviour();
                }
                else
                {
                    BoolToggle.ToggleState = true;
                    ToggledBehaviour();
                }

                var response = await _microInteractionService.HandleBookmark(champagneId, state);

                if(!response.IsSuccesfull)
                {
                    await _basePageModel.DisplayAlert(response.Message);
                }
            }
            else
            {
                if(BoolToggle.ToggleState)
                {
                    BoolToggle.ToggleState = false;
                    App.MicroInteractionsQueManager.QueInteraction(champagneId, false);
                    UntoggledBehaviour();
                }
                else
                {
                    BoolToggle.ToggleState = true;
                    App.MicroInteractionsQueManager.QueInteraction(champagneId, false);
                    ToggledBehaviour();
                }
            }
        }

        public async Task Handle_LikeToggle(Guid contextId, bool state, bool isTasting = true, Action toggleBehaviour = null, Action untoggleBehaviour = null)
        {
            if (!App.MicroInteractionsQueManager.IsInteractionInProgress(contextId))
            {
                var prevState = state;

                if (state) //We should unbookmark
                {
                    if (untoggleBehaviour != null)
                        untoggleBehaviour();
                }
                else
                {
                    if (toggleBehaviour != null)
                        toggleBehaviour();
                }

                var response = await _microInteractionService.HandleLike(contextId, prevState, isTasting);

                if (!response.IsSuccesfull)
                {
                    await _basePageModel.DisplayAlert(response.Message);
                }
            }
            else
            {
                if (state)
                {
                    if (untoggleBehaviour != null)
                        untoggleBehaviour();
                    App.MicroInteractionsQueManager.QueInteraction(contextId, false);
                }
                else
                {
                    if (toggleBehaviour != null)
                        toggleBehaviour();
                    App.MicroInteractionsQueManager.QueInteraction(contextId, false);
                }
            }
        }

        private void UntoggledBehaviour()
        {
            if (_untoggledBehaviour != null)
            {
                _untoggledBehaviour();
            }
        }

        private void ToggledBehaviour()
        {
            if (_toggledBehaviour != null)
            {
                _toggledBehaviour();
            }
        }
    }
}
