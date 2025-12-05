using UnityEngine;
using UnityEngine.UI;

public class TotemTokenManager : MonoBehaviour
{
    [Header("Prefab & Container")]
    [Tooltip("A Button prefab that has TotemTokenButton component")]
    [SerializeField] private GameObject totemTokenPrefab;

    [Tooltip("Parent transform (Layout Group) where tokens will be spawned")]
    [SerializeField] private Transform tokenContainer;

    [Header("Totem sprites (8) - order matters")]
    [SerializeField] private Sprite[] tokenSprites = new Sprite[8];

    public int SelectedIndex { get; private set; } = -1;

    private void Awake()
    {
        if (totemTokenPrefab == null || tokenContainer == null)
        {
            Debug.LogError("TotemTokenManager: assign prefab & container in inspector.");
            return;
        }

        // Remove existing children (safe for development)
        for (int i = tokenContainer.childCount - 1; i >= 0; i--)
            DestroyImmediate(tokenContainer.GetChild(i).gameObject);

        // Spawn up to 8 totem tokens
        int count = Mathf.Min(8, tokenSprites.Length);
        for (int i = 0; i < count; i++)
        {
            var go = Instantiate(totemTokenPrefab, tokenContainer, false);

            var tokenButton = go.GetComponent<TotemTokenButton>();
            if (tokenButton == null)
            {
                Debug.LogError("TotemTokenManager: prefab missing TotemTokenButton.");
                continue;
            }

            tokenButton.Setup(i, tokenSprites[i], OnTokenSelected);
        }
    }

    private void OnTokenSelected(int index)
    {
        SelectedIndex = index;
        Debug.Log($"Totem selected: {index}");

        // Highlight selection
        var tokens = tokenContainer.GetComponentsInChildren<TotemTokenButton>();
        foreach (var t in tokens)
            t.SetSelected(t.Index == index);

        // TODO: Add game logic hook here if required
        // Example: GameManager.Instance.SetPlayerTotem(index);
    }
}
