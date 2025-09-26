using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public GameObject playerPrefab;     // assign in Inspector
    public Transform[] spawnPoints;     // optional
    private Color[] playerColors = { Color.red, Color.blue, Color.green, Color.yellow };

    void Start()
    {
        SpawnPlayers(2); // change count as needed
    }

    void SpawnPlayers(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = (spawnPoints != null && spawnPoints.Length > i)
                ? spawnPoints[i].position
                : new Vector3(i * 2f, 0f, 0f);

            GameObject tokenGO = Instantiate(playerPrefab, spawnPos, Quaternion.identity);
            tokenGO.name = $"Player_{i+1}";

            string uniqueId = System.Guid.NewGuid().ToString();
            Color assignedColor = playerColors[i % playerColors.Length];

            var token = tokenGO.GetComponent<PlayerToken>();
            token.Init(uniqueId, assignedColor);

            if (GameManager.I != null) GameManager.I.Register(token);

            Debug.Log($"Spawned Player ID={uniqueId}, Color={assignedColor}");
        }
    }
}
