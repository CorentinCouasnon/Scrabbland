using AdventureFSM;
using UnityEngine;

public class AdventureController : MonoBehaviour
{
    AdventureState _state;
    
    public AdventureState State
    {
        get => _state;
        set
        {
            _state?.Leave();
            _state = value;
            _state.Enter(this);
        }
    }

    public Adventure Adventure { get; set; }
    
    public static AdventureController Instance { get; set; }
    
    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        State?.Update();
    }
}