using System.Collections;
using UnityEngine;

namespace Powerups
{
    public abstract class PowerupSO : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int BuyCost { get; private set; }
        [field: SerializeField] public int UseCost { get; private set; }
        [field: SerializeField] public int UsageCount { get; private set; }

        public virtual IEnumerator Apply(Match match)
        {
            Debug.Log(Name + " used !");
            yield break;
        }
    }
}