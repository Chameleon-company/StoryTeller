using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Name of the next scene to load when Play is clicked
    [SerializeField] private string lobbySceneName = "Lobby";

    // Reference to the instructions panel UI (assign in Inspector later)
    [SerializeField] private GameObject instructionsPanel;

    private void Awake()
    {
        // Hide instructions at start if it exists
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }

    // Called by Play button
    public void OnPlayClicked()
    {
        Debug.Log("[MainMenu] Play clicked -> Loading Lobby");
        SceneManager.LoadScene(lobbySceneName);
    }

    // Called by How To Play button
    public void OnHowToPlayClicked()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("[MainMenu] Instructions panel not assigned yet.");
        }
    }

    // Called by Close button on instructions panel
    public void OnCloseInstructions()
    {
        if (instructionsPanel != null)
        {
            instructionsPanel.SetActive(false);
        }
    }

    // Called by Quit button
    public void OnQuitClicked()
    {
        Debug.Log("[MainMenu] Quit clicked");
    #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
    #else
        Application.Quit();
    #endif
    }
}
