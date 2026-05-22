using UnityEngine;
using TMPro;

public class TextSizeToggle : MonoBehaviour
{
    public TMP_Text statusLabel;
    public TMP_Text[] textsToResize;

    private int currentIndex = 0;

    private float[] sizes = { 20f, 28f, 36f };
    private string[] labels = { "Text size: Small", "Text size: Medium", "Text size: Large" };

    void Start()
    {
        UpdateTextSize();
    }

    public void ToggleTextSize()
    {
        currentIndex = (currentIndex + 1) % 3;
        UpdateTextSize();
    }

    void UpdateTextSize()
    {
        float size = sizes[currentIndex];

        statusLabel.text = labels[currentIndex];

        foreach (TMP_Text txt in textsToResize)
        {
            if (txt != null)
            {
                txt.fontSize = size;
            }
        }
    }
}
