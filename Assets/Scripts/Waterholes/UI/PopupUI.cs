using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupUI : MonoBehaviour
{
    public static PopupUI I;

    [Header("Panel + Widgets")]
    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text bodyText;
    public Button playAudioButton;
    public Button learnMoreButton;
    public Button closeButton;
    public AudioSource audioSource;
    public Image infoImageUI;
    public AudioClip closeClickSound;

    string currentUrl;

    void Awake()
    {
        // Singleton (simple version)
        if (I != null && I != this)
        {
            Debug.LogWarning("Duplicate PopupUI detected. Destroying this instance.");
            Destroy(gameObject);
            return;
        }
        I = this;

        if (panel) panel.SetActive(false);

        // Clear any pre-existing listeners (e.g., if added in the Inspector)
        if (closeButton) closeButton.onClick.RemoveAllListeners();
        if (playAudioButton) playAudioButton.onClick.RemoveAllListeners();
        if (learnMoreButton) learnMoreButton.onClick.RemoveAllListeners();

        // Wire up listeners
        if (closeButton) closeButton.onClick.AddListener(OnCloseButtonClicked);
        if (playAudioButton) playAudioButton.onClick.AddListener(() =>
        {
            if (audioSource && audioSource.clip) audioSource.Play();
        });
        if (learnMoreButton) learnMoreButton.onClick.AddListener(() =>
        {
            if (!string.IsNullOrEmpty(currentUrl)) Application.OpenURL(currentUrl);
        });
    }

    void OnDestroy()
    {
        // Clean-up listeners to avoid leaks if this object is destroyed
        if (closeButton) closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        if (playAudioButton) playAudioButton.onClick.RemoveAllListeners();
        if (learnMoreButton) learnMoreButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// Show the popup and populate fields.
    /// </summary>
    public void Show(string title, string body, string url, AudioClip clip, Sprite image = null)
    {
        if (!panel) return;

        // Stop any currently playing audio before switching
        if (audioSource && audioSource.isPlaying) audioSource.Stop();

        if (titleText) titleText.text = title ?? "";
        if (bodyText) bodyText.text = body ?? "";
        currentUrl = url ?? "";

        // Audio
        if (audioSource)
        {
            audioSource.clip = clip;
        }
        if (playAudioButton)
        {
            playAudioButton.gameObject.SetActive(clip != null);
            playAudioButton.interactable = (clip != null);
        }

        // Link
        if (learnMoreButton)
        {
            bool hasUrl = !string.IsNullOrEmpty(currentUrl);
            learnMoreButton.gameObject.SetActive(hasUrl);
            learnMoreButton.interactable = hasUrl;
        }

        // Image + (optional) container
        if (infoImageUI)
        {
            if (image)
            {
                infoImageUI.sprite = image;
                infoImageUI.gameObject.SetActive(true);
                if (infoImageUI.transform.parent) infoImageUI.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                infoImageUI.gameObject.SetActive(false);
                if (infoImageUI.transform.parent) infoImageUI.transform.parent.gameObject.SetActive(false);
            }
        }

        panel.SetActive(true);
    }

    private void OnCloseButtonClicked()
    {
        // Play close SFX in world so it doesn't get cut off if panel/audioSource gets disabled
        if (closeClickSound)
        {
            Vector3 at = Camera.main ? Camera.main.transform.position : Vector3.zero;
            AudioSource.PlayClipAtPoint(closeClickSound, at);
        }

        // Hide the panel
        if (panel) panel.SetActive(false);
    }
}

/// <summary>
/// PopupUI is a reusable manager for displaying popup panels in the Storyteller system.  
/// It provides a title, body text, optional image, optional audio playback, and a 
/// "Learn More" button that can open a provided URL.  
///
/// Features:
/// - **Singleton**: Ensures only one PopupUI instance is active at a time.  
/// - **Content Binding**: Dynamically sets title, body text, audio clip, image, and link.  
/// - **Buttons**: 
///     - Close → hides the panel and plays an optional close sound.  
///     - Play Audio → plays an attached AudioClip (if provided).  
///     - Learn More → opens a URL in the browser (if provided).  
/// - **Flexible UI**: If no audio, URL, or image is passed, the corresponding widgets are hidden.  
///
/// Usage:
/// - Attach this script to a UI Canvas GameObject in the scene.  
/// - Hook up references for panel, text fields, buttons, AudioSource, and Image in the Inspector.  
/// - Call `PopupUI.I.Show(title, body, url, audioClip, optionalSprite)` to display a popup.  
/// </summary>
