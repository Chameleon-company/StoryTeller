using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameStartButton : MonoBehaviourPunCallbacks
{
    public Button startButton;
    public int minPlayersToStart = 2;

    void Start()
    {
        startButton.gameObject.SetActive(false);
        UpdateButton();
    }

    void UpdateButton()
    {
        bool enoughPlayers = PhotonNetwork.CurrentRoom.PlayerCount >= minPlayersToStart;
        bool isMaster = PhotonNetwork.IsMasterClient;

        startButton.gameObject.SetActive(enoughPlayers && isMaster);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        UpdateButton();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        UpdateButton();
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        UpdateButton();
    }

    public void OnStartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Test_Scene");
        }
    }
}
