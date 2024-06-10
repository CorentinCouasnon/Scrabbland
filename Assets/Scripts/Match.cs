using System.Collections.Generic;

public class Match
{
    int _currentParticipantIndex;
    
    public List<Participant> Participants { get; set; }

    public Participant GetCurrentParticipant()
    {
        return Participants[_currentParticipantIndex];
    }

    public void SwitchCurrentParticipant()
    {
        _currentParticipantIndex++;

        if (_currentParticipantIndex == Participants.Count)
            _currentParticipantIndex = 0;
    }
}