using UnityEngine;

namespace MatchFSM
{
    public class InitializingState : MatchState
    {
        public override void Enter(MatchController matchController)
        {
            base.Enter(matchController);
            
            var matchUI = Object.FindAnyObjectByType<MatchUI>(FindObjectsInactive.Include);
            matchUI.Setup();
            MatchController.Instance.State = new PlayerTurnState();
        }
    }
}