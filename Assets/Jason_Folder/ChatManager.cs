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
        string text = chatInputField.text;

        if (string.IsNullOrEmpty(text))
        {
            return;
        }

        
        string senderName = "Unknown";

        if (!string.IsNullOrEmpty(PhotonNetwork.NickName))
        {
            senderName = PhotonNetwork.NickName;
        }

        string fullMessage = senderName + ": " + text;

        
        chatInputField.text = "";

        // Send to everyone in the room
        RaiseEventOptions options = new RaiseEventOptions();
        options.Receivers = ReceiverGroup.All;

        SendOptions sendOptions = new SendOptions();
        sendOptions.Reliability = true;

        PhotonNetwork.RaiseEvent(ChatEventCode, fullMessage, options, sendOptions);
    }

    public void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == ChatEventCode)
        {
            object data = photonEvent.CustomData;
            string messageText = data as string;

            if (messageText != null)
            {
                AddMessageToUI(messageText);
            }
        }
    }

    void AddMessageToUI(string messageText)
    {
        GameObject item = Instantiate(messageItemPrefab, messagesContainer);

        TMP_Text textComponent = item.GetComponentInChildren<TMP_Text>();
        if (textComponent != null)
        {
            textComponent.text = messageText;
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
