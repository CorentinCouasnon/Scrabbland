using UnityEngine;

public abstract class Location : MonoBehaviour
{
    protected GameUI _gameUI;

    [field: SerializeField] public int Weight { get; private set; }

    void Awake()
    {
        _gameUI = FindAnyObjectByType<GameUI>();
    }

    public abstract void Open();
}