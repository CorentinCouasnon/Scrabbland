using UnityEngine;

public abstract class Location : MonoBehaviour
{
    protected GameUI _gameUI;

    public bool CanBeSelected { get; set; }
    
    void Awake()
    {
        _gameUI = FindAnyObjectByType<GameUI>();
    }

    public abstract void Open();
}