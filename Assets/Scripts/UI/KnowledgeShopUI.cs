using System.Collections.Generic;
using Powerups;
using TMPro;
using UnityEngine;

public class KnowledgeShopUI : MonoBehaviour
{
    [SerializeField] MainMenu _mainMenu;
    [SerializeField] GameObject _container;
    [SerializeField] List<KnowledgeShopSlotUI> _slots;
    [SerializeField] List<PowerupSO> _powerups;
    [SerializeField] TextMeshProUGUI _quidsText;

    public void Show()
    {
        _container.SetActive(true);

        for (int i = 0; i < _powerups.Count; i++)
        {
            _slots[i].PowerupSO = _powerups[i];
        }

        _quidsText.text = $"{ProgressController.Instance.Progress.Quids}";
        ProgressController.Instance.Progress.QuidsChanged += OnQuidsChanged;
    }

    public void Hide()
    {
        _container.SetActive(false);
        _mainMenu.Show();
        ProgressController.Instance.Progress.QuidsChanged -= OnQuidsChanged;
    }

    void OnQuidsChanged(int newValue)
    {
        _quidsText.text = $"{newValue}";
    }
}