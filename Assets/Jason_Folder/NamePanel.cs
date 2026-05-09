using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class NamePanel : MonoBehaviour
{
    public TMP_InputField nameInput;
    public GameObject avatarPanel;

    public void OnConfirmName()
    {
        string playerName = nameInput.text; 

        if (string.IsNullOrEmpty(playerName))
        {
            return; 

        } 

        
        PhotonNetwork.NickName = playerName;
        PlayerData.SetPlayerName(playerName); 

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable(); 
        hash["playerName"] = playerName; 
        Photon.Pun.PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        
        if (!PhotonNetwork.InRoom)
        {
            RoomOptions options = new RoomOptions { MaxPlayers = 8 };
            PhotonNetwork.JoinOrCreateRoom("Room1", options, TypedLobby.Default);
        }

        gameObject.SetActive(false); 
        avatarPanel.SetActive(true);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
