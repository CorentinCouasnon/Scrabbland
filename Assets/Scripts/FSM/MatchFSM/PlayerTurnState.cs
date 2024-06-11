using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MatchFSM
{
    public class PlayerTurnState : MatchState
    {
        MatchUI _matchUI;
        Participant _participant;
        List<LetterSlotUI> _letterSlots;
        List<PowerupSlotUI> _powerupSlots;
        BoardController _boardController;
        WordsValidator _wordsValidator;
        ScoreCounter _scoreCounter;
        
        public override void Enter(MatchController matchController)
        {
            base.Enter(matchController);

            _participant = MatchController.Instance.Match.GetCurrentParticipant();
            _boardController = Object.FindAnyObjectByType<BoardController>(FindObjectsInactive.Include);
            _wordsValidator = Object.FindAnyObjectByType<WordsValidator>(FindObjectsInactive.Include);
            _scoreCounter = Object.FindAnyObjectByType<ScoreCounter>(FindObjectsInactive.Include);
            _matchUI = Object.FindAnyObjectByType<MatchUI>(FindObjectsInactive.Include);
            _letterSlots = Object.FindObjectsByType<LetterSlotUI>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList();
            _powerupSlots = Object.FindObjectsByType<PowerupSlotUI>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList();
            _powerupSlots.ForEach(powerup => powerup.CanBeUsed = true);
            _matchUI.EnablePlayerInputs();
            _matchUI.Drawn += OnDrawn;
            _matchUI.Validated += OnValidated;
            _matchUI.EndedTurn += OnEndedTurn;
            LetterSlotUI.Clicked += OnLetterSlotClicked;
            BoardSlotUI.Clicked += OnBoardSlotClicked;
        }

        public override void Leave()
        {
            base.Leave();

            _matchUI.DisablePlayerInputs();
            _matchUI.Drawn -= OnDrawn;
            _matchUI.Validated -= OnValidated;
            _matchUI.EndedTurn -= OnEndedTurn;
            _boardController.ClearUnselectedLetters();
            _matchUI.UpdateLetters(_participant.Letters);
            _powerupSlots.ForEach(powerup => powerup.CanBeUsed = false);
            LetterSlotUI.Clicked -= OnLetterSlotClicked;
            BoardSlotUI.Clicked -= OnBoardSlotClicked;
        }

        void OnDrawn()
        {
            _participant.Draw();
        }

        void OnValidated()
        {
            if (!_boardController.IsCenterSlotOccupied())
                return;
            
            var words = _boardController.GetNewWords();

            if (words.Any(word => !_wordsValidator.IsWord(string.Join("", word.Select(l => l.Letter.Value)))))
                return;

            var score = _scoreCounter.GetScore(words);

            _boardController.LockNewLetters();

            _participant.Score += score;
            for (var i = _participant.Letters.Count - 1; i >= 0; i--)
            {
                var letter = _participant.Letters[i];

                if (letter.OnBoard)
                    _participant.Letters.Remove(letter);
            }
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
            // Remove board letter
            if (slot.Letter != null)
            {
                _letterSlots.ForEach(slot => slot.IsLetterSelected = false);
                slot.Letter.OnBoard = false;
                slot.Letter = null;
                _matchUI.UpdateLetters(_participant.Letters);
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