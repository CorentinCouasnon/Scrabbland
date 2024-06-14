using TMPro;
using UnityEngine;

public class TurnSwitcherUI : MonoBehaviour
{
    [SerializeField] GameObject _container;
    [SerializeField] TextMeshProUGUI _text;

    public void Show(string text)
    {
        _text.text = text;
        _container.SetActive(true);
    }

    public void Hide()
    {
        _container.SetActive(false);
    }
}