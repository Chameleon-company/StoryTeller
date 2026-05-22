using UnityEngine;
using Photon.Pun; 
using Photon.Realtime;


public class AvatarPanel : MonoBehaviourPunCallbacks
{
    

   
    public void OnAvatarSelected(int avatarIndex)
    {
        Debug.Log("Avatar Button clicked");
        PlayerData.SetAvatarIndex(avatarIndex);

        ExitGames.Client.Photon.Hashtable hash = new ExitGames.Client.Photon.Hashtable(); 
        hash["avatarIndex"] = avatarIndex; 
        Photon.Pun.PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
        
        if (!PhotonNetwork.InRoom)
        {
            RoomOptions options = new RoomOptions { MaxPlayers = 8 };
            PhotonNetwork.JoinOrCreateRoom("Room1", options, TypedLobby.Default);
        }
        

        
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
