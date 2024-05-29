using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatchUI : MonoBehaviour
{
    // Powerups
    [SerializeField] List<PowerupSlotUI> _slots;

    // Leaderboard
    [SerializeField] Transform _leaderboard;
    [SerializeField] EntryUI _entryPrefab;

    public void Setup()
    {
        Clear();

        var adventure = AdventureController.Instance.Adventure;
        var match = MatchController.Instance.Match;
        var playerParticipant = match.Participants.Single(p => p.Character == adventure.SelectedCharacter);

        // Letters
        
        // Powerups
        for (var i = 0; i < playerParticipant.Powerups.Count; i++)
        {
            var powerup = playerParticipant.Powerups[i];
            _slots[i].Powerup = powerup;
        }

        // Leaderboard
        foreach (var participant in match.Participants)
        {
            var newEntry = Instantiate(_entryPrefab, _leaderboard);
            newEntry.Participant = participant;
        }
    }

    public void Clear()
    {
        foreach (Transform entry in _leaderboard)
        {
            Destroy(entry.gameObject);
        }
    }
}