using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.Collections;

public class JoinRoomUI : MonoBehaviourPunCallbacks
{
    [Header("UI References")]
    public GameObject joinRoomPanel;
    public TMP_InputField roomCodeInput;
    public TMP_Text feedbackText;

    [Header("Scene To Load After Join")]
    public string lobbySceneName = "LobbyScene";

    [Header("Feedback Settings")]
    public float visibleDuration = 1.5f;
    public float fadeDuration = 0.5f;

    private Coroutine feedbackCoroutine;
    private CanvasGroup feedbackCanvasGroup;

    void Start()
    {
        feedbackCanvasGroup = feedbackText.GetComponent<CanvasGroup>();

        if (feedbackCanvasGroup == null)
        {
            feedbackCanvasGroup = feedbackText.gameObject.AddComponent<CanvasGroup>();
        }

        feedbackCanvasGroup.alpha = 0f;
        feedbackText.text = "";
    }

    public void OpenPanel()
    {
        joinRoomPanel.SetActive(true);
        roomCodeInput.text = "";
        ClearFeedback();
        roomCodeInput.ActivateInputField();
    }

    public void ClosePanel()
    {
        joinRoomPanel.SetActive(false);
        roomCodeInput.text = "";
        ClearFeedback();
    }

    public void OnJoinRoomClicked()
    {
        string input = roomCodeInput.text.Trim();
        string roomCode = input;

        if (input.Contains("code:"))
        {
            int index = input.IndexOf("code:") + 5;
            roomCode = input.Substring(index).Trim();
            roomCodeInput.text = roomCode;
        }

        if (string.IsNullOrEmpty(roomCode))
        {
            ShowFeedback("Please enter a room code.");
            return;
        }

        if (!PhotonNetwork.IsConnectedAndReady)
        {
            ShowFeedback("Not connected to Photon.");
            Debug.LogWarning("Join failed: Photon is not connected and ready.");
            return;
        }

        ShowFeedback("Joining room...");
        Debug.Log("Trying to join room: " + roomCode);
        PhotonNetwork.JoinRoom(roomCode);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
        SceneManager.LoadScene(lobbySceneName);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        ShowFeedback("Room not found or unavailable.");
        Debug.LogWarning($"Join room failed: {returnCode} - {message}");
    }

    void ShowFeedback(string message)
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        feedbackCoroutine = StartCoroutine(FeedbackRoutine(message, visibleDuration, fadeDuration));
    }

    IEnumerator FeedbackRoutine(string message, float visibleTime, float fadeTime)
    {
        feedbackText.text = message;
        feedbackCanvasGroup.alpha = 1f;

        yield return new WaitForSeconds(visibleTime);

        float elapsed = 0f;

        while (elapsed < fadeTime)
        {
            elapsed += Time.deltaTime;
            feedbackCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeTime);
            yield return null;
        }

        feedbackCanvasGroup.alpha = 0f;
        feedbackText.text = "";
        feedbackCoroutine = null;
    }

    void ClearFeedback()
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
            feedbackCoroutine = null;
        }

        feedbackText.text = "";
        if (feedbackCanvasGroup != null)
        {
            feedbackCanvasGroup.alpha = 0f;
        }
    }
}