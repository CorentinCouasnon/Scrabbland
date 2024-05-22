using TMPro;
using UnityEngine;
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
            _participant = value;
            UpdateEntry();
        }
    }

    void UpdateEntry()
    {
        _icon.sprite = Participant.Character.Icon;
        _nameText.text = Participant.Character.Name;
        _scoreText.text = $"{0}/{Participant.Handicap}";
        _actionText.text = Participant.Actions.ToString();
        _letterCountText.text = Participant.Letters.Count.ToString();
    }
}