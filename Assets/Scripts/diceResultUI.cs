using TMPro;
using UnityEngine;

public class diceResultUI : MonoBehaviour
{
    [SerializeField] private TMP_Text diceOneResult, diceTwoResult, totalResult;

    private int diceOneValue = 0;
    private int diceTwoValue = 0;

    private void OnEnable()
    {
        dice.OnDiceResult += SetText;
    }

    private void OnDisable()
    {
        dice.OnDiceResult -= SetText;
    }

    private void SetText(int diceIndex, int diceResult)
    {
        if (diceIndex == 0)
        {
            diceOneValue = diceResult;
            diceOneResult.SetText(sourceText: $"DICE ONE: {diceResult}");
        }
        else
        {
            diceTwoValue = diceResult;
            diceTwoResult.SetText(sourceText: $"DICE TWO: {diceResult}");
        }

        totalResult.SetText(sourceText: $"You can move: {diceOneValue + diceTwoValue}");
    }
}
