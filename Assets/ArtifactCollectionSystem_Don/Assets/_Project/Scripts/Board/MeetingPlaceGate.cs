using UnityEngine;

public class MeetingPlaceGate : MonoBehaviour
{
    public Transform meetingPlaceCenter;

    // NEW: reference to popup controller (same one used by tiles)
    public ArtifactPopupUI popupUI;

    // Optional: small Y offset when teleporting back
    public float returnYOffset = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        PlayerInventory inv = other.GetComponentInParent<PlayerInventory>();
        if (inv == null) return;

        if (!inv.HasAllRequired())
        {
            Debug.Log("Meeting Place locked. Collect more artefacts. (" + inv.Count + "/" + inv.requiredArtifacts + ")");

            // 1) Show popup with remaining count
            if (popupUI != null)
                popupUI.ShowGateLocked(inv.Count, inv.requiredArtifacts);

            // 2) Send player back to previous tile
            if (inv.TryGetLastTilePosition(out Vector3 lastTilePos))
            {
                other.transform.position = lastTilePos + new Vector3(0, returnYOffset, 0);
            }

            return;
        }

        Debug.Log("Meeting Place unlocked! Entering...");
        if (meetingPlaceCenter != null)
        {
            other.transform.position = meetingPlaceCenter.position + new Vector3(0, 0.5f, 0);
        }
    }
}
