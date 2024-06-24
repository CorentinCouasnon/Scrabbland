using UnityEngine;

namespace AdventureFSM
{
    public class TreasureState : AdventureState 
    {
        GameUI _gameUI;
        TreasureUI _treasureUI;
        
        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);
            
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _gameUI.OpenTreasure();
            
            _treasureUI = Object.FindAnyObjectByType<TreasureUI>(FindObjectsInactive.Include);
            _treasureUI.Continued += OnContinued;
        }
        
        public override void Leave()
        {
            base.Leave();
            
            _gameUI.HideTreasure();
        }

        void OnContinued()
        {
            _treasureUI.Continued -= OnContinued;
            AdventureController.Instance.State = new LocationSelectionState();
        }
    }
}