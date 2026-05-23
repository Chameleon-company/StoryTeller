using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;

public class ChatManager : MonoBehaviourPunCallbacks, IOnEventCallback
{
    public Transform messagesContainer;
    public GameObject messageItemPrefab;

    public TMP_InputField chatInputField;
    public Button sendButton;

    private const byte ChatEventCode = 1;

    private void Start()
    {
        sendButton.onClick.AddListener(SendChatMessage);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && chatInputField.isFocused)
        {
            SendChatMessage();
        }
    }

    void SendChatMessage()
    {
        string text = chatInputField.text.Trim();

        if (string.IsNullOrEmpty(text))
            return;

        object[] data = new object[]
        {
            PhotonNetwork.LocalPlayer.ActorNumber,
            PhotonNetwork.NickName,
            text
        };

        PhotonNetwork.RaiseEvent(
            ChatEventCode,
            data,
            new RaiseEventOptions { Receivers = ReceiverGroup.All },
            new SendOptions { Reliability = true }
        );

        chatInputField.text = "";
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code != ChatEventCode)
            return;

        object[] data = (object[])photonEvent.CustomData;

        int actorNumber = (int)data[0];
        string senderName = (string)data[1];
        string messageText = (string)data[2];

        Player sender = PhotonNetwork.CurrentRoom.GetPlayer(actorNumber);

        int avatarIndex = 0;

        if (sender != null)
        {
            if (sender.CustomProperties.TryGetValue("AvatarIndex", out object value))
            {
                avatarIndex = (int)value;
            }
        }

        Debug.Log($"MESSAGE FROM {senderName} | Actor={actorNumber} | Avatar={avatarIndex}");

        AddMessageToUI(senderName, messageText, avatarIndex);
    }

    void AddMessageToUI(string senderName, string messageText, int avatarIndex)
    {
        GameObject item = Instantiate(messageItemPrefab, messagesContainer);

        TMP_Text txt = item.GetComponentInChildren<TMP_Text>();

        if (txt != null)
        {
            txt.text = $"<color=#ffac57>[{senderName}]</color> :- {messageText}";
        }

        Image avatarImage = item.transform.Find("AvatarImage").GetComponent<Image>();

        if (avatarImage != null)
        {
            Sprite avatarSprite = AvatarDatabase.Instance.GetAvatar(avatarIndex);

            avatarImage.sprite = avatarSprite;

            Debug.Log($"SET AVATAR -> {senderName} -> {avatarIndex}");
        }
    }
}