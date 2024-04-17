using System.Collections;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    [SerializeField] AdventureSelectionMenu _adventureSelectionMenu;

    public static AdventureController Instance { get; set; }
    
    void Awake()
    {
        Instance = this;
    }

    public void PlayAdventure()
    {
        _adventureSelectionMenu.Show();
    }
}