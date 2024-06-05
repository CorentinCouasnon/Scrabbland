using System;
using System.Collections.Generic;
using System.Linq;
using Powerups;
using UnityEngine.Rendering;

public class Participant
{
    int _score;
    int _handicap;
    int _actions;
    
    public CharacterSO Character { get; set; }
    public ObservableList<Letter> Letters { get; set; }
    public List<Powerup> Powerups { get; set; }

    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            ScoreChanged?.Invoke(_score);
        }
    }

    public int Handicap
    {
        get => _handicap;
        set
        {
            _handicap = value;
            HandicapChanged?.Invoke(_handicap);
        }
    }

    public int Actions
    {
        get => _actions;
        set
        {
            _actions = value;
            ActionsChanged?.Invoke(_actions);
        }
    }

    public Action<int> ScoreChanged { get; set; }
    public Action<int> HandicapChanged { get; set; }
    public Action<int> ActionsChanged { get; set; }

    public Participant(CharacterSO character)
    {
        Character = character;
        Letters = new ObservableList<Letter>();
        Powerups = character.BasePowerups.Select(powerupSO => new Powerup(powerupSO)).ToList();
        Score = 0;
        Handicap = character.BaseHandicap;
        Actions = 160 + 20 * character.BaseWisdom;
    }

    public void Draw()
    {
        if (Actions < 20 - Character.BaseSpeed)
            return;

        if (Letters.Count == 6 + Character.BaseIntelligence)
            return;

        Actions -= 20 - Character.BaseSpeed;
        Letters.Add(LetterUtils.CreateRandomLetter());
    }
}