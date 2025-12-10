using System.Collections;

namespace Powerups
{
    public class Powerup
    {
        public PowerupSO PowerupSO { get; set; }
        public int UsageRemainingCount { get; set; }

        public Powerup(PowerupSO powerupSO)
        {
            PowerupSO = powerupSO;
            UsageRemainingCount = powerupSO.UsageCount;
        }

        public IEnumerator Use(Match match)
        {
            if (UsageRemainingCount == 0)
                yield break;

            var user = match.GetCurrentParticipant();
            
            if (user.Actions < PowerupSO.UseCost)
                yield break;

            user.Actions -= PowerupSO.UseCost;
            yield return PowerupSO.Apply(match);
            UsageRemainingCount--;
        }
    }
}