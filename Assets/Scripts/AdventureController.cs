using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    [SerializeField] MainMenu _mainMenu;
    [SerializeField] AdventureSelectionMenu _adventureSelectionMenu;
    [SerializeField] GameUI _gameUI;
    [SerializeField] List<Location> _locationPrefabs;

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
        Adventure.Act = CreateAct(16, false);
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

    Act CreateAct(int locationCount, bool isLastAct)
    {
        var act = new Act { Locations = new List<Location>() };

        for (var i = 0; i < locationCount - 1; i++)
        {
            act.Locations.Add(_locationPrefabs.GetRandomWeighted(location => location.Weight));
        }
        
        act.Locations.Add(_locationPrefabs[0]); // Add a match
        
        return act;
    }
}