namespace AdventureFSM
{
    public abstract class AdventureState
    {
        protected AdventureController _adventureController;

        public virtual void Enter(AdventureController adventureController)
        {
            _adventureController = adventureController;
        }

        public virtual void Update() { }
        public virtual void Leave() { }
    }
}