using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class diceThrower : MonoBehaviour
{
    public dice diceToThrow;
    public int numOfDice = 2;
    public float throwForce = 3f;
    public float rollForce = 6f;

    private List<GameObject> _spawnedDice = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) RollDice();
    }

    private async void RollDice()
    {
        if (diceToThrow == null) return;

        foreach (GameObject die in _spawnedDice)
        {
            Destroy(die);
        }

        for (int i = 0; i < numOfDice; i++)
        {
            dice newDice = Instantiate(diceToThrow, transform.position, transform.rotation);
            _spawnedDice.Add(newDice.gameObject);
            newDice.RollDice(throwForce, rollForce, i);
            await Task.Yield();
        }
    }
}
