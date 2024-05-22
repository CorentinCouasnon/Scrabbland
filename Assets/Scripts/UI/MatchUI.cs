using UnityEngine;

public class MatchUI : MonoBehaviour
{
    [SerializeField] Transform _leaderboard;
    [SerializeField] EntryUI _entryPrefab;

    public void Setup()
    {
        Clear();

        var match = MatchController.Instance.Match;

        // Letters
        
        // Powerups

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