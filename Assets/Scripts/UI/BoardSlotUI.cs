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
    [SerializeField] bool _isOutsideSlot;
    [SerializeField] bool _isCenterSlot;
    [SerializeField] Sprite _candidateDebugSprite;

    Letter _letter;
    bool _isLetterLocked;

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
                _image.sprite = IsLetterLocked ? so.FixedSprite : so.BaseSprite;
            }
        } 
    }
    
    public bool IsLetterLocked
    {
        get => _isLetterLocked;
        set
        {
            _isLetterLocked = value;

            if (Letter == null)
                return;
            
            var so = _lettersSO.Single(l => l.Value == _letter.Value);
            _image.sprite = _isLetterLocked ? so.FixedSprite : so.BaseSprite;
            
            if (_isOutsideSlot && _isLetterLocked)
                OutsideLetterPlaced?.Invoke();
        } 
    }
    
    public bool IsCenterSlot => _isCenterSlot;
    
    public bool IsCandidate { get; set; }

    public static Action<BoardSlotUI> Clicked { get; set; }
    public static Action OutsideLetterPlaced { get; set; }

    public void Click()
    {
        Clicked?.Invoke(this);
    }

    public void ShowCandidateDebug()
    {
        if (!IsCandidate && Letter == null)
        {
            _image.sprite = _candidateDebugSprite;
        }
    }

    public void ResetCandidateDebug()
    {
        if (_image.sprite == _candidateDebugSprite)
        {
            _image.sprite = _emptySprite;
        }
    }
}