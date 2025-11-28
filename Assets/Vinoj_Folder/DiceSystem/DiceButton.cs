using UnityEngine;

public class DiceButton : MonoBehaviour
{
    public void RollDice()
    {
        int roll = Random.Range(1, 7);  
        GameFlowManager.Instance.OnDiceRolled(roll);
    }
}
