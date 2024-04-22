public class UndergroundLocation : Location
{
    public override void Open()
    {
        if (!CanBeSelected)
            return;
        
        _gameUI.OpenMatch();
    }
}