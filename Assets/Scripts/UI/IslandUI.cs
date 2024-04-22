using System.Collections.Generic;
using UnityEngine;

public class IslandUI : MonoBehaviour
{
    [SerializeField] List<IslandLayoutUI> _layouts;

    public void SelectRandomLayout()
    {
        Clear();
        var randomLayout = _layouts.GetRandom();
        randomLayout.gameObject.SetActive(true);
        randomLayout.ResolveAllRandomizers();
    }

    public void Clear()
    {
        _layouts.ForEach(layout =>
        {
            if (!layout.gameObject.activeSelf)
                return;
            
            layout.Clear();
            layout.gameObject.SetActive(false);
        });
    }
}