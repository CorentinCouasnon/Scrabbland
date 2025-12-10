using System.Collections.Generic;
using UnityEngine;

public class IslandLayoutUI : MonoBehaviour
{
    [SerializeField] List<LocationRandomizer> _randomizers;

    public void ResolveAllRandomizers()
    {
        _randomizers.ForEach(randomizer => randomizer.Resolve());
    }

    public void Clear()
    {
        _randomizers.ForEach(randomizer => randomizer.Clear());
    }
}