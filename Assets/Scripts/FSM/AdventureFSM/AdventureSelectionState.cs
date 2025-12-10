using UnityEngine;

namespace AdventureFSM
{
    public class AdventureSelectionState : AdventureState
    {
        AdventureSelectionMenu _adventureSelectionMenu;
        
        public override void Enter(AdventureController adventureController)
        {
            base.Enter(adventureController);

            _adventureSelectionMenu = Object.FindAnyObjectByType<AdventureSelectionMenu>(FindObjectsInactive.Include);
            
            AdventureSelectionMenu.AdventureSelected += OnAdventureSelected;
            _adventureSelectionMenu.Show();
        }

        public override void Leave()
        {
            base.Leave();
            
            AdventureSelectionMenu.AdventureSelected -= OnAdventureSelected;
            _adventureSelectionMenu.Hide();
        }
        
        void OnAdventureSelected(Adventure adventure)
        {
            adventure.Character = new Character(adventure.SelectedCharacter);
            _adventureController.Adventure = adventure;
            
            _adventureController.State = new InitializationState();
        }
    }
}