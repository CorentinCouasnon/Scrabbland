using AdventureFSM;

public class EncounterLocation : Location
{
    public override void Open()
    {
        base.Open();
        
        if (!CanBeSelected)
            return;
        
        AdventureController.Instance.State = new EncounterState();
    }
}