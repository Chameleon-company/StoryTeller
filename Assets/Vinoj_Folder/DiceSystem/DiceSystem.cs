using UnityEngine;

public class DiceSystem : MonoBehaviour
{
    public int RollDice()
    {
        int value = Random.Range(1, 7); 
        Debug.Log("Dice rolled: " + value);
        return value;
    }
}
