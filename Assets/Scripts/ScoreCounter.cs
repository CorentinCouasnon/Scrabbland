using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    public int GetScore(List<List<BoardSlotUI>> words)
    {
        /* Score = Nb lettres déjà posées + Nb nouvelles lettres au carré */

        var score = 0;

        foreach (var word in words)
        {
            var fixedLetterCount = word.Count(slot => slot.IsLetterLocked);
            var newLetterCount = word.Count(slot => !slot.IsLetterLocked);
            
            score += fixedLetterCount + newLetterCount * newLetterCount;
        }

        return score;
    }
}