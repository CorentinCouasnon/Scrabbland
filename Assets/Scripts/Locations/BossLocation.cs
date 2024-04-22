public class BossLocation : Location
{
    public override void Open()
    {
        if (!CanBeSelected)
            return;
        
        _gameUI.OpenMatch();
    }
}