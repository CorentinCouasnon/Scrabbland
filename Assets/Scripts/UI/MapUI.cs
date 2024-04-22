using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField] List<IslandUI> _islands;
    
    public void ChooseIslandLayout(int islandIndex)
    {
        _islands[islandIndex].SelectRandomLayout();    
    }

    public void Clear()
    {
        _islands.ForEach(island => island.Clear());
    }
}