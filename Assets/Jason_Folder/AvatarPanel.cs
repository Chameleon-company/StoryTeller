using UnityEngine;
using Photon.Pun; 
using Photon.Realtime;


public class AvatarPanel : MonoBehaviourPunCallbacks
{
    

   
    public void OnAvatarSelected(int avatarIndex)
    {
        if (PhotonNetwork.InRoom)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                if (player != PhotonNetwork.LocalPlayer && player.CustomProperties.ContainsKey("avatarIndex"))
                {
                    if ((int)player.CustomProperties["avatarIndex"] == avatarIndex)
                    {
                        Debug.Log("Avatar already taken!");
                        return;
                    }
                }
            }
        }

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
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LobbyScene");
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
