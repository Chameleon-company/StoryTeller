using UnityEngine;

public class PlayerToken : MonoBehaviour
{
    [HideInInspector] public string playerId;   // unique ID
    [HideInInspector] public Color playerColor; // assigned color

    public void Init(string id, Color color)
    {
        playerId = id;
        playerColor = color;

        // Apply color to humanoid parts
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer rend in renderers)
        {
            rend.material.color = color;
        }
    }
}
