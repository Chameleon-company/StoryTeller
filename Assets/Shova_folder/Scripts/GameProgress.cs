using UnityEngine;

public class GameProgress : MonoBehaviour
{
    public int unlockedWaterholes = 0;
    public string lastScene = "MainMenu";

    void Start()
    {
        // Auto-load when game starts
        LoadProgress();
    }

    public void SaveProgress()
    {
        // Basic sanity check
        if (unlockedWaterholes < 0) unlockedWaterholes = 0;

        PlayerPrefs.SetInt("UnlockedWaterholes", unlockedWaterholes);
        PlayerPrefs.SetString("LastScene", lastScene);
        PlayerPrefs.Save();

        Debug.Log("Game progress saved!");
    }

    public void LoadProgress()
    {
        if (PlayerPrefs.HasKey("UnlockedWaterholes"))
        {
            unlockedWaterholes = PlayerPrefs.GetInt("UnlockedWaterholes");
            lastScene = PlayerPrefs.GetString("LastScene");

            Debug.Log("Game progress loaded!");
            Debug.Log("Unlocked Waterholes: " + unlockedWaterholes);
            Debug.Log("Last Scene: " + lastScene);
        }
        else
        {
            Debug.Log("No saved data found. Starting new game.");
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        unlockedWaterholes = 0;
        lastScene = "MainMenu";
        Debug.Log("Game progress reset!");
    }

    void Update()
    {
        // Optional: press R to reset during testing
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetProgress();
        }
    }
}
