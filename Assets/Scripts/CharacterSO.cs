using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, Multiline] public string Description { get; private set; }
    [field: SerializeField, Range(0, 10)] public int BaseIntelligence { get; private set; }
    [field: SerializeField, Range(0, 10)] public int BaseSpeed { get; private set; }
    [field: SerializeField, Range(0, 10)] public int BaseWisdom { get; private set; }
    [field: SerializeField] public int BaseHandicap { get; private set; }
}