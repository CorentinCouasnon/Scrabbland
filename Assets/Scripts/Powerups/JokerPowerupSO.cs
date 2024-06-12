using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Power-ups/Joker")]
    public class JokerPowerupSO : PowerupSO
    {
        [SerializeField] List<LetterSO> _letters;

        public override IEnumerator Apply(Match match)
        {
            yield return base.Apply(match);

            LetterSO letterChosen = null;

            yield return SelectionPanelUI.Instance.Open("Choose a letter", _letters.Select(letter => letter.BaseSprite).ToList(),
                index => letterChosen = _letters[index]);
            
            var participant = match.GetCurrentParticipant();
            
            if (participant.Letters.Count == 6 + participant.Character.BaseIntelligence)
                yield break;
            
            participant.Letters.Add(new Letter { Value = letterChosen.Value });
        }
    }
}