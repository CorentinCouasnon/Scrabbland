using System.Collections;
using UnityEngine;

namespace PowerUps
{
    public abstract class PowerUp : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
    
        public virtual IEnumerator Apply(Match match) { yield break; }
    }
}