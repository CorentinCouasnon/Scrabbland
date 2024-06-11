using System.Collections;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Power-ups/Boost")]
    public class BoostPowerupSO : PowerupSO
    {
        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);

            var participant = match.GetCurrentParticipant();

            for (var i = 0; i < 3; i++)
            {
                if (participant.Letters.Count == 6 + participant.Character.BaseIntelligence)
                    break;
        
                participant.Letters.Add(LetterUtils.CreateRandomLetter());
            }
        }
    }
}