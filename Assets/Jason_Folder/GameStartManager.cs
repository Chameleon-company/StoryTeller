using UnityEngine;
using UnityEngine.UI; 
using Photon.Pun; 
using Photon.Realtime;
using TMPro;

public class GameStartManager : MonoBehaviourPunCallbacks
{
    public TMP_Text statusText;
    public int minPlayersToStart = 2;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateStatus();
    }

    void UpdateStatus()
    {
        int count = PhotonNetwork.CurrentRoom.PlayerCount;
        int needed = Mathf.Max(0, minPlayersToStart - count);

        if (count < minPlayersToStart)
        {
           statusText.text = $"Waiting for players to join... ({count}/8)";
        }
        else
        {
            statusText.text = $"Waiting for more players to join... ({count}/8) Game will start soon by Host Player";
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateStatus();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateStatus();
    }

   
}
