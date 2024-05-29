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
        
            match.GetCurrentParticipant().Letters.Clear();
        }
    }
}