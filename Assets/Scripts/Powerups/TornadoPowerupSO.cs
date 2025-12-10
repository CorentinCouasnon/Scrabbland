using System.Collections;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Power-ups/Tornado")]
    public class TornadoPowerupSO : PowerupSO
    {
        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);
            
            var participant = match.GetCurrentParticipant();

            for (int i = 0; i < participant.Letters.Count; i++)
            {
                participant.Letters[i] = LetterUtils.CreateRandomLetter();
            }
        }
    }
}