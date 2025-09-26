using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour {
    public static TooltipUI I;

    public GameObject panel;
    public TMP_Text tooltipText;

    void Awake() {
        I = this;
        panel.SetActive(false);
    }

    public void Show(string message, Vector3 position) {
        tooltipText.text = message;
        panel.SetActive(true);

        // convert world pos to screen pos
        Vector3 screenPos = Camera.main.WorldToScreenPoint(position);
        panel.transform.position = screenPos;
    }

    public void Hide() {
        panel.SetActive(false);
    }
}
/// <summary>
/// TooltipUI provides a simple on-screen tooltip system.  
/// It shows short text messages near objects in the scene when hovered or interacted with.  
///
/// Features:
/// - **Singleton**: Exposes a global instance `TooltipUI.I` for easy access.  
/// - **Dynamic Text**: Updates tooltip content with any message string.  
/// - **World → Screen Conversion**: Positions the tooltip panel above world-space objects 
///   by converting their 3D position to screen space.  
/// - **Show / Hide**: Easy methods to toggle the tooltip on and off.  
///
/// Usage:
/// - Attach this script to a Canvas-based UI element containing a panel and a TMP text field.  
/// - Assign the `panel` and `tooltipText` references in the Inspector.  
/// - Call `TooltipUI.I.Show("Message", worldPosition)` to display a tooltip at a given location.  
/// - Call `TooltipUI.I.Hide()` to remove the tooltip from view.  
/// </summary>
