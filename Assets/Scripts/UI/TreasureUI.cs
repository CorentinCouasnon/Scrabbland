using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreasureUI : MonoBehaviour
{
    [SerializeField] GameObject _openButton;
    [SerializeField] GameObject _continueButton;
    [SerializeField] TextMeshProUGUI _text;
    
    public Action Continued { get; set; }

    void OnEnable()
    {
        _openButton.SetActive(true);
        _continueButton.SetActive(false);
        _text.gameObject.SetActive(false);
    }

    public void Open()
    {
        var goldGained = Random.Range(100, 300);
        var temp = 0;

        _openButton.SetActive(false);
        _text.text = "You found 0 <sprite=0> in the chest.";
        _text.gameObject.SetActive(true);
        
        DOTween.To(() => temp, x => temp = x, goldGained, 3f)
            .OnUpdate(() => { 
                _text.text = $"You found <size={temp/(float)goldGained*100+30}>{temp}</size> <sprite=0> in the chest.";
            })
            .OnComplete(() => {
                AdventureController.Instance.Adventure.Character.Gold += goldGained;
                _continueButton.SetActive(true);
            });
    }
    
    public void Continue()
    {
        Continued?.Invoke();
    }
}