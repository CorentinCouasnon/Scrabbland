﻿using System.Collections.Generic;
using System.Linq;
using MatchFSM;
using Powerups;
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
        var participant = new Participant(playerCharacter);
        participant.Powerups.AddRange(ProgressController.Instance.Progress.UnlockedPowerups.Where(p => p.Name != "Trash").Select(p => new Powerup(p)).OrderBy(p => p.PowerupSO.BuyCost));
        match.Participants.Add(participant);

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

    [ContextMenu("Debug match state")]
    public void Debug_MatchState()
    {
        Debug.Log(State);
    }
}