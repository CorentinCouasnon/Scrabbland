using UnityEngine;

[CreateAssetMenu]
public class LetterSO : ScriptableObject
{
    [field: SerializeField] public char Value { get; private set; }
    [field: SerializeField] public Sprite BaseSprite { get; private set; }
    [field: SerializeField] public Sprite ActiveSprite { get; private set; }
    [field: SerializeField] public Sprite FixedSprite { get; private set; }
}