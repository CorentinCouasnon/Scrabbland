using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject _topContainer;
    [SerializeField] GameObject _optionsMenu;

    public void Show()
    {
        _topContainer.SetActive(true);
    }

    public void OpenOptions()
    {
        _optionsMenu.SetActive(true);
    }
    
    public void Hide()
    {
        _topContainer.SetActive(false);
        _optionsMenu.SetActive(false);
    }
}