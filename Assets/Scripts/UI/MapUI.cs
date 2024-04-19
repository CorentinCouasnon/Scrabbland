using UnityEngine;

public class MapUI : MonoBehaviour
{
    Adventure _adventure;

    void OnEnable()
    {
        _adventure = AdventureController.Instance.Adventure;
        
        if (_adventure == null)
            return;

    }

    void OnDisable()
    {
        Clear();
    }
    
    void Clear()
    {
        _adventure = null;
    }
}