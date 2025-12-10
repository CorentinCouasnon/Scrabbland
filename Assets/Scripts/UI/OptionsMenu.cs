using AdventureFSM;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public void Resume()
    {
        gameObject.SetActive(false);            
    }

    public void Quit()
    {
        AdventureController.Instance.State = new QuittingState();
    }
}