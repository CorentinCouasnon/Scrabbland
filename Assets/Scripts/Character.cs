using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterSO CharacterSO { get; private set; }
    public int Intelligence { get; private set; }
    public int Speed { get; private set; }
    public int Wisdom { get; private set; }
    public int Handicap { get; private set; }
}