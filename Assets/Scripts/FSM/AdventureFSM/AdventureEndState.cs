using UnityEngine;

namespace AdventureFSM
{
    public class AdventureEndState : AdventureState
    {
        GameUI _gameUI;
        AdventureEndUI _adventureEndUI;

        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);
            
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _gameUI.OpenMap();
            
            _adventureEndUI = Object.FindAnyObjectByType<AdventureEndUI>(FindObjectsInactive.Include);
            _adventureEndUI.SetTexts();
            _adventureEndUI.Show();
            _adventureEndUI.Continued += OnContinued;
        }

        public override void Leave()
        {
            base.Leave();

            _adventureEndUI.Hide();
        }

        void OnContinued()
        {
            _adventureEndUI.Continued -= OnContinued;

            AdventureController.Instance.State = new QuittingState();
        }
    }
}