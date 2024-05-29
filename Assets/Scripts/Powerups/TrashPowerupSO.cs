using System.Collections;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Power-ups/Trash")]
    public class TrashPowerupSO : PowerupSO
    {
        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);
        
            match.GetCurrentParticipant().Letters.Clear();
        }
    }
}