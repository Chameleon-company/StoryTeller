using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonConnect : MonoBehaviourPunCallbacks
{
    [Header("Photon Test Room Settings")]
    public string gameVersion = "1";
    public string testRoomName = "StoryTellerTestRoom";
    public byte maxPlayers = 8;

    private string uniquePlayerId;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.GameVersion = gameVersion;

        // Gives each game copy a different identity when testing on one PC
        uniquePlayerId = "Player_" + Random.Range(1000, 9999);

        PhotonNetwork.NickName = uniquePlayerId;
        PhotonNetwork.AuthValues = new AuthenticationValues(uniquePlayerId);

        Debug.Log("Photon player identity set as: " + uniquePlayerId);
    }

    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            Debug.Log("Connecting to Photon...");
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server.");
        Debug.Log("Joining Photon lobby...");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby Success!");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = maxPlayers;
        roomOptions.IsVisible = true;
        roomOptions.IsOpen = true;

        Debug.Log("Joining or creating room: " + testRoomName);

        PhotonNetwork.JoinOrCreateRoom(
            testRoomName,
            roomOptions,
            TypedLobby.Default
        );
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Photon room successfully.");
        Debug.Log("Room Name: " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Players in Room: " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers);

        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Master client loading LobbyScene...");
            PhotonNetwork.LoadLevel("LobbyScene");
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("New player joined room: " + newPlayer.NickName);
        Debug.Log("Players in Room: " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("Player left room: " + otherPlayer.NickName);
        Debug.Log("Players in Room: " + PhotonNetwork.CurrentRoom.PlayerCount + "/" + PhotonNetwork.CurrentRoom.MaxPlayers);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Join room failed: " + message);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Create room failed: " + message);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning("Disconnected from Photon: " + cause);
    }
}