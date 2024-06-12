using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Power-ups/Theft")]
    public class TheftPowerupSO : PowerupSO
    {
        [SerializeField] List<LetterSO> _letters;
        
        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);

            var participant = match.GetCurrentParticipant();
            Participant participantChosen = null;
            var otherParticipants = match.Participants.Where(p => p != participant).ToList();
            
            yield return SelectionPanelUI.Instance.Open("Choose an opponent", otherParticipants.Select(p => p.Character.Icon).ToList(),
                index => participantChosen = otherParticipants[index]);

            Letter letterChosen = null;
            
            yield return SelectionPanelUI.Instance.Open($"Choose a letter to steal from {participantChosen.Character.Name}",
                participantChosen.Letters.Select(l => _letters.Single(so => so.Value == l.Value).BaseSprite).ToList(),
                index => letterChosen = participantChosen.Letters[index]);

            participantChosen.Letters.Remove(letterChosen);
            
            if (participant.Letters.Count == 6 + participant.Character.BaseIntelligence)
                yield break;
            
            participant.Letters.Add(new Letter { Value = letterChosen.Value });
        }
    }
}