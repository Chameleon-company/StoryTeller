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
