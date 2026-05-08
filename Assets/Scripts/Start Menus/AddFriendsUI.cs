using UnityEngine;
using TMPro;
using System.Collections;

public class AddFriendsUI : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_Text feedbackText;

    public float fadeDuration = 0.4f;
    public float visibleDuration = 1.8f;

    private Coroutine feedbackCoroutine;

    private void Start()
    {
        if (feedbackText != null)
        {
            feedbackText.text = "";
            SetTextAlpha(0f);
        }
    }

    public void OnAddClicked()
    {
        string username = usernameInput.text.Trim();

        if (string.IsNullOrEmpty(username))
        {
            ShowFeedback("Please enter a username");
            return;
        }

        ShowFeedback("Friend requests coming soon");
    }

    public void OnBackClicked()
    {
        if (usernameInput != null)
            usernameInput.text = "";

        if (feedbackText != null)
        {
            if (feedbackCoroutine != null)
                StopCoroutine(feedbackCoroutine);

            feedbackText.text = "";
            SetTextAlpha(0f);
        }

        gameObject.SetActive(false);
    }

    private void ShowFeedback(string message)
    {
        if (feedbackText == null)
            return;

        if (feedbackCoroutine != null)
            StopCoroutine(feedbackCoroutine);

        feedbackCoroutine = StartCoroutine(FadeFeedback(message));
    }

    private IEnumerator FadeFeedback(string message)
    {
        feedbackText.text = message;
        SetTextAlpha(0f);

        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(0f, 1f, t / fadeDuration);
            SetTextAlpha(alpha);
            yield return null;
        }

        SetTextAlpha(1f);

        yield return new WaitForSeconds(visibleDuration);

        t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            SetTextAlpha(alpha);
            yield return null;
        }

        SetTextAlpha(0f);
        feedbackText.text = "";
        feedbackCoroutine = null;
    }

    private void SetTextAlpha(float alpha)
    {
        Color color = feedbackText.color;
        color.a = alpha;
        feedbackText.color = color;
    }
}