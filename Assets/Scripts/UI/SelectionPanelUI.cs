using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectionPanelUI : MonoBehaviour
{
    [SerializeField] Image _background;
    [SerializeField] GameObject _container;
    [SerializeField] TextMeshProUGUI _titleText;
    [SerializeField] Transform _buttonContainer;
    [SerializeField] GameObject _okButton;
    [SerializeField] SelectionPanelButtonUI _buttonPrefab;
    
    public static SelectionPanelUI Instance { get; set; }

    bool _okButtonFlag;
    
    void Awake()
    {
        Instance = this;
    }

    public IEnumerator Open(string title, List<Sprite> options, Action<int> callback)
    {
        Clear();
        _titleText.text = title;
        Show();

        var clickedIndex = -1;

        for (var i = 0; i < options.Count; i++)
        {
            var button = Instantiate(_buttonPrefab, _buttonContainer);
            var index = i;
            button.Initialize(() => clickedIndex = index, options[i]);
        }
        
        yield return new WaitUntil(() => clickedIndex != -1);
        Hide();
        callback?.Invoke(clickedIndex);
    }
    
    public IEnumerator Open(string title, List<Sprite> options)
    {
        Clear();
        _titleText.text = title;
        _okButton.SetActive(true);
        Show();

        _okButtonFlag = false;

        foreach (var option in options)
        {
            var button = Instantiate(_buttonPrefab, _buttonContainer);
            button.Initialize(null, option);
        }

        yield return new WaitUntil(() => _okButtonFlag);
        
        Hide();
    }
    
    public void Ok()
    {
        _okButtonFlag = true;
    }

    void Show()
    {
        _container.SetActive(true);
        _background.raycastTarget = true;
    }

    void Hide()
    {
        _container.SetActive(false);
        _background.raycastTarget = false;
    }

    void Clear()
    {
        _titleText.text = string.Empty;
        _okButton.SetActive(false);
        
        foreach (Transform child in _buttonContainer)
            Destroy(child.gameObject);
    }
}