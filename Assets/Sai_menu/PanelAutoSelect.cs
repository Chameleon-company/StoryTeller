using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PanelAutoSelect : MonoBehaviour
{
    public Selectable firstSelectable;

    void OnEnable()
    {
        // Clear current selection
        EventSystem.current.SetSelectedGameObject(null);
        // Select the first UI element for this panel
        if (firstSelectable != null)
        {
            EventSystem.current.SetSelectedGameObject(firstSelectable.gameObject);
        }
    }
}
