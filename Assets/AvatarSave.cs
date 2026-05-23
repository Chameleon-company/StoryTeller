using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;

public class AvatarSave : MonoBehaviourPunCallbacks  // ← Changed here
{
    [Header("Assign your avatar buttons")]
    public Button[] avatarButtons;

    private string saveKey = "SelectedAvatar";
    public int CurrentAvatarIndex { get; private set; } = 0;

    private void Start()
    {
        CurrentAvatarIndex = PlayerPrefs.GetInt(saveKey, 0);

        // Button listeners
        for (int i = 0; i < avatarButtons.Length; i++)
        {
            int index = i;
            avatarButtons[i].onClick.AddListener(() => SelectAvatar(index));
        }

        // Sync after a short delay
        Invoke(nameof(SyncToPhoton), 0.5f);
    }

    public void SelectAvatar(int index)
    {
        CurrentAvatarIndex = index;
        PlayerPrefs.SetInt(saveKey, index);
        PlayerPrefs.Save();

        SyncToPhoton();
        Debug.Log($"[Avatar] Changed to {index}");
    }

    public void SyncToPhoton()
    {
        Hashtable props = new Hashtable
    {
        { "AvatarIndex", CurrentAvatarIndex }
    };

        PhotonNetwork.LocalPlayer.SetCustomProperties(props);
    }

    public int GetAvatarIndex()
    {
        return CurrentAvatarIndex;
    }

    // ✅ Now this will work
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        SyncToPhoton();
        Debug.Log("[Avatar] Synced on Joined Room");
    }
}