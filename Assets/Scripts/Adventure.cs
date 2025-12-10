public class Adventure
{
    public CharacterSO SelectedCharacter { get; set; }
    public DifficultySO SelectedDifficulty { get; set; }
    
    public Character Character { get; set; }
    
    public int CurrentStep { get; set; }
    
    public bool IsBossDefeated { get; set; }
    public int TotalOpponentDefeated { get; set; }
    public int TotalActCompleted { get; set; }
}