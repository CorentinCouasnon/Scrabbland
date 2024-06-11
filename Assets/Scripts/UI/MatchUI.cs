using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class MatchUI : MonoBehaviour
{
    // Letters
    [SerializeField] List<LetterSlotUI> _letterSlots;
    [SerializeField] TextMeshProUGUI _actionsText;
    [SerializeField] Button _drawButton;
    [SerializeField] Button _validateButton;
    [SerializeField] Button _endTurnButton;

    // Powerups
    [SerializeField] List<PowerupSlotUI> _powerupSlots;

    // Leaderboard
    [SerializeField] Transform _leaderboard;
    [SerializeField] EntryUI _entryPrefab;
    
    public Action Drawn { get; set; }
    public Action Validated { get; set; }
    public Action EndedTurn { get; set; }

    public void Setup()
    {
        Clear();

        var adventure = AdventureController.Instance.Adventure;
        var match = MatchController.Instance.Match;
        var playerParticipant = match.Participants.Single(p => p.Character == adventure.SelectedCharacter);

        // Letters
        _actionsText.text = $"{playerParticipant.Actions}";
        _letterSlots.Skip(6 + playerParticipant.Character.BaseIntelligence).ToList().ForEach(slot => slot.IsLocked = true);

        playerParticipant.ActionsChanged += OnActionsChanged;
        playerParticipant.Letters.ItemAdded += OnLettersChanged;
        playerParticipant.Letters.ItemRemoved += OnLettersChanged;
        
        // Powerups
        for (var i = 0; i < playerParticipant.Powerups.Count; i++)
        {
            var powerup = playerParticipant.Powerups[i];
            _powerupSlots[i].Powerup = powerup;
        }

        // Leaderboard
        foreach (var participant in match.Participants)
        {
            var newEntry = Instantiate(_entryPrefab, _leaderboard);
            newEntry.Participant = participant;
        }
    }

    public void Draw()
    {
        Drawn?.Invoke();
    }

    public void Validate()
    {
        Validated?.Invoke();
    }

    public void EndTurn()
    {
        EndedTurn?.Invoke();
    }

    public void DisablePlayerInputs()
    {
        _drawButton.interactable = false;
        _validateButton.interactable = false;
        _endTurnButton.interactable = false;
    }

    public void EnablePlayerInputs()
    {
        _drawButton.interactable = true;
        _validateButton.interactable = true;
        _endTurnButton.interactable = true;
    }

    public void Clear()
    {
        var adventure = AdventureController.Instance.Adventure;
        var match = MatchController.Instance.Match;
        var playerParticipant = match.Participants.Single(p => p.Character == adventure.SelectedCharacter);
        
        playerParticipant.ActionsChanged -= OnActionsChanged;
        playerParticipant.Letters.ItemAdded -= OnLettersChanged;
        playerParticipant.Letters.ItemRemoved -= OnLettersChanged;
        
        DisablePlayerInputs();
        
        // Letters
        for (int i = 0; i < _letterSlots.Count; i++)
        {
            _letterSlots[i].IsLocked = false;
            _letterSlots[i].Letter = null;
        }
        
        // Powerups
        for (var i = 0; i < _powerupSlots.Count; i++)
        {
            _powerupSlots[i].Powerup = null;
        }
        
        // Leaderboard
        foreach (Transform entry in _leaderboard)
        {
            Destroy(entry.gameObject);
        }
    }

    void OnActionsChanged(int value)
    {
        _actionsText.text = $"{value}";
    }

    void OnLettersChanged(ObservableList<Letter> letters, ListChangedEventArgs<Letter> args)
    {
        UpdateLetters(letters);
    }

    public void UpdateLetters(ObservableList<Letter> letters)
    {
        for (int i = 0; i < _letterSlots.Count; i++)
        {
            _letterSlots[i].Letter = null;
        }
        
        for (var i = 0; i < letters.Count; i++)
        {
            if (letters[i].OnBoard)
                continue;
            
            _letterSlots[i].Letter = letters[i];
        }
    }
}