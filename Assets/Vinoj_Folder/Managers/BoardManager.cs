using UnityEngine;
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
        // Add your existing movement coroutine here
        Debug.Log("Moving " + steps + " steps...");
    }

    public void EnterFinalPath()
    {
        Debug.Log("Entering final kangaroo track path!");
    }
    
}
