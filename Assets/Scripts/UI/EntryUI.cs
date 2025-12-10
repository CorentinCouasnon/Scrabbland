using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class EntryUI : MonoBehaviour
{
    [SerializeField] Image _icon;
    [SerializeField] TextMeshProUGUI _nameText;
    [SerializeField] TextMeshProUGUI _scoreText;
    [SerializeField] TextMeshProUGUI _actionText;
    [SerializeField] TextMeshProUGUI _letterCountText;

    Participant _participant;
    
    public Participant Participant
    {
        get => _participant;
        set
        {
            if (_participant != null)
            {
                _participant.Letters.ItemAdded -= OnLettersChanged;
                _participant.Letters.ItemRemoved -= OnLettersChanged;
                _participant.ActionsChanged -= OnActionsChanged;
                _participant.ScoreChanged -= OnScoreChanged;
                _participant.HandicapChanged -= OnHandicapChanged;
            }

            _participant = value;
            UpdateEntry();

            _participant.Letters.ItemAdded += OnLettersChanged;
            _participant.Letters.ItemRemoved += OnLettersChanged;
            _participant.ActionsChanged += OnActionsChanged;
            _participant.ScoreChanged += OnScoreChanged;
            _participant.HandicapChanged += OnHandicapChanged;
        }
    }

    void UpdateEntry()
    {
        _icon.sprite = Participant.Character.Icon;
        _nameText.text = Participant.Character.Name;
        _scoreText.text = $"{Participant.Score}/{Participant.Handicap}";
        _actionText.text = $"{Participant.Actions}";
        _letterCountText.text = $"{Participant.Letters.Count}";
    }

    void OnLettersChanged(ObservableList<Letter> letters, ListChangedEventArgs<Letter> args)
    {
        _letterCountText.text = $"{Participant.Letters.Count}";
    }

    void OnActionsChanged(int value)
    {
        _actionText.text = $"{Participant.Actions}";
    }

    void OnScoreChanged(int value)
    {
        _scoreText.text = $"{Participant.Score}/{Participant.Handicap}";
    }

    void OnHandicapChanged(int value)
    {
        _scoreText.text = $"{Participant.Score}/{Participant.Handicap}";
    }
}