using AdventureFSM;

public class MatchLocation : Location
{
    public override void Open()
    {
        base.Open();

        if (!CanBeSelected)
            return;
        
        AdventureController.Instance.State = new MatchState();
    }
}