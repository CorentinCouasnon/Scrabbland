using TMPro;
using UnityEngine;

public class CheatInputUI : MonoBehaviour
{
    [SerializeField] TMP_InputField _inputField;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            _inputField.gameObject.SetActive(true);
    }

    public void Validate()
    {
        var text = _inputField.text;

        if (text == "iwanttoberich")
        {
            ProgressController.Instance.Progress.Quids += 2000;
        }
        
        if (text == "verbovirtuoso")
        {
            var participant = MatchController.Instance.Match?.GetCurrentParticipant();
            
            if (participant != null)
                participant.Score = participant.Handicap - 1;
        }
        
        if (text == "letmeplay")
        {
            var participant = MatchController.Instance.Match?.GetCurrentParticipant();
            
            if (participant != null)
                participant.Actions += 200;
        }

        _inputField.text = "";
        _inputField.gameObject.SetActive(false);
    }
}