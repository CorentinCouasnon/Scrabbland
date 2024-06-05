using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LetterSlotUI : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Button _button;
    [SerializeField] List<LetterSO> _lettersSO;
    [SerializeField] Sprite _emptySprite;

    Letter _letter;
    bool _isLocked;
    bool _isLetterSelected;
    
    public Letter Letter
    {
        get => _letter;
        set
        {
            _letter = value;

            if (_letter == null)
            {
                _image.sprite = _emptySprite;
            }
            else
            {
                var so = _lettersSO.Single(l => l.Value == _letter.Value);
                _image.sprite = so.BaseSprite;
            }
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
    
    public bool IsLetterSelected
    {
        get => _isLetterSelected;
        set
        {
            _isLetterSelected = value;

            if (_letter != null)
            {
                 var so = _lettersSO.Single(l => l.Value == _letter.Value);

                 if (_isLetterSelected)
                     _image.sprite = so.ActiveSprite;
                 else
                     _image.sprite = so.BaseSprite;
            }
        }
    }
    
    public static Action<LetterSlotUI> Clicked { get; set; }

    public void Click()
    {
        Clicked?.Invoke(this);
    }
}