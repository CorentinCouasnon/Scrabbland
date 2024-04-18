﻿using System.Collections;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    [SerializeField] MainMenu _mainMenu;
    [SerializeField] AdventureSelectionMenu _adventureSelectionMenu;
    [SerializeField] GameUI _gameUI;

    public Adventure Adventure { get; set; }
    
    public static AdventureController Instance { get; set; }
    
    void Awake()
    {
        Instance = this;
    }

    public IEnumerator PlayAdventure()
    {
        _adventureSelectionMenu.Show();
        AdventureSelectionMenu.AdventureSelected += OnAdventureSelected;
        yield return new WaitWhile(() => Adventure == null);
        _adventureSelectionMenu.Hide();
        Adventure.Character = new Character(Adventure.SelectedCharacter);
        _gameUI.Show();
    }

    public void QuitAdventure()
    {
        _gameUI.Hide();
        Adventure = null;
        _mainMenu.Show();
    }

    void OnAdventureSelected(Adventure adventure)
    {
        Adventure = adventure;
        AdventureSelectionMenu.AdventureSelected -= OnAdventureSelected;
    }
}