using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Power-ups/Revolution")]
    public class RevolutionPowerupSO : PowerupSO
    {
        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);
        
            var participant = match.GetCurrentParticipant();
            Participant participantChosen = null;
            var otherParticipants = match.Participants.Where(p => p != participant).ToList();
            
            yield return SelectionPanelUI.Instance.Open("Choose an opponent to switch your hand with", 
                otherParticipants.Select(p => p.Character.Icon).ToList(),
                index => participantChosen = otherParticipants[index]);

            var otherLetterCount = participantChosen.Letters.Count;
            var letterCount = participant.Letters.Count;
            
            if (otherLetterCount != letterCount)
                yield break;

            var temp = new List<Letter>(participant.Letters);

            for (int i = 0; i < letterCount; i++)
            {
                participant.Letters[i] = new Letter { Value = participantChosen.Letters[i].Value };
            }
            
            for (int i = 0; i < letterCount; i++)
            {
                participantChosen.Letters[i] = new Letter { Value = temp[i].Value };
            }
        }
    }
}