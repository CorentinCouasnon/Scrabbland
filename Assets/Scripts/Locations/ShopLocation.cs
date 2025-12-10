using AdventureFSM;

public class ShopLocation : Location
{
    public override void Open()
    {
        base.Open();

        if (!CanBeSelected)
            return;
        
        AdventureController.Instance.State = new ShopState();
    }
}