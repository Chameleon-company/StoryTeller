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

    void Start()
    {
        sendButton.onClick.AddListener(OnSendButtonClicked);
    }

    void OnDestroy()
    {
        sendButton.onClick.RemoveListener(OnSendButtonClicked);
    }

    void OnSendButtonClicked()
    {
        SendChatMessage();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SendChatMessage();
        }
    }

    void SendChatMessage()
    {
        if (chatInputField == null)
        {
            Debug.LogError("ChatInputField is NULL!");
            return;
        }

        string text = chatInputField.text;

        if (string.IsNullOrEmpty(text))
            return;

        string senderName = string.IsNullOrEmpty(PhotonNetwork.NickName)
            ? "Unknown"
            : PhotonNetwork.NickName;

        int avatarIndex = 0;

        if (AvatarManager.Instance != null)
        {
            avatarIndex = AvatarManager.Instance.GetAvatarIndex();
        }
        else
        {
            Debug.LogWarning("AvatarManager not found!");
        }

        object[] data = new object[] { senderName, text, avatarIndex };

        chatInputField.text = "";

        RaiseEventOptions options = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All
        };

        SendOptions sendOptions = new SendOptions
        {
            Reliability = true
        };

        PhotonNetwork.RaiseEvent(ChatEventCode, data, options, sendOptions);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == ChatEventCode)
        {
            object[] receivedData = (object[])photonEvent.CustomData;

            string senderName = (string)receivedData[0];
            string messageText = (string)receivedData[1];
            int avatarIndex = (int)receivedData[2];

            AddMessageToUI(senderName, messageText, avatarIndex);
        }
    }

    void AddMessageToUI(string senderName, string messageText, int avatarIndex)
    {
        GameObject item = Instantiate(messageItemPrefab, messagesContainer);

        // Set text
        TMP_Text textComponent = item.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = senderName + ": " + messageText;
        }

        // Set avatar image
        Transform avatarTransform = item.transform.Find("AvatarImage");

        if (avatarTransform != null)
        {
            Image avatarImg = avatarTransform.GetComponent<Image>();
            avatarImg.sprite = AvatarDatabase.Instance.GetAvatar(avatarIndex);
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
    }
}
