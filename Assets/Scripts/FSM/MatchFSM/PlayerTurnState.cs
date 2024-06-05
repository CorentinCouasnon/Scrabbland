using UnityEngine;

namespace MatchFSM
{
    public class PlayerTurnState : MatchState
    {
        public override void Enter(MatchController matchController)
        {
            base.Enter(matchController);
            
            var matchUI = Object.FindAnyObjectByType<MatchUI>(FindObjectsInactive.Include);
            matchUI.EnablePlayerInputs();
            matchUI.Drawn += OnDrawn;
            matchUI.Validated += OnValidated;
            matchUI.EndedTurn += OnEndedTurn;
        }

        public override void Leave()
        {
            base.Leave();
            
            var matchUI = Object.FindAnyObjectByType<MatchUI>(FindObjectsInactive.Include);
            matchUI.DisablePlayerInputs();
            matchUI.Drawn -= OnDrawn;
            matchUI.Validated -= OnValidated;
            matchUI.EndedTurn -= OnEndedTurn;
        }

        void OnDrawn()
        {
            MatchController.Instance.Match.GetCurrentParticipant().Draw();
        }

        void OnValidated()
        {
            
        }

        void OnEndedTurn()
        {
            MatchController.Instance.State = new ChangeTurnState();
        }
    }
}