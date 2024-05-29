using UnityEngine;

public class Tooltip : MonoBehaviour
{
    [SerializeField] string _message;
    
    public virtual string GetMessage()
    {
        return _message;
    }
}