using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab;
    public Transform[] spawnPoints;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected!");
        PhotonNetwork.JoinOrCreateRoom("Room1", new RoomOptions(), TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room!");

        int playerIndex = PhotonNetwork.CurrentRoom.PlayerCount - 1;
        playerIndex = playerIndex % spawnPoints.Length;

        Transform spawnPoint = spawnPoints[playerIndex];

        Vector3 spawnPos = spawnPoint.position;
        spawnPos.y = 0; // keep on ground

        // ✅ SPAWN PLAYER
        GameObject player = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, spawnPoint.rotation);

        // ✅ LINK PLAYER TO LOCAL USER (VERY IMPORTANT)
        PhotonNetwork.LocalPlayer.TagObject = player;
    }
}