using UnityEngine;

namespace AdventureFSM
{
    public class InitializationState : AdventureState
    {
        GameUI _gameUI;
        MapUI _mapUI;
        
        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);

            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _mapUI = Object.FindAnyObjectByType<MapUI>(FindObjectsInactive.Include);
            
            _mapUI.ChooseIslandLayout(0);
            _gameUI.Show();
            
            _adventureController.State = new LocationSelectionState();
        }
    }
}