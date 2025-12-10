using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MatchFSM
{
    public class AITurnState : MatchState
    {
        public override void Enter(MatchController matchController)
        {
            base.Enter(matchController);

            matchController.StartCoroutine(PlayTurn());
        }

        IEnumerator PlayTurn()
        {
            var boardController = Object.FindAnyObjectByType<BoardController>(FindObjectsInactive.Include);
            var wordsValidator = Object.FindAnyObjectByType<WordsValidator>(FindObjectsInactive.Include);
            var scoreCounter = Object.FindAnyObjectByType<ScoreCounter>(FindObjectsInactive.Include);
            var participant = MatchController.Instance.Match.GetCurrentParticipant();
            var waitBetweenDraws = new WaitForSeconds(0.2f);
            
            while (participant.CanDraw())
            {
                participant.Draw();
                yield return waitBetweenDraws;
            }

            var rowWordPlaced = false;
            
            List<BoardSlotUI> selectedRow = null;
            BoardSlotUI hookSlot = null;
            int hookSlotIndex = -1;
            
            for (int i = 0; i < 19; i++)
            {
                var row = boardController.GetRowCells(i);
                var lettersInRow = row.Where(s => s.Letter != null && s.IsLetterLocked).ToList();

                if (lettersInRow.Count == 1)
                {
                    if (row.All(s => s.IsCandidate || (!s.IsCandidate && s.Letter != null)))
                    {
                        hookSlot = lettersInRow[0];
                        hookSlotIndex = row.IndexOf(hookSlot);
                        selectedRow = row;
                        break;
                    }
                }
            }

            if (selectedRow != null)
            {
                var words = wordsValidator.Search(
                    string.Join("", new List<Letter>(participant.Letters) { hookSlot.Letter }.Select(l => l.Value)))
                    .OrderByDescending(w => w.Length).ToList();
                
                string selectedWord = null;
                int hookIndex = -1;
                
                foreach (var word in words)
                {
                    var index = word.ToLower().IndexOf(hookSlot.Letter.Value);
                    if (index == -1 || index > hookSlotIndex)
                        continue;

                    hookIndex = index;
                    selectedWord = word;
                    break;
                }
                
                if (selectedWord != null)
                {
                    var sequence = boardController.GetSlotSequence(boardController.GetSiblingIndex(hookSlot) - hookIndex - 1, selectedWord.Length, 1);

                    for (var i = 0; i < sequence.Count; i++)
                    {
                        var slot = sequence[i];
                        
                        if (slot.Letter != null)
                            continue;

                        var neededChar = char.ToLower(selectedWord[i]);
                        var pLetter = participant.Letters.First(l => !l.OnBoard && l.Value == neededChar);
                        pLetter.OnBoard = true;
                        slot.Letter = pLetter;
                    }
                    
                    yield return new WaitForSeconds(1f);

                    var newWords = boardController.GetNewWords();
                    var score = scoreCounter.GetScore(newWords);
                    boardController.LockNewLetters();
                    participant.Score += score;
                    
                    for (var i = participant.Letters.Count - 1; i >= 0; i--)
                    {
                        var letter = participant.Letters[i];

                        if (letter.OnBoard)
                            participant.Letters.Remove(letter);
                    }

                    rowWordPlaced = true;
                }
            }

            if (!rowWordPlaced)
            {
                List<BoardSlotUI> selectedCol = null;
                hookSlot = null;
                hookSlotIndex = -1;
                
                for (int i = 0; i < 19; i++)
                {
                    var column = boardController.GetColumnCells(i);
                    var lettersInCol = column.Where(s => s.Letter != null && s.IsLetterLocked).ToList();

                    if (lettersInCol.Count == 1)
                    {
                        if (column.All(s => s.IsCandidate || (!s.IsCandidate && s.Letter != null)))
                        {
                            hookSlot = lettersInCol[0];
                            hookSlotIndex = column.IndexOf(hookSlot);
                            selectedCol = column;
                            break;
                        }
                    }
                }

                if (selectedCol != null)
                {
                    var words = wordsValidator.Search(
                        string.Join("", new List<Letter>(participant.Letters) { hookSlot.Letter }.Select(l => l.Value)))
                        .OrderByDescending(w => w.Length).ToList();
                    
                    string selectedWord = null;
                    int hookIndex = -1;
                    
                    foreach (var word in words)
                    {
                        var index = word.ToLower().IndexOf(hookSlot.Letter.Value);
                        if (index == -1 || index > hookSlotIndex)
                            continue;

                        hookIndex = index;
                        selectedWord = word;
                        break;
                    }
                    
                    if (selectedWord != null)
                    {
                        var sequence = boardController.GetSlotSequence(boardController.GetSiblingIndex(hookSlot) - hookIndex * 19 - 19, selectedWord.Length, 19);

                        for (var i = 0; i < sequence.Count; i++)
                        {
                            var slot = sequence[i];
                            
                            if (slot.Letter != null)
                                continue;

                            var neededChar = char.ToLower(selectedWord[i]);
                            var pLetter = participant.Letters.First(l => !l.OnBoard && l.Value == neededChar);
                            pLetter.OnBoard = true;
                            slot.Letter = pLetter;
                        }
                        
                        yield return new WaitForSeconds(1f);

                        var newWords = boardController.GetNewWords();
                        var score = scoreCounter.GetScore(newWords);
                        boardController.LockNewLetters();
                        participant.Score += score;
                        
                        for (var i = participant.Letters.Count - 1; i >= 0; i--)
                        {
                            var letter = participant.Letters[i];

                            if (letter.OnBoard)
                                participant.Letters.Remove(letter);
                        }
                    }
                }
            }
            
            yield return new WaitForSeconds(3f);

            MatchController.Instance.State = new ChangeTurnState();
        }
    }
}