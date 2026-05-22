using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using TMPro;

public class LobbyDisplay : MonoBehaviourPunCallbacks
{
    public Transform container;
    public GameObject playerSlotPrefab;

     void OnEnable()
    {
        StartCoroutine(WaitForDatabase());
    }

    System.Collections.IEnumerator WaitForDatabase()
    {
        while (AvatarDatabase.Instance == null)
        {
            yield return null;
        }

        RefreshPlayerList();
    }

    void RefreshPlayerList()
    {
        // Clear old slots
        for (int i = 0; i < container.childCount; i++)
        {
            Transform child = container.GetChild(i);
            Destroy(child.gameObject);
        }

        // Create new slots
        Player[] players = PhotonNetwork.PlayerList;

        for (int i = 0; i < players.Length; i++)
        {
            CreateSlot(players[i]);
        }
    }

    void CreateSlot(Player player)
    {
        GameObject slot = Instantiate(playerSlotPrefab, container);

        
        Transform nameTransform = slot.transform.Find("NameText");
        if (nameTransform == null)
        {
            Debug.LogError("NameText object not found in PlayerSlot prefab");
            return;
        }

        TMP_Text nameText = nameTransform.GetComponent<TMP_Text>();
        if (nameText == null)
        {
            Debug.LogError("TMP_Text component missing on NameText");
            return;
        }

        string playerName = "Unknown";

        if (player.CustomProperties.ContainsKey("playerName"))
        {
            object nameObj = player.CustomProperties["playerName"];
            if (nameObj != null)
            {
                playerName = nameObj.ToString();
            }
        }

        nameText.text = playerName;

        
        Transform avatarTransform = slot.transform.Find("AvatarImage");
        if (avatarTransform == null)
        {
            Debug.LogError("AvatarImage object not found in PlayerSlot prefab");
            return;
        }

        Image avatarImage = avatarTransform.GetComponent<Image>();
        if (avatarImage == null)
        {
            Debug.LogError("Image component missing on AvatarImage");
            return;
        }

        int avatarIndex = 0;

        if (player.CustomProperties.ContainsKey("avatarIndex"))
        {
            object indexObj = player.CustomProperties["avatarIndex"];
            if (indexObj != null)
            {
                avatarIndex = (int)indexObj;
            }
        }

        if (AvatarDatabase.Instance == null)
        {
            Debug.LogError("AvatarDatabase.Instance is NULL");
            return;
        }

        Sprite avatarSprite = AvatarDatabase.Instance.GetAvatar(avatarIndex);
        if (avatarSprite == null)
        {
            Debug.LogError("Avatar sprite is NULL for index: " + avatarIndex);
            return;
        }

        avatarImage.sprite = avatarSprite;
    }

       


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        RefreshPlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RefreshPlayerList();
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        RefreshPlayerList();
    }
}
