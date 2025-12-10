namespace MatchFSM
{
    public abstract class MatchState
    {
        protected MatchController _matchController;

        public virtual void Enter(MatchController matchController)
        {
            _matchController = matchController;
        }

        public virtual void Update() { }
        public virtual void Leave() { }
    }
}