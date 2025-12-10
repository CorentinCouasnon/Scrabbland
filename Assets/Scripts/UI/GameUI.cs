using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField] GameObject _topContainer;
    [SerializeField] GameObject _optionsMenu;
    [SerializeField] GameObject _mapContainer;
    [SerializeField] GameObject _shopContainer;
    [SerializeField] GameObject _matchContainer;
    [SerializeField] GameObject _encounterContainer;
    [SerializeField] GameObject _treasureContainer;

    public void Show()
    {
        _topContainer.SetActive(true);
    }

    public void OpenOptions()
    {
        _optionsMenu.SetActive(true);
    }
    
    public void OpenMap()
    {
        _mapContainer.SetActive(true);
    }
    
    public void OpenShop()
    {
        _shopContainer.SetActive(true);
    }
    
    public void OpenMatch()
    {
        _matchContainer.SetActive(true);
    }
    
    public void OpenEncounter()
    {
        _encounterContainer.SetActive(true);
    }
    
    public void OpenTreasure()
    {
        _treasureContainer.SetActive(true);
    }
    
    public void HideMap()
    {
        _mapContainer.SetActive(false);
    }
    
    public void HideShop()
    {
        _shopContainer.SetActive(false);
    }
    
    public void HideMatch()
    {
        _matchContainer.SetActive(false);
    }
    
    public void HideEncounter()
    {
        _encounterContainer.SetActive(false);
    }
    
    public void HideTreasure()
    {
        _treasureContainer.SetActive(false);
    }
    
    public void Hide()
    {
        _topContainer.SetActive(false);
        _optionsMenu.SetActive(false);
        _mapContainer.SetActive(false);
        _shopContainer.SetActive(false);
        _matchContainer.SetActive(false);
        _encounterContainer.SetActive(false);
        _treasureContainer.SetActive(false);
    }
}