using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FriendItemUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text statusText;
    public Button inviteButton;
    public TMP_Text infoText;

    [SerializeField] private bool isOnline = true;
    [SerializeField] private float fadeDuration = 0.25f;
    [SerializeField] private float messageDuration = 2f;

    private Coroutine messageCoroutine;

    void Start()
    {
        UpdateUI();
        inviteButton.onClick.AddListener(OnInviteClicked);
    }

    void UpdateUI()
    {
        if (isOnline)
        {
            statusText.text = "Online";
            statusText.color = new Color32(122, 140, 90, 255); // #7A8C5A
            inviteButton.interactable = true;
        }
        else
        {
            statusText.text = "Offline";
            statusText.color = new Color32(140, 120, 120, 255);
            inviteButton.interactable = false;
        }
    }

    void OnInviteClicked()
    {
        if (infoText != null)
        {
            if (messageCoroutine != null)
            {
                StopCoroutine(messageCoroutine);
            }

            messageCoroutine = StartCoroutine(ShowTemporaryMessage());
        }
    }

    IEnumerator ShowTemporaryMessage()
    {
        yield return StartCoroutine(FadeText(1f, 0f));

        infoText.text = "Invites will be available soon";

        yield return StartCoroutine(FadeText(0f, 1f));

        yield return new WaitForSeconds(messageDuration);

        yield return StartCoroutine(FadeText(1f, 0f));

        infoText.text = "Friends feature coming soon — stay tuned!";

        yield return StartCoroutine(FadeText(0f, 1f));

        messageCoroutine = null;
    }

    IEnumerator FadeText(float startAlpha, float endAlpha)
    {
        Color textColor = infoText.color;
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / fadeDuration;

            textColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
            infoText.color = textColor;

            yield return null;
        }

        textColor.a = endAlpha;
        infoText.color = textColor;
    }
}