using System.Collections;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Power-ups/Vowel")]
    public class VowelPowerupSO : PowerupSO
    {
        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);
        
            var participant = match.GetCurrentParticipant();

            if (participant.Letters.Count == 6 + participant.Character.BaseIntelligence)
                yield break;
        
            participant.Letters.Add(LetterUtils.CreateRandomVowel());
        }
    }
}