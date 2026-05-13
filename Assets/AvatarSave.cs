using UnityEngine;
using UnityEngine.UI;

public class AvatarSave : MonoBehaviour
{
    [Header("Assign your 7 buttons here")]
    public Button[] avatarButtons;

    private string saveKey = "SelectedAvatar";

    private void Start()
    {
        // Setup button listeners
        for (int i = 0; i < avatarButtons.Length; i++)
        {
            int index = i;

            avatarButtons[i].onClick.AddListener(() =>
            {
                OnAvatarButtonClicked(index);
            });
        }

     
    }

    void OnAvatarButtonClicked(int index)
    {
        Debug.Log("Button Pressed: " + index);

        // Save selected avatar
        PlayerPrefs.SetInt(saveKey, index);
        PlayerPrefs.Save();
    }

    void LoadAvatar()
    {
        if (PlayerPrefs.HasKey(saveKey))
        {
            int savedIndex = PlayerPrefs.GetInt(saveKey);
            Debug.Log("Loaded Avatar: " + savedIndex);

            // You can use this index to update UI later
        }
        else
        {
            Debug.Log("No Avatar Saved Yet");
        }
    }
}