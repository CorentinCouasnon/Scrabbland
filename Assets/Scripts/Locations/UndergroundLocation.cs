using AdventureFSM;

public class UndergroundLocation : Location
{
    public override void Open()
    {
        base.Open();

        if (!CanBeSelected)
            return;
        
        AdventureController.Instance.State = new UndergroundState();
    }
}