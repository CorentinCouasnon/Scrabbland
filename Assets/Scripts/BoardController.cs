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
    
    public List<List<BoardSlotUI>> GetNewWords()
    {
        var newSlots = _slots.Where(slot => slot.Letter != null && !slot.IsLetterLocked);
        var words = new List<List<BoardSlotUI>>();

        foreach (var newSlot in newSlots)
        {
            var horizontalPrefix = GetSlotSequence(newSlot.transform.GetSiblingIndex(), -1);
            var verticalPrefix = GetSlotSequence(newSlot.transform.GetSiblingIndex(), -19);
            var horizontalSuffix = GetSlotSequence(newSlot.transform.GetSiblingIndex(), 1);
            var verticalSuffix = GetSlotSequence(newSlot.transform.GetSiblingIndex(), 19);

            var horizontalWord = new List<BoardSlotUI>();
            horizontalWord.AddRange(horizontalPrefix);
            horizontalWord.Add(newSlot);
            horizontalWord.AddRange(horizontalSuffix);
            
            var verticalWord = new List<BoardSlotUI>();
            verticalWord.AddRange(verticalPrefix);
            verticalWord.Add(newSlot);
            verticalWord.AddRange(verticalSuffix);
            
            if (horizontalWord.Count >= 2)
                words.Add(horizontalWord);
            if (verticalWord.Count >= 2)
                words.Add(verticalWord);
        }
        
        var duplicateTracker = new List<List<BoardSlotUI>>();

        for (var i = words.Count - 1; i >= 0; i--)
        {
            var word = words[i];

            if (!duplicateTracker.Any(sw => sw.SequenceEqual(word)))
                duplicateTracker.Add(word);
            else
                words.Remove(word);
        }
        
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

    List<BoardSlotUI> GetSlotSequence(int start, int increment)
    {
        var letters = new List<BoardSlotUI>();
        var pointer = start;
        
        while (!IsOutOfBounds(pointer + increment))
        {
            pointer += increment;

            var slot = _slots[pointer];
            
            if (slot.Letter == null)
                break;

            if (increment > 0)
                letters.Add(slot);
            else
                letters.Insert(0, slot);
        }

        return letters;
    }

    bool IsOutOfBounds(int siblingIndex)
    {
        return siblingIndex is < 0 or > 360;
    }
}