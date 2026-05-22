using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class PhotonConnect : MonoBehaviourPunCallbacks
{

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Connecting to Photon..."); 
        PhotonNetwork.ConnectUsingSettings(); 
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon Master Server.");
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby Success!");

        
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joinning Room and Loading the scene now"); 
        SceneManager.LoadScene("LobbyScene");
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        Debug.LogWarning("Disconnected from Photon: " + cause);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
