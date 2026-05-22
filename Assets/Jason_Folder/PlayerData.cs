using UnityEngine;
using Photon.Pun; 
using ExitGames.Client.Photon;

public static class PlayerData
{
    public static void SetPlayerName(string name) 
    { 
        Hashtable props = new Hashtable(); 
        props["playerName"] = name; 
        PhotonNetwork.LocalPlayer.SetCustomProperties(props); 
    }
    
    public static void SetAvatarIndex(int index) 
    { 
        Hashtable props = new Hashtable(); 
        props["avatarIndex"] = index; 
        PhotonNetwork.LocalPlayer.SetCustomProperties(props); 
    }
}
