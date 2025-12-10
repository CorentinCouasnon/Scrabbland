using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Power-ups/Recycling")]
    public class RecyclingPowerupSO : PowerupSO
    {
        [SerializeField] List<LetterSO> _letters;

        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);
        
            var participant = match.GetCurrentParticipant();

            if (participant.Letters.IsEmpty())
                yield break;

            yield return SelectionPanelUI.Instance.Open("Choose a letter to replace",
                participant.Letters.Select(l => _letters.Single(so => so.Value == l.Value).BaseSprite).ToList(),
                index =>
                {
                    participant.Letters[index] = LetterUtils.CreateRandomLetter();
                });
        }
    }
}