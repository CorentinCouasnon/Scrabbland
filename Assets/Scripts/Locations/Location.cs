using UnityEngine;

public abstract class Location : MonoBehaviour
{
    protected GameUI _gameUI;

    [field: SerializeField] public int Step { get; private set; }
    
    public bool CanBeSelected { get; set; }
    
    void Awake()
    {
        _gameUI = FindAnyObjectByType<GameUI>();
    }

    public virtual void Open()
    {
        if (!CanBeSelected)
            return;
        
        AdventureController.Instance.Adventure.CurrentStep++;
    }
}