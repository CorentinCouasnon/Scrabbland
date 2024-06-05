using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BoardSlotUI : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Button _button;
    [SerializeField] List<LetterSO> _lettersSO;
    [SerializeField] Sprite _emptySprite;

    Letter _letter;

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

    public static Action<BoardSlotUI> Clicked { get; set; }

    public void Click()
    {
        Clicked?.Invoke(this);
    }
}