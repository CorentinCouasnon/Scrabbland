public class TreasureLocation : Location
{
    public override void Open()
    {
        if (!CanBeSelected)
            return;
        
        _gameUI.OpenTreasure();
    }
}