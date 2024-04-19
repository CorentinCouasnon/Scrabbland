using System;

public class Adventure
{
    public CharacterSO SelectedCharacter { get; set; }
    public DifficultySO SelectedDifficulty { get; set; }
    
    public Character Character { get; set; }
    
    public Act Act { get; set; }
}