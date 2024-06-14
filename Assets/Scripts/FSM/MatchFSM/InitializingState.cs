using DG.Tweening;
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
            
            var turnSwitcherUI = Object.FindAnyObjectByType<TurnSwitcherUI>(FindObjectsInactive.Include);
            var match = MatchController.Instance.Match;
            turnSwitcherUI.Show($"{match.GetCurrentParticipant().Character.Name}'s turn");

            var seq = DOTween.Sequence();
            seq.AppendInterval(1.5f);
            seq.OnComplete(() =>
            {
                turnSwitcherUI.Hide();
                MatchController.Instance.State = new PlayerTurnState();
            });
        }
    }
}