using UnityEngine;
using UnityEngine.UI;

public class AvatarManager : MonoBehaviour
{
    public static AvatarManager Instance;

    private string saveKey = "SelectedAvatar";

    [Header("UI (Optional)")]
    public Image avatarImage; // Assign in Inspector (game screen image)

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateAvatarUI(); // auto update on start
    }

    public int GetAvatarIndex()
    {
        return PlayerPrefs.GetInt(saveKey, 0);
    }

    public Sprite GetAvatarSprite()
    {
        int index = GetAvatarIndex();
        return AvatarDatabase.Instance.GetAvatar(index);
    }

    public void UpdateAvatarUI()
    {
        if (avatarImage == null) return;

        avatarImage.sprite = GetAvatarSprite();
    }
}