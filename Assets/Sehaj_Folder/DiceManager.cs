using Photon.Pun;
using UnityEngine;

public class DiceManager : MonoBehaviourPun
{
    public int diceValue;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TryRollDice();
        }
    }

    public void RollDice()
    {
        TryRollDice();
    }

    void TryRollDice()
    {
        // 🔥 Find local player
        PlayerMovement player = FindObjectOfType<PlayerMovement>();

        if (player != null && player.IsMoving())
        {
            Debug.Log("WAIT! Player still moving ❌");
            return;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RollDiceRPC", RpcTarget.All);
        }
    }

    [PunRPC]
    void RollDiceRPC()
    {
        diceValue = Random.Range(1, 7);
        Debug.Log("Dice: " + diceValue);

        foreach (PlayerMovement player in FindObjectsOfType<PlayerMovement>())
        {
            player.MoveSteps(diceValue);
        }
    }
}