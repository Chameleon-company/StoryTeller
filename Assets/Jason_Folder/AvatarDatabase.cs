using UnityEngine;

public class AvatarDatabase : MonoBehaviour
{
    public static AvatarDatabase Instance;
    public Sprite[] avatars;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);   
        }
        else
        {
            Destroy(gameObject);             
        }
    }

    public Sprite GetAvatar(int index)
    {
        if (index >= 0 && index < avatars.Length)
        {
            return avatars[index];
        }

        Debug.LogWarning("Avatar index out of range: " + index);
        return null;
    }
}
