using TMPro;
using UnityEngine;

public class WaterholeTooltipManager : MonoBehaviour
{
    public static WaterholeTooltipManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject tooltipPanel;
    [SerializeField] private TMP_Text tooltipText;

    [Header("World Position Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Vector3 worldOffset = new Vector3(0f, 1.2f, 0f);

    private RectTransform tooltipRectTransform;

    private void Awake()
    {
        Instance = this;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (tooltipPanel != null)
        {
            tooltipRectTransform = tooltipPanel.GetComponent<RectTransform>();

            if (tooltipText == null)
            {
                tooltipText = tooltipPanel.GetComponentInChildren<TMP_Text>(true);
            }

            tooltipPanel.SetActive(false);
        }
    }

    public void ShowTooltip(string waterholeName, string description, Vector3 worldPosition)
    {
        if (tooltipPanel == null)
        {
            Debug.LogWarning("Tooltip Panel is missing.");
            return;
        }

        if (tooltipText == null)
        {
            tooltipText = tooltipPanel.GetComponentInChildren<TMP_Text>(true);
        }

        if (tooltipText == null)
        {
            Debug.LogWarning("Tooltip Text is missing.");
            return;
        }

        if (tooltipRectTransform == null)
        {
            tooltipRectTransform = tooltipPanel.GetComponent<RectTransform>();
        }

        tooltipText.text = "<align=\"center\"><size=120%><b>" + waterholeName + "</b></size>\n<size=95%>" + description + "</size></align>";

        Vector3 screenPosition = mainCamera.WorldToScreenPoint(worldPosition + worldOffset);
        tooltipRectTransform.position = screenPosition;

        tooltipPanel.SetActive(true);
    }

    public void HideTooltip()
    {
        if (tooltipPanel != null)
        {
            tooltipPanel.SetActive(false);
        }
    }
}
