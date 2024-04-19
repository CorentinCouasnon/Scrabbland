public class TreasureLocation : Location
{
    public override void Open()
    {
        _gameUI.OpenTreasure();
    }
}