using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupUI : MonoBehaviour {
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

    void Awake() {
        I = this;
        panel.SetActive(false);

        closeButton.onClick.AddListener(()=> {
        if (audioSource != null && closeClickSound != null) {
            audioSource.PlayOneShot(closeClickSound);
        }
        panel.SetActive(false);
    });

        playAudioButton.onClick.AddListener(()=> { if(audioSource) audioSource.Play(); });
        learnMoreButton.onClick.AddListener(()=> { if(!string.IsNullOrEmpty(currentUrl)) Application.OpenURL(currentUrl); });
    }


    // Added a Sprite parameter for the image
    public void Show(string title, string body, string url, AudioClip clip, Sprite image = null)
    {
    titleText.text = title;
    bodyText.text = body;
    currentUrl = url;

    // assign audio
    if (audioSource != null)
    {
        audioSource.clip = clip;  
        playAudioButton.gameObject.SetActive(clip != null); // only show if there’s audio
    }

    // link
    learnMoreButton.gameObject.SetActive(!string.IsNullOrEmpty(url));

    // image + container
    if (infoImageUI != null)
    {
        if (image != null)
        {
            infoImageUI.sprite = image;
            infoImageUI.gameObject.SetActive(true);
            infoImageUI.transform.parent.gameObject.SetActive(true); // LEFT container ON
        }
        else
        {
            infoImageUI.gameObject.SetActive(false);
            infoImageUI.transform.parent.gameObject.SetActive(false); // LEFT container OFF
        }
    }

    panel.SetActive(true);
}

}
