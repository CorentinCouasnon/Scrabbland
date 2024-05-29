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
        
            match.GetCurrentParticipant().Letters.Clear();
        }
    }
}