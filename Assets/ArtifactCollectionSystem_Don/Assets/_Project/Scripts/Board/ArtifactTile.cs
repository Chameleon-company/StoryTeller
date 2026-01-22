using UnityEngine;

public class ArtifactTile : MonoBehaviour
{
    public ArtifactDefinition artifact;
    public ArtifactPopupUI popupUI;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER HIT by: " + other.name);

        PlayerInventory inv = other.GetComponentInParent<PlayerInventory>();
        Debug.Log("Inventory found? " + (inv != null));

        if (inv == null) return;

        bool added = inv.AddArtifact(artifact);
        Debug.Log("Added artifact? " + added);

        if (added)
        {
            if (popupUI != null)
            {
                // NEW: show remaining requirement info in popup
                popupUI.ShowCollected(artifact, inv.Count, inv.requiredArtifacts);
            }
            else
            {
                Debug.LogError("popupUI is NULL on this tile - assign PopupController!");
            }
        }
    }
}
