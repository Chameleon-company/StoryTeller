using UnityEngine;

public class WaterholeHoverTarget : MonoBehaviour
{
    [Header("Waterhole Information")]
    [SerializeField] private string waterholeName;

    [TextArea(3, 8)]
    [SerializeField] private string waterholeDescription;

    [Header("Optional Highlight")]
    [SerializeField] private Renderer waterholeRenderer;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color hoverColor = Color.yellow;

    private Material waterholeMaterial;

    private void Start()
    {
        if (waterholeRenderer == null)
        {
            waterholeRenderer = GetComponent<Renderer>();
        }

        if (waterholeRenderer != null)
        {
            waterholeMaterial = waterholeRenderer.material;
            waterholeMaterial.color = normalColor;
        }
    }

    public void HoverEnter()
    {
        if (WaterholeTooltipManager.Instance != null)
        {
            WaterholeTooltipManager.Instance.ShowTooltip(
                waterholeName,
                waterholeDescription,
                transform.position
            );
        }

        if (waterholeMaterial != null)
        {
            waterholeMaterial.color = hoverColor;
        }
    }

    public void HoverExit()
    {
        if (WaterholeTooltipManager.Instance != null)
        {
            WaterholeTooltipManager.Instance.HideTooltip();
        }

        if (waterholeMaterial != null)
        {
            waterholeMaterial.color = normalColor;
        }
    }
}
