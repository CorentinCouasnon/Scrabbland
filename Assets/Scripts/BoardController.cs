using System.Collections.Generic;
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

    public void ResetBoard()
    {
        _slots.ForEach(slot =>
        {
            slot.Letter = null;
            slot.IsLetterLocked = false;
        });
    }
}