using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }
     public List<Tile> tiles = new List<Tile>(); // all tiles on board
    public PlayerController playerController;   // drag player here in inspector

    private void Start()
    {
        // 1. Find all tiles under BoardManager
        tiles = GetComponentsInChildren<Tile>()
                .OrderBy(t => t.tileIndex)      // 2. Sort tiles by index
                .ToList();                      // 3. Store as movement path

        Debug.Log("Tiles loaded: " + tiles.Count);
    }

    public void MovePlayer(int steps)
{
    StartCoroutine(MovePlayerCoroutine(steps));
}
private void HandleTileLanding(Tile tile)
{
    Debug.Log("Player landed on tile: " + tile.tileIndex);

    switch (tile.tileType)
    {
        case TileType.Normal:
            break;

        case TileType.MessageStick:
            Debug.Log("Message Stick tile");
            break;

        case TileType.GoneWalkabout:
            Debug.Log("Gone Walkabout – skip next turn");
            break;

        case TileType.Waterhole:
            Debug.Log("Waterhole reached");
            break;

        case TileType.MeetingPlace:
            Debug.Log("Meeting Place reached");
            break;

        case TileType.Artifact:
            Debug.Log("Artifact tile");
            break;
    }
}



private IEnumerator MovePlayerCoroutine(int steps)
{
    if (tiles == null || tiles.Count == 0)
    {
        Debug.LogWarning("No tiles found! Check if tiles are children of BoardManager and have Tile.cs attached.");
        yield break;
    }

    if (playerController == null)
    {
        Debug.LogWarning("PlayerController not assigned in BoardManager Inspector!");
        yield break;
    }

    int startIndex = playerController.currentTileIndex;

    // Safety check
    if (startIndex < 0 || startIndex >= tiles.Count)
    {
        Debug.LogWarning("Player currentTileIndex is out of range.");
        yield break;
    }

    // Read current tile to decide direction
    Tile currentTile = tiles[startIndex];
    int direction = (currentTile.footprintColor == FootprintColor.White) ? 1 : -1;

    int targetIndex = startIndex;

    // Move step-by-step (like a real board game)
    for (int i = 0; i < steps; i++)
    {
        targetIndex += direction;

        // Wrap around board
        if (targetIndex >= tiles.Count) targetIndex = 0;
        if (targetIndex < 0) targetIndex = tiles.Count - 1;

        // Move the player to the next tile position
        playerController.transform.position = tiles[targetIndex].transform.position;

        // Small delay so you can SEE movement
        yield return new WaitForSeconds(0.25f);
    }

    // Save new position
    playerController.currentTileIndex = targetIndex;

    // Trigger tile landing logic
    HandleTileLanding(tiles[targetIndex]);
}


    public void EnterFinalPath()
    {
        Debug.Log("Entering final kangaroo track path!");
    }
    
}
