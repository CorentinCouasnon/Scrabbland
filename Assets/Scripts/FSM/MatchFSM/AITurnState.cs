using DG.Tweening;

namespace MatchFSM
{
    public class AITurnState : MatchState
    {
        public override void Enter(MatchController matchController)
        {
            base.Enter(matchController);
            
            var participant = MatchController.Instance.Match.GetCurrentParticipant();
            participant.Draw();
            participant.Score += 10;
            
            var seq = DOTween.Sequence();
            seq.AppendInterval(3f);
            seq.OnComplete(() => { MatchController.Instance.State = new ChangeTurnState(); });
        }
    }
}