using UnityEngine;
using TMPro;

public class DiceManager : MonoBehaviour
{
    public static DiceManager Instance;

    public PlayerController playerController;

    public TMP_Text diceText1;
    public TMP_Text diceText2;

    private int dice1;
    private int dice2;

    public bool usingSingleDie = false; // once player leaves starting waterhole

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void RollDice()
    {
        if (!usingSingleDie)
        {
            //Roll BOTH dice
            dice1 = Random.Range(1, 7);
            dice2 = Random.Range(1, 7);

            diceText1.text = dice1.ToString();
            diceText2.text = dice2.ToString();

            GameFlowManager.Instance.OnDiceRolled(dice1 + dice2);
            
        }
        else
        {
            //Roll ONE die
            dice1 = Random.Range(1, 7);

            diceText1.text = dice1.ToString();
            diceText2.text = "-";

            GameFlowManager.Instance.OnDiceRolled(dice1);
        }
    }

    public void SwitchToSingleDie()
    {
        usingSingleDie = true;
        diceText2.text = "(Stored)";
    }

    public void RestoreSecondDie()
    {
        usingSingleDie = false;
        diceText2.text = "0";
    }

    public int GetDiceTotal()
    {
        return usingSingleDie ? dice1 : (dice1 + dice2);
    }

    void Update()
{
    
    if (Input.GetKeyDown(KeyCode.Space))
    {
        RollDice();
    }
}
 void RollDiceSpace()
    {
        int diceValue = Random.Range(1, 7);
        playerController.MovePlayer(diceValue);
    }
}