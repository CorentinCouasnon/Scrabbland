using Powerups;
using UnityEngine;
using UnityEngine.UI;

public class PowerupSlotUI : MonoBehaviour
{
    [SerializeField] Image _image;
    [SerializeField] Sprite _emptySprite;

    Powerup _powerup;
    
    public Powerup Powerup
    {
        get => _powerup;
        set
        {
            _powerup = value;

            if (_powerup == null)
                _image.sprite = _emptySprite;
            else
                _image.sprite = _powerup.PowerupSO.Icon;
        } 
    }
    
    public bool CanBeUsed { get; set; }

    public void Use()
    {
        if (!CanBeUsed)
            return;
        
        if (Powerup == null)
            return;
        
        StartCoroutine(Powerup.Use(MatchController.Instance.Match));
    }
}