using System;
using TMPro;
using UnityEngine;

public class TraitUpgradeUI : MonoBehaviour
{
    [SerializeField] Transform _container;
    [SerializeField] GameObject _upgradeBarPrefab;
    [SerializeField] TextMeshProUGUI _buyButton;

    public Action Bought { get; set; }
    
    public void Set(int point, int price)
    {
        Clear();

        for (int i = 0; i < point; i++)
        {
            Instantiate(_upgradeBarPrefab, _container);
        }

        _buyButton.text = price == -1 ? "MAX" : $"{price} <sprite=0>";
    }

    public void Buy()
    {
        Bought?.Invoke();
    }
    
    void Clear()
    {
        foreach (Transform child in _container)
        {
            Destroy(child.gameObject);
        }
    }
}