using Powerups;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KnowledgeShopSlotUI : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Button _button;

    PowerupSO _powerupSO;

    public PowerupSO PowerupSO
    {
        get => _powerupSO;
        set
        {
            _powerupSO = value;
            UpdateLayout();
        } 
    }
    
    public void Buy()
    {
        if (ProgressController.Instance.Progress.Quids < PowerupSO.BuyCost)
            return;

        ProgressController.Instance.Progress.Quids -= PowerupSO.BuyCost;
        ProgressController.Instance.Progress.UnlockedPowerups.Add(PowerupSO);
        UpdateLayout();
    }

    void UpdateLayout()
    {
        _image.sprite = _powerupSO.Icon;

        var isAcquired = ProgressController.Instance.Progress.UnlockedPowerups.Contains(_powerupSO);

        _button.interactable = !isAcquired;

        if (isAcquired)
            _text.text = "ACQUIRED";
        else
            _text.text = $"{_powerupSO.BuyCost} <sprite=0>";
    }
}