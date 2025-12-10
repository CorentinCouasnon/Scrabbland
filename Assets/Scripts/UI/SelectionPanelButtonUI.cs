using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SelectionPanelButtonUI : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Button _button;

    public void Initialize(UnityAction onClick, Sprite sprite)
    {
        _image.sprite = sprite;
        _button.onClick.AddListener(() => onClick?.Invoke());
    }
}