using System.Collections;
using UnityEngine;

namespace PowerUps
{
    [CreateAssetMenu(menuName = "Power-ups/Trash")]
    public class TrashPowerUp : PowerUp
    {
        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);
        
            match.GetCurrentParticipant().Letters.Clear();
        }
    }
}