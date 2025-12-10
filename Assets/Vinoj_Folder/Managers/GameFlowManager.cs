using UnityEngine;

public class GameFlowManager : MonoBehaviour
{
    public static GameFlowManager Instance;

    public int requiredStartNumber = 6; // example, set in Inspector
    public bool hasLeftStart = false;
    public bool finalStage = false;

    public int artifactsCollected = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void OnDiceRolled(int rolledValue)
    {
        if (!hasLeftStart)
        {
            HandleStartPhase(rolledValue);
            return;
        }

        if (finalStage)
        {
            HandleFinalStage(rolledValue);
            return;
        }

        HandleNormalMovement(rolledValue);
    }

    private void HandleStartPhase(int rolled)
    {
        Debug.Log("Start Phase Roll: " + rolled);

        if (rolled == requiredStartNumber)
        {
            Debug.Log("Player leaves the waterhole!");

            hasLeftStart = true;

            DiceManager.Instance.SwitchToSingleDie(); // store second die
        }
        else
        {
            Debug.Log("Roll again until you get: " + requiredStartNumber);
        }
    }

    private void HandleNormalMovement(int rolled)
    {
        Debug.Log("Move player by: " + rolled);

        BoardManager.Instance.MovePlayer(rolled); // your existing movement
    }

    private void HandleFinalStage(int rolled)
    {
        Debug.Log("Final Stage Roll: " + rolled);

        if (rolled == 8)
        {
            Debug.Log("SUCCESS — Move along kangaroo tracks!");
            BoardManager.Instance.EnterFinalPath();
        }
        else
        {
            Debug.Log("Must roll EXACTLY 8!");
        }
    }

    public void TryEnterFinalStage()
    {
        if (artifactsCollected == 4)
        {
            finalStage = true;
            DiceManager.Instance.RestoreSecondDie(); // regain second die
        }
    }
}
