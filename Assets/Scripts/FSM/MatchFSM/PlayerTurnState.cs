using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MatchFSM
{
    public class PlayerTurnState : MatchState
    {
        List<LetterSlotUI> _letterSlots;
        
        public override void Enter(MatchController matchController)
        {
            base.Enter(matchController);
            
            var matchUI = Object.FindAnyObjectByType<MatchUI>(FindObjectsInactive.Include);
            _letterSlots = Object.FindObjectsByType<LetterSlotUI>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList();
            matchUI.EnablePlayerInputs();
            matchUI.Drawn += OnDrawn;
            matchUI.Validated += OnValidated;
            matchUI.EndedTurn += OnEndedTurn;
            LetterSlotUI.Clicked += OnLetterSlotClicked;
            BoardSlotUI.Clicked += OnBoardSlotClicked;
        }

        public override void Leave()
        {
            base.Leave();
            
            var matchUI = Object.FindAnyObjectByType<MatchUI>(FindObjectsInactive.Include);
            matchUI.DisablePlayerInputs();
            matchUI.Drawn -= OnDrawn;
            matchUI.Validated -= OnValidated;
            matchUI.EndedTurn -= OnEndedTurn;
            LetterSlotUI.Clicked -= OnLetterSlotClicked;
            BoardSlotUI.Clicked -= OnBoardSlotClicked;
        }

        void OnDrawn()
        {
            MatchController.Instance.Match.GetCurrentParticipant().Draw();
        }

        void OnValidated()
        {
            
        }

        void OnEndedTurn()
        {
            MatchController.Instance.State = new ChangeTurnState();
        }

        void OnLetterSlotClicked(LetterSlotUI slot)
        {
            _letterSlots.ForEach(slot => slot.IsLetterSelected = false);
            
            if (slot.Letter != null)
                slot.IsLetterSelected = true;
        }
        
        void OnBoardSlotClicked(BoardSlotUI slot)
        {
            // Move a letter already on board
            if (slot.Letter != null)
            {
                
            }
            // Place a new letter
            else
            {
                var selectedLetterSlot = _letterSlots.SingleOrDefault(slot => slot.IsLetterSelected);

                if (selectedLetterSlot == null)
                    return;

                slot.Letter = selectedLetterSlot.Letter;
                slot.Letter.OnBoard = true;
                selectedLetterSlot.Letter = null;
                selectedLetterSlot.IsLetterSelected = false;
            }
        }
    }
}