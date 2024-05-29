using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipManager : MonoBehaviour
{
    [SerializeField] RectTransform _tooltip;
    [SerializeField] TextMeshProUGUI _tooltipText;

    void Update()
    {
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            _tooltip.gameObject.SetActive(false);
            return;
        }

        EventSystem.current.currentSelectedGameObject.TryGetComponent<Tooltip>(out var tooltip);

        if (tooltip == null)
        {
            _tooltip.gameObject.SetActive(false);
            return;
        }
        
        _tooltip.position = tooltip.transform.position;
        _tooltipText.text = tooltip.GetMessage();
        _tooltip.gameObject.SetActive(true);
    }
}