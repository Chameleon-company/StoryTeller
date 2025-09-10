using UnityEngine;
using TMPro;

/// <summary>
/// Handles showing and hiding the Instructions Panel in the UI.
/// This script automatically loads text (from a .txt file) and displays it in a TMP_Text field.
/// Attach this script to your Instructions Panel GameObject.
/// </summary>
public class InstructionsUI : MonoBehaviour
{
    [Header("Data")]
    [Tooltip("Plain text or basic rich text file that contains the game instructions. Drag a .txt file here.")]
    public TextAsset instructionsText;   // Holds the external text file with instructions/rules

    [Header("UI")]
    [Tooltip("Reference to the TextMeshPro text element that will display the instructions.")]
    public TMP_Text bodyText;            // Assign the TMP text UI element inside your Instructions Panel

    void Awake()
    {
        // Safety check: Ensure both the UI text component and text asset are assigned
        if (bodyText != null && instructionsText != null)
        {
            // Copy the contents of the .txt file into the UI text field at runtime
            bodyText.text = instructionsText.text;
        }

        // Hide this panel at the start of the game (to avoid cluttering the UI)
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Call this method to show the Instructions Panel.
    /// Typically connected to a button in the Start Menu (e.g., "How to Play").
    /// </summary>
    public void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// Call this method to hide the Instructions Panel.
    /// Typically connected to a "Close" button inside the panel.
    /// </summary>
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
