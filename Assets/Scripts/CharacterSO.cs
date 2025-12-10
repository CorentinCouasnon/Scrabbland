using System.Collections.Generic;
using Powerups;
using UnityEngine;

[CreateAssetMenu]
public class CharacterSO : ScriptableObject
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField, Multiline] public string Description { get; private set; }
    [field: SerializeField, Range(0, 10)] public int BaseIntelligence { get; set; }
    [field: SerializeField, Range(0, 10)] public int BaseSpeed { get; set; }
    [field: SerializeField, Range(0, 10)] public int BaseWisdom { get; set; }
    [field: SerializeField] public int BaseHandicap { get; private set; }
    [field: SerializeField] public List<PowerupSO> BasePowerups { get; private set; }

    public void AllocatePoints(int points)
    {
        BaseIntelligence = 0;
        BaseSpeed = 0;
        BaseWisdom = 0;
        
        var attributions = new int[3];

        for (int i = 0; i < points; i++)
        {
            var characteristic = Random.Range(0, 3);
            attributions[characteristic]++;
        }

        BaseIntelligence = attributions[0];
        BaseSpeed = attributions[1];
        BaseWisdom = attributions[2];
    }
}