using UnityEngine;
using TMPro;  // Import TextMeshPro namespace

public class GameModeController : MonoBehaviour
{
    public TextMeshProUGUI gameModeText; // Use TMP instead of UI.Text
    private string currentMode = "Classic";

    // Called when button is clicked
    public void ToggleGameMode()
    {
        if (currentMode == "Classic")
        {
            currentMode = "Quickplay";
        }
        else
        {
            currentMode = "Classic";
        }

        // Update UI text
        gameModeText.text = "Game modes: " + currentMode;
    }
}
