using System.Collections.Generic;
using UnityEngine;

public class LocationRandomizer : MonoBehaviour
{
    [SerializeField] List<GameObject> _possibleLocations;

    public void Resolve()
    {
        _possibleLocations.GetRandom().SetActive(true);
    }

    public void Clear()
    {
        _possibleLocations.ForEach(location => location.SetActive(false));
    }
}