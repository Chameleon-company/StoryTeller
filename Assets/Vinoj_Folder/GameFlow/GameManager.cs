using System;
using UnityEngine;
public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public int playerPosition = 0;
    public int lapsCompleted = 0;
    public bool hasLeftStartingWaterhole = false;
    public bool enteringMeetingPlace = false;

    private void Awake()
    {
        Instance = this;
    }

    public void OnDiceRolled(int roll)
    {
        if (!hasLeftStartingWaterhole)
        {
            HandleStartWaterhole(roll);
            return;
        }

        if (enteringMeetingPlace)
        {
            HandleMeetingPlaceEntry(roll);
            return;
        }

        MovePlayer(roll);
    }

    void HandleStartWaterhole(int roll)
    {
        if (roll == 6)
        {
            hasLeftStartingWaterhole = true;
            Debug.Log("Player has left the starting waterhole!");
        }
        else
        {
            Debug.Log("Keep rolling, you must roll the correct number to leave.");
        }
    }

    void MovePlayer(int roll)
    {
        playerPosition += roll;
        Debug.Log("Player moved to tile: " + playerPosition);
    }

    void HandleMeetingPlaceEntry(int roll)
    {
        if (roll == 8)
        {
            Debug.Log("Player entered the Meeting Place!");
        }
        else
        {
            Debug.Log("Must roll an 8. Try again next turn.");
        }
    }
}
