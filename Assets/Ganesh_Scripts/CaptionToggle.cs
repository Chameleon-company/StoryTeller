using UnityEngine;

public class CaptionToggle : MonoBehaviour
{
    public GameObject captionsPanel;
    public void Toggle() { captionsPanel.SetActive(!captionsPanel.activeSelf); }
}
