using AdventureFSM;

public class TreasureLocation : Location
{
    public override void Open()
    {
        base.Open();

        if (!CanBeSelected)
            return;
        
        AdventureController.Instance.State = new TreasureState();
    }
}