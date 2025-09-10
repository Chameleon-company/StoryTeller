using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class PlayerSlot
{
    // Basic info for each player
    public string playerName;            // The name shown in UI
    public GameObject token;             // Player’s board piece
    public Color uiColor = Color.white;  // Optional: personalize UI with player’s color
    public bool isAI = false;            // Flag for future AI-controlled players
}

public class TurnManager : MonoBehaviour
{
    [Header("Players (order = turn order)")]
    public List<PlayerSlot> players = new List<PlayerSlot>();

    [Header("UI")]
    public TMP_Text turnText;        // Shows whose turn it is
    public Button nextTurnButton;    // Button to manually go to next turn
    public Button rollDiceButton;    // Optional dice button (can be null if unused)
    public TMP_Text timerText;       // Displays countdown for turns

    [Header("Settings")]
    public float turnDuration = 30f;   // Seconds each player has per turn
    public bool autoAdvanceOnTimeout = true; // If true, auto ends the turn on timeout

    [Header("State")]
    private int currentIndex = 0;   // Tracks which player is active
    private float timeRemaining;    // Countdown timer value

    // Events that other systems (e.g., dice manager, board manager) can subscribe to
    public static event Action<PlayerSlot> OnTurnStarted;
    public static event Action<PlayerSlot> OnTurnEnded;

    void Start()
    {
        // Safety check: make sure we have players
        if (players.Count == 0)
        {
            Debug.LogError("TurnManager: No players assigned.");
            enabled = false; // Disable script if empty
            return;
        }

        // Start the first player’s turn
        BeginTurn(currentIndex);
    }

    void Update()
    {
        // Handle the countdown timer
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            // Update UI text if available
            if (timerText != null)
                timerText.text = $"{Mathf.Ceil(timeRemaining)}s";

            // Timeout reached
            if (timeRemaining <= 0 && autoAdvanceOnTimeout)
            {
                EndTurn();   // Finish current turn
                NextTurn();  // Move to the next player
            }
        }
    }

    // Returns the currently active player
    public PlayerSlot Current => players[currentIndex];

    // Adds a new player dynamically at runtime
    public void AddPlayer(string name, GameObject token = null, bool isAI = false)
    {
        players.Add(new PlayerSlot { playerName = name, token = token, isAI = isAI });
    }

    // Removes a player by name
    public void RemovePlayer(string name)
    {
        players.RemoveAll(p => p.playerName == name);
    }

    // Public method to manually skip to the next turn
    public void NextTurn()
    {
        EndTurn(); // Trigger end-of-turn logic for current player
        currentIndex = (currentIndex + 1) % players.Count; // Wrap around list
        BeginTurn(currentIndex); // Start new player’s turn
    }

    // Starts a new turn
    private void BeginTurn(int index)
    {
        var p = players[index];
        timeRemaining = turnDuration;  // Reset timer

        UpdateUI(p); // Update visuals
        OnTurnStarted?.Invoke(p); // Trigger event for external systems

        Debug.Log($"Turn started: {p.playerName}");
    }

    // Ends the current turn
    private void EndTurn()
    {
        var p = Current;
        OnTurnEnded?.Invoke(p); // Trigger event for external systems

        Debug.Log($"Turn ended: {p.playerName}");
    }

    // Updates the text and button states
    private void UpdateUI(PlayerSlot p)
    {
        if (turnText != null)
        {
            turnText.text = $"{p.playerName}'s Turn";
            turnText.color = p.uiColor;
        }

        // If AI is playing, disable buttons
        if (rollDiceButton != null) rollDiceButton.interactable = !p.isAI;
        if (nextTurnButton != null) nextTurnButton.interactable = !p.isAI;

        // Highlight only the current player’s token
        HighlightOnly(p.token);
    }

    // Enlarges the active player’s token for clarity
    private void HighlightOnly(GameObject tokenToHighlight)
    {
        foreach (var slot in players)
        {
            if (slot.token == null) continue;

            float scale = (slot.token == tokenToHighlight) ? 1.15f : 1f;
            slot.token.transform.localScale = Vector3.one * scale;
        }
    }

    // Can be called when dice rolling is complete
    public void OnDiceResolvedAdvanceTurn()
    {
        NextTurn();
    }
}
