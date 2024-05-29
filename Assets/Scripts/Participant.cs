using System.Collections.Generic;
using System.Linq;
using Powerups;

public class Participant
{
    public CharacterSO Character { get; set; }
    public List<Letter> Letters { get; set; }
    public List<Powerup> Powerups { get; set; }
    public int Score { get; set; }
    public int Handicap { get; set; }
    public int Actions { get; set; }

    public Participant(CharacterSO character)
    {
        Character = character;
        Letters = new List<Letter>();
        Powerups = character.BasePowerups.Select(powerupSO => new Powerup(powerupSO)).ToList();
        Score = 0;
        Handicap = character.BaseHandicap;
        Actions = 160 + 20 * character.BaseWisdom;
    }
}