using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

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
