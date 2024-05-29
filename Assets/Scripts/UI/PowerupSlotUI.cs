using Powerups;
using UnityEngine;
using UnityEngine.UI;

public class PowerupSlotUI : MonoBehaviour
{
    [SerializeField] Image _image;

    Powerup _powerup;
    
    public Powerup Powerup
    {
        get => _powerup;
        set
        {
            _powerup = value;
            _image.sprite = _powerup.PowerupSO.Icon;
        } 
    }

    public void Use()
    {
        StartCoroutine(Powerup.Use(MatchController.Instance.Match));
    }
}