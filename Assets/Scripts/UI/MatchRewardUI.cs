using System;
using TMPro;
using UnityEngine;

public class MatchRewardUI : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _goldText;

    public Action Continued { get; set; }
    
    public void Show(string text, int goldGained)
    {
        _text.text = text;
        _goldText.text = $"{goldGained}";
        _container.SetActive(true);
    }

    public void Hide()
    {
        _container.SetActive(false);
    }

    public void Continue()
    {
        Continued?.Invoke();
    }
}