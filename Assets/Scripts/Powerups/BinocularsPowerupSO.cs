using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Power-ups/Binoculars")]
    public class BinocularsPowerupSO : PowerupSO
    {
        [SerializeField] List<LetterSO> _letters;
        
        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);

            Participant participantChosen = null;
            var otherParticipants = match.Participants.Where(p => p != match.GetCurrentParticipant()).ToList();
            
            yield return SelectionPanelUI.Instance.Open("Choose an opponent", otherParticipants.Select(p => p.Character.Icon).ToList(),
                index => participantChosen = otherParticipants[index]);
            
            yield return SelectionPanelUI.Instance.Open($"{participantChosen.Character.Name}'s letters",
                participantChosen.Letters.Select(l => _letters.Single(so => so.Value == l.Value).BaseSprite).ToList());
        }
    }
}