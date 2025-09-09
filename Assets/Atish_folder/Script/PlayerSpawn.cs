using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;  // assign your Player prefab in Inspector
    public Transform[] spawnPoints;  // optional: set in Inspector
    private Color[] playerColors = { Color.red, Color.blue, Color.green, Color.yellow };

    void Start()
    {
        SpawnPlayers(2); // example: 2 players
    }

    void SpawnPlayers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = (spawnPoints.Length > i) 
                ? spawnPoints[i].position 
                : new Vector3(i * 2, 0, 0);

            GameObject token = Instantiate(playerPrefab, spawnPos, Quaternion.identity);

            string uniqueId = System.Guid.NewGuid().ToString();
            Color assignedColor = playerColors[i % playerColors.Length];

            token.GetComponent<PlayerToken>().Init(uniqueId, assignedColor);

            Debug.Log($"Spawned Player ID={uniqueId}, Color={assignedColor}");
        }
    }
}
