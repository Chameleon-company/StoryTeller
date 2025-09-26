using UnityEngine;
using UnityEngine.EventSystems;

public class HoverPanelCheck : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static bool isHoverin = false;

    public void OnPointerEnter(PointerEventData Data)
    {
        isHoverin = true;
    }

    public void OnPointerExit(PointerEventData Data)
    {
        isHoverin = false;
        Invoke("HidePanel", 0.1f);
    }

    public void HidePanel()
    {
        if(!isHoverin)
        {
            HoverPreview preview = FindFirstObjectByType<HoverPreview>();
            if (preview != null && !preview.enabled)
            {
                return;
            }

            if(preview !=null)
            {
                preview.SendMessage("HideHoverWindow");
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
