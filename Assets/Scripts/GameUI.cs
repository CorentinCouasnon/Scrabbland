using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject _topContainer;

    public void Show()
    {
        _topContainer.SetActive(true);
    }
    
    public void Hide()
    {
        _topContainer.SetActive(false);
    }
}