using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LetterSlotUI : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Button _button;
    [SerializeField] List<LetterSO> _lettersSO;

    Letter _letter;
    bool _isLocked;
    
    public Letter Letter
    {
        get => _letter;
        set
        {
            _letter = value;

            var so = _lettersSO.Single(l => l.Value == _letter.Value);
            _image.sprite = so.BaseSprite;
        } 
    }

    public bool IsLocked
    {
        get => _isLocked;
        set
        {
            _isLocked = value;
            _button.interactable = !_isLocked;
        }
    }
}