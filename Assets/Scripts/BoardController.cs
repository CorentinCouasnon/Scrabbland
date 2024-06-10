using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField] List<BoardSlotUI> _slots;

    void OnEnable()
    {
        BoardSlotUI.OutsideLetterPlaced += ResetBoard;
    }

    void OnDisable()
    {
        BoardSlotUI.OutsideLetterPlaced -= ResetBoard;
    }

    [ContextMenu("Test")]
    public void Debug_GetNewWords()
    {
        Debug.Log(GetNewWords().ToDisplayString());
    }

    public void LockNewLetters()
    {
        var newLetters = _slots.Where(slot => slot.Letter != null && !slot.IsLetterLocked);

        foreach (var newLetter in newLetters)
        {
            newLetter.IsLetterLocked = true;
        }
    }
    
    public List<string> GetNewWords()
    {
        var newSlots = _slots.Where(slot => slot.Letter != null && !slot.IsLetterLocked);
        var words = new List<string>();

        foreach (var newSlot in newSlots)
        {
            var horizontalPrefix = GetLetterSequence(newSlot.transform.GetSiblingIndex(), -1);
            var verticalPrefix = GetLetterSequence(newSlot.transform.GetSiblingIndex(), -19);
            var horizontalSuffix = GetLetterSequence(newSlot.transform.GetSiblingIndex(), 1);
            var verticalSuffix = GetLetterSequence(newSlot.transform.GetSiblingIndex(), 19);

            var horizontalWord = $"{horizontalPrefix}{newSlot.Letter.Value}{horizontalSuffix}";
            var verticalWord = $"{verticalPrefix}{newSlot.Letter.Value}{verticalSuffix}";
            
            if (horizontalWord.Length >= 2)
                words.Add(horizontalWord);
            if (verticalWord.Length >= 2)
                words.Add(verticalWord);
        }

        words = words.Distinct().ToList();

        return words;
    }

    public void ResetBoard()
    {
        _slots.ForEach(slot =>
        {
            slot.Letter = null;
            slot.IsLetterLocked = false;
        });
    }

    string GetLetterSequence(int start, int increment)
    {
        var letters = string.Empty;
        var pointer = start;
        
        while (!IsOutOfBounds(pointer + increment))
        {
            pointer += increment;

            var slot = _slots[pointer];
            
            if (slot.Letter == null)
                break;

            if (increment > 0)
                letters += slot.Letter.Value;
            else
                letters = letters.Insert(0, $"{slot.Letter.Value}");
        }

        return letters;
    }

    bool IsOutOfBounds(int siblingIndex)
    {
        return siblingIndex is < 0 or > 360;
    }
}