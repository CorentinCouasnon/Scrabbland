using System.Collections.Generic;

public class Match
{
    public int _currentParticipantIndex;
    
    public List<Participant> Participants { get; set; }
    public Board Board { get; set; }

    public Participant GetCurrentParticipant()
    {
        return Participants[_currentParticipantIndex];
    }

    public void OnTurnEnded()
    {
        _currentParticipantIndex++;

        if (_currentParticipantIndex == Participants.Count)
            _currentParticipantIndex = 0;
    }
}