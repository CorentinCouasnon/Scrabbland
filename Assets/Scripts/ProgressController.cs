using Powerups;
using UnityEngine;

public class ProgressController : MonoBehaviour
{
    [SerializeField] PowerupSO _trashPowerup;
    
    public Progress Progress { get; set; }
    
    public static ProgressController Instance { get; set; }
    
    void Awake()
    {
        Instance = this;
        
        Progress = new Progress();
        Progress.UnlockedPowerups.Add(_trashPowerup);
    }
}