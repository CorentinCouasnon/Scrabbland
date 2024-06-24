using System;
using System.Collections.Generic;
using Encounters;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EncounterUI : MonoBehaviour
{
    [SerializeField] List<Encounter> _encounters;
    [SerializeField] GameObject _option1Button;
    [SerializeField] GameObject _option2Button;
    [SerializeField] TextMeshProUGUI _option1Text;
    [SerializeField] TextMeshProUGUI _option2Text;
    [SerializeField] GameObject _continueButton;
    [SerializeField] Image _avatarImage;
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] TextMeshProUGUI _resultText;
    
    public Action Continued { get; set; }

    Encounter _encounter;
    
    void OnEnable()
    {
        _encounter = _encounters.GetRandom();

        _avatarImage.sprite = _encounter.Avatar;
        _text.text = _encounter.Text;
        _option1Text.text = _encounter.Option1Text;
        _option2Text.text = _encounter.Option2Text;
        
        _option1Button.SetActive(true);
        _option2Button.SetActive(true);
        _continueButton.SetActive(false);
        _resultText.gameObject.SetActive(false);
    }

    public void Accepted()
    {
        _resultText.text = _encounter.Accept();
        _resultText.gameObject.SetActive(true);
        _continueButton.SetActive(true);
        
        _option1Button.SetActive(false);
        _option2Button.SetActive(false);
    }
    
    public void Rejected()
    {
        _resultText.text = _encounter.Reject();
        _resultText.gameObject.SetActive(true);
        _continueButton.SetActive(true);

        _option1Button.SetActive(false);
        _option2Button.SetActive(false);
    }
    
    public void Continue()
    {
        Continued?.Invoke();
    }
}