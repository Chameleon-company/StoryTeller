using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public int requiredArtifacts = 4;
    private HashSet<string> collected = new HashSet<string>();

    // NEW: last tile tracking
    private Vector3 lastTilePosition;
    private bool hasLastTilePosition = false;

    public bool AddArtifact(ArtifactDefinition artifact)
    {
        if (artifact == null) return false;
        return collected.Add(artifact.artifactId);
    }

    public int Count => collected.Count;

    public bool HasAllRequired()
    {
        return Count >= requiredArtifacts;
    }

    // NEW helpers
    public int RemainingToUnlock => Mathf.Max(0, requiredArtifacts - Count);

    public void SetLastTilePosition(Vector3 tilePos)
    {
        lastTilePosition = tilePos;
        hasLastTilePosition = true;
    }

    public bool TryGetLastTilePosition(out Vector3 pos)
    {
        pos = lastTilePosition;
        return hasLastTilePosition;
    }
}
