using UnityEngine;

namespace AdventureFSM
{
    public class EncounterState : AdventureState { 
        
        GameUI _gameUI;
        EncounterUI _encounterUI;

        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);
            
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _gameUI.OpenEncounter();
            
            _encounterUI = Object.FindAnyObjectByType<EncounterUI>(FindObjectsInactive.Include);
            _encounterUI.Continued += OnContinued;
        }

        public override void Leave()
        {
            base.Leave();
            
            _gameUI.HideEncounter();
        }

        void OnContinued()
        {
            _encounterUI.Continued -= OnContinued;
            AdventureController.Instance.State = new LocationSelectionState();
        }
    }
}