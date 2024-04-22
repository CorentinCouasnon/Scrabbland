using System.Collections;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    [SerializeField] MainMenu _mainMenu;
    [SerializeField] AdventureSelectionMenu _adventureSelectionMenu;
    [SerializeField] GameUI _gameUI;
    [SerializeField] MapUI _mapUI;

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
        _mapUI.ChooseIslandLayout(0);
        _gameUI.Show();
        _gameUI.OpenMap();
    }

    public void QuitAdventure()
    {
        _gameUI.Hide();
        _mapUI.Clear();
        Adventure = null;
        _mainMenu.Show();
    }

    void OnAdventureSelected(Adventure adventure)
    {
        Adventure = adventure;
        AdventureSelectionMenu.AdventureSelected -= OnAdventureSelected;
    }
}