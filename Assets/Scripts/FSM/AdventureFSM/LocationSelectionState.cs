using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdventureFSM
{
    public class LocationSelectionState : AdventureState
    {
        GameUI _gameUI;
        List<Location> _locations;

        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);
            
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);

            _gameUI.OpenMap();

            _locations = Object.FindObjectsByType<Location>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
            _locations.ForEach(location => location.CanBeSelected = _adventureController.Adventure.CurrentStep == location.Step);
        }

        public override void Leave()
        {
            base.Leave();
            
            _locations.ForEach(location => location.CanBeSelected = false);
            _gameUI.HideMap();
        }
    }
}