using UnityEngine;
using UnityEngine.UI;

public class MenuMusicController : MonoBehaviour
{
    public AudioSource audioSource;   // Reference to the Audio Source
    public Slider volumeSlider;       // Reference to the UI Slider

    void Start()
    {
        // Start playing music
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Set initial volume
        if (volumeSlider != null)
        {
            audioSource.volume = volumeSlider.value / 100f;

            // Add listener to handle slider changes
            volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });
        }
    }

    public void ChangeVolume()
    {
        if (audioSource != null && volumeSlider != null)
        {
            audioSource.volume = volumeSlider.value / 100f;
        }
    }
}
