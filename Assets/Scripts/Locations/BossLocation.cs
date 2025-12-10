using AdventureFSM;

public class BossLocation : Location
{
    public override void Open()
    {
        base.Open();
        
        if (!CanBeSelected)
            return;

        AdventureController.Instance.State = new BossState();
    }
}