using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdventureSelectionMenu : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] Image _characterIconImage;
    [SerializeField] TextMeshProUGUI _characterNameText;
    [SerializeField] TextMeshProUGUI _characterDescriptionText;
    [SerializeField] TextMeshProUGUI _characterIntelligenceText;
    [SerializeField] TextMeshProUGUI _characterSpeedText;
    [SerializeField] TextMeshProUGUI _characterWisdomText;
    [SerializeField] TextMeshProUGUI _difficultyNameText;
    
    [Space]
    [SerializeField] List<CharacterSO> _characters;
    [SerializeField] List<DifficultySO> _difficulties;

    int _currentCharacterIndex;
    int _currentDifficultyIndex;
    
    public static Action<Adventure> AdventureSelected { get; set; }   
    
    public void Show()
    {
        Clear();
        UpdateLayout();
        _container.SetActive(true);
    }
    
    public void Hide()
    {
        _container.SetActive(false);
        Clear();
    }

    public void IncrementCharacterIndex()
    {
        if (_currentCharacterIndex == _characters.Count - 1)
            return;

        _currentCharacterIndex++;
        _currentDifficultyIndex = 0;
        UpdateLayout();
    }
    
    public void DecrementCharacterIndex()
    {
        if (_currentCharacterIndex == 0)
            return;

        _currentCharacterIndex--;
        _currentDifficultyIndex = 0;
        UpdateLayout();
    }

    public void IncrementDifficultyIndex()
    {
        if (_currentDifficultyIndex == _difficulties.Count - 1)
            return;

        _currentDifficultyIndex++;
        UpdateLayout();
    }
    
    public void DecrementDifficultyIndex()
    {
        if (_currentDifficultyIndex == 0)
            return;

        _currentDifficultyIndex--;
        UpdateLayout();
    }
    
    public void Confirm()
    {
        AdventureSelected?.Invoke(new Adventure
        {
            SelectedCharacter = _characters[_currentCharacterIndex],
            SelectedDifficulty = _difficulties[_currentDifficultyIndex],
        });
    }

    void UpdateLayout()
    {
        var currentCharacter = _characters[_currentCharacterIndex];
        var currentDifficulty = _difficulties[_currentDifficultyIndex];

        _characterIconImage.sprite = currentCharacter.Icon;
        _characterNameText.text = currentCharacter.Name;
        _characterDescriptionText.text = currentCharacter.Description;
        _characterIntelligenceText.text = currentCharacter.BaseIntelligence.ToString();
        _characterSpeedText.text = currentCharacter.BaseSpeed.ToString();
        _characterWisdomText.text = currentCharacter.BaseWisdom.ToString();
        _difficultyNameText.text = currentDifficulty.Name;
    }

    void Clear()
    {
        _currentCharacterIndex = 0;
        _currentDifficultyIndex = 0;
        
        _characterIconImage.sprite = null;
        _characterNameText.text = string.Empty;
        _characterDescriptionText.text = string.Empty;
        _characterIntelligenceText.text = string.Empty;
        _characterSpeedText.text = string.Empty;
        _characterWisdomText.text = string.Empty;
        _difficultyNameText.text = string.Empty;
    }
}