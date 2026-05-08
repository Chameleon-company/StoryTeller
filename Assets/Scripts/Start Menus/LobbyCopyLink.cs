using UnityEngine;
using Photon.Pun;
using TMPro;

public class LobbyCopyLink : MonoBehaviour
{
    [Header("Optional UI feedback")]
    public TMP_Text statusText;

    [Header("Optional")]
    public float clearMessageAfter = 2f;

    public void CopyGameLink()
    {
        // Make sure player is actually inside a room
        if (!PhotonNetwork.InRoom || PhotonNetwork.CurrentRoom == null)
        {
            ShowStatus("No room joined yet!");
            Debug.Log("CopyGameLink: Player is not in a Photon room.");
            return;
        }

        string roomCode = PhotonNetwork.CurrentRoom.Name;

        // This is the text that gets copied
        string shareText = "Join my StoryTeller game with code: " + roomCode;

        GUIUtility.systemCopyBuffer = shareText;

        ShowStatus("Room code copied!");
        Debug.Log("Copied to clipboard: " + shareText);
    }

    private void ShowStatus(string message)
    {
        if (statusText != null)
        {
            statusText.text = message;
            CancelInvoke(nameof(ClearStatus));
            Invoke(nameof(ClearStatus), clearMessageAfter);
        }
    }

    private void ClearStatus()
    {
        if (statusText != null)
        {
            statusText.text = "";
        }
    }
}