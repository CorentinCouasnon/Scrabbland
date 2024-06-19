using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopUI : MonoBehaviour
{
    [SerializeField] TraitUpgradeUI _intelligenceUpgrade;
    [SerializeField] TraitUpgradeUI _speedUpgrade;
    [SerializeField] TraitUpgradeUI _wisdomUpgrade;
    
    [SerializeField] List<int> _prices;
    
    public Action Quitted { get; set; }

    void OnEnable()
    {
        var character = AdventureController.Instance.Adventure.Character;

        _intelligenceUpgrade.Set(character.Intelligence, _prices[character.Intelligence]);
        _speedUpgrade.Set(character.Speed, _prices[character.Speed]);
        _wisdomUpgrade.Set(character.Wisdom, _prices[character.Wisdom]);

        _intelligenceUpgrade.Bought += OnIntelligenceBought;
        _speedUpgrade.Bought += OnSpeedBought;
        _wisdomUpgrade.Bought += OnWisdomBought;
    }

    void OnDisable()
    {
        _intelligenceUpgrade.Bought -= OnIntelligenceBought;
        _speedUpgrade.Bought -= OnSpeedBought;
        _wisdomUpgrade.Bought -= OnWisdomBought;
    }

    public void Quit()
    {
        Quitted?.Invoke();
    }

    void OnIntelligenceBought()
    {
        var character = AdventureController.Instance.Adventure.Character;

        if (character.Intelligence >= 10)
            return;

        if (character.Gold < _prices[character.Intelligence])
            return;

        character.Gold -= _prices[character.Intelligence];
        character.Intelligence++;
        character.CharacterSO.BaseIntelligence++;
        _intelligenceUpgrade.Set(character.Intelligence, _prices[character.Intelligence]);
    }
    
    void OnSpeedBought()
    {
        var character = AdventureController.Instance.Adventure.Character;

        if (character.Speed >= 10)
            return;

        if (character.Gold < _prices[character.Speed])
            return;

        character.Gold -= _prices[character.Speed];
        character.Speed++;
        character.CharacterSO.BaseSpeed++;
        _speedUpgrade.Set(character.Speed, _prices[character.Speed]);
    }
    
    void OnWisdomBought()
    {
        var character = AdventureController.Instance.Adventure.Character;

        if (character.Wisdom >= 10)
            return;

        if (character.Gold < _prices[character.Wisdom])
            return;

        character.Gold -= _prices[character.Wisdom];
        character.Wisdom++;
        character.CharacterSO.BaseWisdom++;
        _wisdomUpgrade.Set(character.Wisdom, _prices[character.Wisdom]);
    }
}