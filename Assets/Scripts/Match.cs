using System.Collections.Generic;
using System.Linq;

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
        if (Participants.All(p => p.Score >= p.Handicap))
            return;

        do
        {
            _currentParticipantIndex++;

            if (_currentParticipantIndex == Participants.Count)
                _currentParticipantIndex = 0;
        } 
        while (Participants[_currentParticipantIndex].Score >= Participants[_currentParticipantIndex].Handicap);
    }
}