using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardPopup : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private RectTransform cardRect;
    [SerializeField] private GameObject cardBack;
    [SerializeField] private GameObject cardFront;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text bodyText;
    [SerializeField] private Image illustrationImage;
    [SerializeField] private GameObject illustrationContainer;
    [SerializeField] private Image dimmer;
    [SerializeField] private AudioSource audioSource;

    [Header("Timings")]
    [SerializeField] private float fadeDuration = 0.15f;
    [SerializeField] private float flipDuration = 0.35f;

    private bool isShowing;
    private MessageStickCard current;

    public void ShowCard(MessageStickCard data)
    {
        if (isShowing) return;
        isShowing = true;
        current = data;

        // Prep visuals
        cardBack.SetActive(true);
        cardFront.SetActive(false);
        titleText.text = "";
        bodyText.text = "";
        if (illustrationContainer) illustrationContainer.SetActive(false);
        cardRect.localRotation = Quaternion.identity;
        canvasGroup.alpha = 0f;
        gameObject.SetActive(true);
        if (dimmer) dimmer.raycastTarget = true;

        StartCoroutine(FadeInThenFlip());
    }

    public void Close()
    {
        if (!isShowing) return;
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeInThenFlip()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.SmoothStep(0f, 1f, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 1f;
        yield return StartCoroutine(FlipToFront());
    }

    private IEnumerator FlipToFront()
    {
        float half = flipDuration * 0.5f;
        float t = 0f;

        // 0 -> 90
        while (t < half)
        {
            t += Time.unscaledDeltaTime;
            float angle = Mathf.Lerp(0f, 90f, t / half);
            cardRect.localRotation = Quaternion.Euler(0f, angle, 0f);
            yield return null;
        }

        // Swap face
        cardBack.SetActive(false);
        cardFront.SetActive(true);

        titleText.text = current?.Title ?? "Message Stick";
        bodyText.text = current?.Body ?? "—";
        if (illustrationContainer)
        {
            bool hasArt = current && current.Illustration;
            illustrationContainer.SetActive(hasArt);
            if (hasArt) illustrationImage.sprite = current.Illustration;
        }

        if (current?.Narration && audioSource)
        {
            audioSource.Stop();
            audioSource.clip = current.Narration;
            audioSource.Play();
        }

        // 90 -> 180
        t = 0f;
        while (t < half)
        {
            t += Time.unscaledDeltaTime;
            float angle = Mathf.Lerp(90f, 180f, t / half);
            cardRect.localRotation = Quaternion.Euler(0f, angle, 0f);
            yield return null;
        }

        // Normalize
        cardRect.localRotation = Quaternion.Euler(0f, 0f, 0f);
    }

    private IEnumerator FadeOut()
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.SmoothStep(1f, 0f, t / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
        if (dimmer) dimmer.raycastTarget = false;
        isShowing = false;
        gameObject.SetActive(false);
    }
}