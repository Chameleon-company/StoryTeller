using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatMessageItem : MonoBehaviour
{
    public TMP_Text messageText;
    public Image avatarImage;

    public void Setup(string sender, string message, Sprite avatar)
    {
        messageText.text = $"<color=#ffac57>[{sender}]</color> :- {message}";

        if (avatar != null)
        {
            avatarImage.sprite = avatar;
        }
    }
}