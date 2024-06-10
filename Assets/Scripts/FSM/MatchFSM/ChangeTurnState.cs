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
            particpant.Actions += 50;
            MatchController.Instance.State = particpant.IsPlayer ? new PlayerTurnState() : new AITurnState();
        }
    }
}