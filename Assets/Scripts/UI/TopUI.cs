using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopUI : MonoBehaviour
{
    [SerializeField] Image _characterIcon;
    [SerializeField] TextMeshProUGUI _characterName;
    [SerializeField] TextMeshProUGUI _characterIntelligence;
    [SerializeField] TextMeshProUGUI _characterSpeed;
    [SerializeField] TextMeshProUGUI _characterWisdom;
    [SerializeField] TextMeshProUGUI _characterGold;

    Character _character;

    void OnEnable()
    {
        var adventure = AdventureController.Instance.Adventure;
        
        if (adventure == null)
            return;

        _character = adventure.Character;

        if (_character.CharacterSO == null)
            return;
        
        _characterIcon.sprite = _character.CharacterSO.Icon;
        _characterName.text = _character.CharacterSO.Name;
        _characterIntelligence.text = _character.Intelligence.ToString();
        _characterSpeed.text = _character.Speed.ToString();
        _characterWisdom.text = _character.Wisdom.ToString();
        _characterGold.text = _character.Gold.ToString();

        _character.IntelligenceChanged += OnIntelligenceChanged;
        _character.SpeedChanged += OnSpeedChanged;
        _character.WisdomChanged += OnWisdomChanged;
        _character.GoldChanged += OnGoldChanged;
    }

    void OnDisable()
    {
        Clear();

        _character.IntelligenceChanged -= OnIntelligenceChanged;
        _character.SpeedChanged -= OnSpeedChanged;
        _character.WisdomChanged -= OnWisdomChanged;
        _character.GoldChanged -= OnGoldChanged;
    }

    void OnIntelligenceChanged(int oldValue, int newValue)
    {
        _characterIntelligence.text = $"{newValue}";
    }
    
    void OnSpeedChanged(int oldValue, int newValue)
    {
        _characterSpeed.text = $"{newValue}";
    }
    
    void OnWisdomChanged(int oldValue, int newValue)
    {
        _characterWisdom.text = $"{newValue}";
    }
    
    void OnGoldChanged(int oldValue, int newValue)
    {
        _characterGold.text = $"{newValue}";
    }

    void Clear()
    {
        _characterIcon.sprite = null;
        _characterName.text = string.Empty;
        _characterIntelligence.text = string.Empty;
        _characterSpeed.text = string.Empty;
        _characterWisdom.text = string.Empty;
        _characterGold.text = string.Empty;
    }
}