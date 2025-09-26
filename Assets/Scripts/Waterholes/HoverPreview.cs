using UnityEngine;
using UnityEngine.EventSystems;

public class HoverPreview : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject hoverPanel;
    private bool isPointIn = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData Data)
    {
        isPointIn = true;
        hoverPanel.SetActive(true);
    }

      public void OnPointerExit(PointerEventData Data)
    {
        isPointIn = false;
        Invoke("HideHoverWindow", 0.1f);
    }

    public void HideHoverWindow()
    {
        if(!isPointIn && !HoverPanelCheck.isHoverin)
        {
            hoverPanel.SetActive(false);
        }
    }
}
