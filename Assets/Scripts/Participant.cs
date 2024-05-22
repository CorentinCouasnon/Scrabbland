using System.Collections.Generic;
using PowerUps;

public class Participant
{
    public CharacterSO Character { get; set; }
    public List<Letter> Letters { get; set; }
    public List<PowerUp> PowerUps { get; set; }
    public int Score { get; set; }
    public int Handicap { get; set; }
    public int Actions { get; set; }

    public Participant(CharacterSO character)
    {
        Character = character;
        Letters = new List<Letter>();
        PowerUps = new List<PowerUp>(character.BasePowerUps);
        Score = 0;
        Handicap = character.BaseHandicap;
        Actions = 160 + 20 * character.BaseWisdom;
    }
}