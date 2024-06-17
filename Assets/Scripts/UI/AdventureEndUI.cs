using System;
using TMPro;
using UnityEngine;

public class AdventureEndUI : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _resultText;
    [SerializeField] TextMeshProUGUI _adventureQuidsText;
    [SerializeField] TextMeshProUGUI _actQuidsText;
    [SerializeField] TextMeshProUGUI _opponentsQuidsText;

    public Action Continued { get; set; }

    public void SetTexts()
    {
        var adventure = AdventureController.Instance.Adventure;
        var kingQuids = adventure.IsBossDefeated ? 50 : 0;
        var actQuids = adventure.TotalActCompleted * 10;
        var opponentsQuids = adventure.TotalOpponentDefeated;
        
        _text.text = $"{adventure.SelectedCharacter.Name}, {adventure.SelectedDifficulty.Name}";
        _resultText.text = adventure.IsBossDefeated ? "Victory!" : "Defeat..";
        _adventureQuidsText.text = $"King beaten : {kingQuids} <sprite=0>";
        _actQuidsText.text = $"Act completed : {actQuids} <sprite=0>";
        _opponentsQuidsText.text = $"Opponents defeated : {opponentsQuids} <sprite=0>";
        
        ProgressController.Instance.Progress.Quids += kingQuids + actQuids + opponentsQuids;
    }

    public void Show()
    {
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