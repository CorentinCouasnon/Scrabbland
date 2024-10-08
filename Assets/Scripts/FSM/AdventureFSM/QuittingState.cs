﻿using UnityEngine;

namespace AdventureFSM
{
    public class QuittingState : AdventureState
    {
        MainMenu _mainMenu;
        GameUI _gameUI;
        MapUI _mapUI;
        
        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);
            
            _mainMenu = Object.FindAnyObjectByType<MainMenu>(FindObjectsInactive.Include);
            _gameUI = Object.FindAnyObjectByType<GameUI>(FindObjectsInactive.Include);
            _mapUI = Object.FindAnyObjectByType<MapUI>(FindObjectsInactive.Include);

            _adventureController.Adventure.SelectedCharacter.BaseIntelligence = 0;
            _adventureController.Adventure.SelectedCharacter.BaseWisdom = 0;
            _adventureController.Adventure.SelectedCharacter.BaseSpeed = 0;
            
            _gameUI.Hide();
            _mapUI.Clear();
            _adventureController.Adventure = null;
            _mainMenu.Show();
        }
    }
}