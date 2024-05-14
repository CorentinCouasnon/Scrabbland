using AdventureFSM;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _container;

    void Awake()
    {
        Show();
    }

    public void Show()
    {
        _container.SetActive(true);
    }
    
    public void Hide()
    {
        _container.SetActive(false);
    }
    
    public void NewGame()
    {
        AdventureController.Instance.State = new AdventureSelectionState();
        Hide();
    }

    public void Quit()
    {
        Application.Quit();
    }
}