using System;
using System.Collections.Generic;
using Powerups;

public class Progress
{
    int _quids;
    
    public int Quids
    {
        get => _quids;
        set
        {
            _quids = value;
            QuidsChanged?.Invoke(_quids);
        } 
    }
    
    public List<PowerupSO> UnlockedPowerups { get; set; } = new List<PowerupSO>();
    
    public Action<int> QuidsChanged { get; set; }
}