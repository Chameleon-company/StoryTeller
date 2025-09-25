using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class WaterholeProgress : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image waterholeOutline;
    public Color baseColor = new Color32(189,189,189,255);
    public Color hoverColor = new Color32(144,202,249,255);
    public Color visitedColor = Color.red;
    private bool visited = false;

    public static int visitedCount = 0;
    public static int totalWaterholes = 0;
    public Text progressText;

    public GameObject infoPanel;
    public Text infoText;
    public string infoContent = "Story preview";

    void Start()
    {
        totalWaterholes++;
        if (waterholeOutline != null) waterholeOutline.color = baseColor;
        if (infoPanel != null) infoPanel.SetActive(false);
        UpdateProgressUI();
    }

    public void OnClick()
    {
        if (visited) return;
        visited = true;
        if (waterholeOutline != null) waterholeOutline.color = visitedColor;
        visitedCount++;
        UpdateProgressUI();
        if (infoPanel != null) infoPanel.SetActive(true);
        if (infoText != null) infoText.text = infoContent;
    }

    public void CloseInfoPanel()
    {
        if (infoPanel != null) infoPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData e)
    {
        if (!visited && waterholeOutline != null) waterholeOutline.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData e)
    {
        if (!visited && waterholeOutline != null) waterholeOutline.color = baseColor;
    }

    void UpdateProgressUI()
    {
        if (progressText != null) progressText.text = visitedCount + " / " + totalWaterholes + " explored";
    }
}
