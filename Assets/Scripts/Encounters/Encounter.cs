using UnityEngine;

namespace Encounters
{
    [CreateAssetMenu]
    public class Encounter : ScriptableObject
    {
        [field: SerializeField] public Sprite Avatar { get; private set; }
        [field: SerializeField] public string Text { get; private set; }
        [field: SerializeField] public string Option1Text { get; private set; }
        [field: SerializeField] public string Option2Text { get; private set; }
        [field: SerializeField] public int GoldGained { get; private set; }
        [field: SerializeField] public int Risk { get; private set; }

        public string Accept()
        {
            var randomValue = Random.value;

            if (randomValue >= Risk / 100f)
            {
                AdventureController.Instance.Adventure.Character.Gold += GoldGained;
                return $"It's your lucky day, you won {GoldGained} gold!";
            }

            AdventureController.Instance.Adventure.Character.Gold -= GoldGained;
            return $"Unlucky, you lost {GoldGained} gold.";
        }

        public string Reject()
        {
            return "You refused the deal.";
        }
    }
}