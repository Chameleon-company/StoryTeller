using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;

    [Header("Movement")]
    public float stepSize = 1f;                 // distance per step
    public float stepSpeed = 4f;                // higher = faster
    public Vector3 moveAxis = Vector3.right;    // change to Vector3.forward if you prefer Z

    private readonly List<PlayerToken> players = new List<PlayerToken>();
    private int currentPlayerIndex = 0;
    private bool isMoving = false;
    private int lastRoll = 0;

    void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
    }

    // Called by PlayerSpawn when a token is created
    public void Register(PlayerToken token)
    {
        if (!players.Contains(token))
        {
            players.Add(token);
            token.name = $"Player_{players.Count}";
            Debug.Log($"Registered {token.name}  (total: {players.Count})");
        }
    }

    public bool IsPlayersTurn(PlayerToken token) =>
        players.Count > 0 && players[currentPlayerIndex] == token;

    public bool IsAnimating() => isMoving;

    void Update()
    {
        if (players.Count == 0 || isMoving) return;

        // SPACE = random roll 1..6
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeTurn(Random.Range(1, 7));
            return;
        }

        // Number keys 1..9 = exact steps (handy for testing)
        foreach (char c in Input.inputString)
        {
            if (char.IsDigit(c))
            {
                int n = c - '0';
                if (n > 0) { TakeTurn(n); break; }
            }
        }
    }

    void TakeTurn(int steps)
    {
        if (players.Count == 0) return;

        var current = players[currentPlayerIndex];
        lastRoll = steps;
        Debug.Log($"{current.name} rolls {steps}");
        StartCoroutine(MovePlayerSteps(current, steps, stepSize));
    }

    IEnumerator MovePlayerSteps(PlayerToken token, int steps, float _stepSize)
    {
        isMoving = true;

        var rb = token.GetComponent<Rigidbody>();
        bool usePhysics = rb && !rb.isKinematic;

        for (int i = 0; i < steps; i++)
        {
            Vector3 start = token.transform.position;
            Vector3 end   = start + moveAxis.normalized * _stepSize;

            float t = 0f;
            while (t < 1f)
            {
                t += Time.deltaTime * stepSpeed;
                Vector3 pos = Vector3.Lerp(start, end, t);
                if (usePhysics) rb.MovePosition(pos);
                else token.transform.position = pos;
                yield return null;
            }
        }

        // next player's turn
        currentPlayerIndex = (currentPlayerIndex + 1) % players.Count;
        isMoving = false;
    }

    // Simple on-screen debug HUD
    void OnGUI()
    {
        if (players.Count == 0) return;
        var current = players[currentPlayerIndex];
        GUI.Box(new Rect(10, 10, 260, 70),
            $"Turn: {current.name}\nLast Roll: {lastRoll}\n[Space]=roll 1–6 | [1..9]=force");
    }
}
