using UnityEngine;

[CreateAssetMenu]
public class DifficultySO : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
}