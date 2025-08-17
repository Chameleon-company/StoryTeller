using UnityEngine;
using UnityEngine.UI;

public class WaterholeProgress : MonoBehaviour
{
    public Image waterholeOutline;    
    public Color visitedColor = Color.green;
    private bool visited = false;

    
    public static int visitedCount = 0;
    public static int totalWaterholes = 0;
    public Text progressText;

    void Start()
    {
        
        totalWaterholes++;
        UpdateProgressUI();
    }

    public void OnClick()
    {
        if (!visited)
        {
            visited = true;
            waterholeOutline.color = visitedColor;
            visitedCount++;
            UpdateProgressUI();
        }
    }

    void UpdateProgressUI()
    {
        if (progressText != null)
        {
            progressText.text = visitedCount + " / " + totalWaterholes + " explored";
        }
    }
}
