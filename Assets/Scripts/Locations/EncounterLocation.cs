public class EncounterLocation : Location
{
    public override void Open()
    {
        if (!CanBeSelected)
            return;
        
        _gameUI.OpenEncounter();
    }
}