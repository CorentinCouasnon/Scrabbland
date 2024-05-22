using System.Collections.Generic;
using System.Linq;
using MatchFSM;
using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField] List<CharacterSO> _characters;

    MatchState _state;
    
    public MatchState State
    {
        get => _state;
        set
        {
            _state?.Leave();
            _state = value;
            _state.Enter(this);
        }
    }

    public Match Match { get; set; }
    
    public static MatchController Instance { get; set; }
    
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        State?.Update();
    }

    public Match CreateMatch(CharacterSO playerCharacter, int opponentCount)
    {
        var match = new Match();
        match.Participants = new List<Participant>();
        match.Participants.Add(new Participant(playerCharacter));

        var opponents = new List<CharacterSO>(_characters)
            .Where(character => character.Name != playerCharacter.Name)
            .OrderBy(_ => Random.value)
            .Take(opponentCount);

        foreach (var opponent in opponents)
        {
            match.Participants.Add(new Participant(opponent));
        }
        
        return match;
    }
}