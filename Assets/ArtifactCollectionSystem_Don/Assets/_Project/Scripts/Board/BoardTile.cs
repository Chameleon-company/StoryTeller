using UnityEngine;

public class BoardTile : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        
        PlayerInventory inv = other.GetComponentInParent<PlayerInventory>();
        if (inv == null) return;

        
        inv.SetLastTilePosition(transform.position);
    }
}
