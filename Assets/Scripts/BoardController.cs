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

    [ContextMenu("Debug_GetNewWords")]
    public void Debug_GetNewWords()
    {
        Debug.Log(GetNewWords().ToDisplayString());
    }

    [ContextMenu("Debug_ShowCandidate")]
    public void Debug_ShowCandidate()
    {
        foreach (var slot in _slots)
        {
            slot.ShowCandidateDebug();
        }
    }
    
    [ContextMenu("Debug_ResetCandidate")]
    public void Debug_ResetCandidate()
    {
        foreach (var slot in _slots)
        {
            slot.ResetCandidateDebug();
        }
    }
    
    public void LockNewLetters()
    {
        var newLetters = _slots.Where(slot => slot.Letter != null && !slot.IsLetterLocked);

        foreach (var newLetter in newLetters)
        {
            newLetter.IsLetterLocked = true;
        }
        
        GrayOutCandidatesCell();
    }
    
    public List<List<BoardSlotUI>> GetNewWords()
    {
        var newSlots = _slots.Where(slot => slot.Letter != null && !slot.IsLetterLocked);
        var words = new List<List<BoardSlotUI>>();

        foreach (var newSlot in newSlots)
        {
            var horizontalPrefix = GetWordSequence(newSlot.transform.GetSiblingIndex(), -1);
            var verticalPrefix = GetWordSequence(newSlot.transform.GetSiblingIndex(), -19);
            var horizontalSuffix = GetWordSequence(newSlot.transform.GetSiblingIndex(), 1);
            var verticalSuffix = GetWordSequence(newSlot.transform.GetSiblingIndex(), 19);

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

    public (BoardSlotUI top, BoardSlotUI topRight, BoardSlotUI right, BoardSlotUI downRight, BoardSlotUI down, BoardSlotUI downLeft, BoardSlotUI left, BoardSlotUI topLeft) 
        GetNeighbors(int siblingIndex)
    {
        (BoardSlotUI top, BoardSlotUI topRight, BoardSlotUI right, BoardSlotUI downRight, BoardSlotUI down, BoardSlotUI downLeft, BoardSlotUI left, BoardSlotUI topLeft) neighbors = 
            (null, null, null, null, null, null, null, null);

        if (!IsOutOfBounds(siblingIndex + 1))
            neighbors.right = _slots[siblingIndex + 1];
        
        if (!IsOutOfBounds(siblingIndex + 19))
            neighbors.down = _slots[siblingIndex + 19];
        
        if (!IsOutOfBounds(siblingIndex - 1))
            neighbors.left = _slots[siblingIndex - 1];
        
        if (!IsOutOfBounds(siblingIndex - 19))
            neighbors.top = _slots[siblingIndex - 19];
        
        if (!IsOutOfBounds(siblingIndex + 18))
            neighbors.downLeft = _slots[siblingIndex + 18];
        
        if (!IsOutOfBounds(siblingIndex + 20))
            neighbors.downRight = _slots[siblingIndex + 20];
        
        if (!IsOutOfBounds(siblingIndex - 18))
            neighbors.topRight = _slots[siblingIndex - 18];
        
        if (!IsOutOfBounds(siblingIndex - 20))
            neighbors.topLeft = _slots[siblingIndex - 20];
        
        return neighbors;
    }

    public List<BoardSlotUI> GetRowCells(int rowIndex)
    {
        var rowCells = new List<BoardSlotUI>();

        if (rowIndex is < 0 or >= 19)
            return null;

        for (var col = 0; col < 19; col++)
        {
            rowCells.Add(_slots[rowIndex * 19 + col]);
        }

        return rowCells;
    }

    public List<BoardSlotUI> GetColumnCells(int colIndex)
    {
        var columnCells = new List<BoardSlotUI>();

        if (colIndex is < 0 or >= 19)
            return null;

        for (var row = 0; row < 19; row++)
        {
            columnCells.Add(_slots[row * 19 + colIndex]);
        }

        return columnCells;
    }

    public int GetSiblingIndex(BoardSlotUI slot)
    {
        return _slots.IndexOf(slot);
    }

    public bool IsCenterSlotOccupied()
    {
        return _slots.Single(slot => slot.IsCenterSlot).Letter != null;
    }

    public void ResetBoard()
    {
        _slots.ForEach(slot =>
        {
            slot.Letter = null;
            slot.IsLetterLocked = false;
            slot.IsCandidate = true;
        });
    }

    public List<BoardSlotUI> GetSlotSequence(int start, int limit, int increment)
    {
        var letters = new List<BoardSlotUI>();
        var pointer = start;
        
        while (letters.Count < limit && !IsOutOfBounds(pointer + increment))
        {
            pointer += increment;

            var slot = _slots[pointer];
            
            letters.Add(slot);
        }

        return letters;
    }
    
    List<BoardSlotUI> GetWordSequence(int start, int increment)
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

    public void ClearUnselectedLetters()
    {
        _slots.ForEach(slot =>
        {
            if (slot.Letter != null && slot.IsLetterLocked == false)
            {
                slot.Letter.OnBoard = false;
                slot.Letter = null;
            }
        });
    }

    void GrayOutCandidatesCell()
    {
        var letterSlots = _slots.Where(slot => slot.Letter != null && slot.IsLetterLocked).ToList();

        foreach (var letterSlot in letterSlots)
        {
            letterSlot.IsCandidate = false;

            var neighbors = GetNeighbors(_slots.IndexOf(letterSlot));

            if (neighbors.top.Letter != null)
                neighbors.down.IsCandidate = false;
            
            if (neighbors.down.Letter != null)
                neighbors.top.IsCandidate = false;
            
            if (neighbors.right.Letter != null)
                neighbors.left.IsCandidate = false;
            
            if (neighbors.left.Letter != null)
                neighbors.right.IsCandidate = false;
            
            if (neighbors.topRight.Letter != null)
            {
                neighbors.top.IsCandidate = false;
                neighbors.right.IsCandidate = false;
            }
            
            if (neighbors.topLeft.Letter != null)
            {
                neighbors.top.IsCandidate = false;
                neighbors.left.IsCandidate = false;
            }
            
            if (neighbors.downRight.Letter != null)
            {
                neighbors.down.IsCandidate = false;
                neighbors.right.IsCandidate = false;
            }

            if (neighbors.downLeft.Letter != null)
            {
                neighbors.down.IsCandidate = false;
                neighbors.left.IsCandidate = false;
            }
        }
    }

    bool IsOutOfBounds(int siblingIndex)
    {
        return siblingIndex is < 0 or > 360;
    }
}