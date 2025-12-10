using DG.Tweening;
using UnityEngine;

namespace MatchFSM
{
    public class ChangeTurnState : MatchState
    {
        public override void Enter(MatchController matchController)
        {
            base.Enter(matchController);

            var match = MatchController.Instance.Match;
            match.SwitchCurrentParticipant();
            var particpant = match.GetCurrentParticipant();
            particpant.Actions += 40;
            
            var turnSwitcherUI = Object.FindAnyObjectByType<TurnSwitcherUI>(FindObjectsInactive.Include);
            turnSwitcherUI.Show($"{particpant.Character.Name}'s turn");

            var seq = DOTween.Sequence();
            seq.AppendInterval(1.5f);
            seq.OnComplete(() =>
            {
                turnSwitcherUI.Hide();
                MatchController.Instance.State = particpant.IsPlayer ? new PlayerTurnState() : new AITurnState();
            });
        }
    }
}