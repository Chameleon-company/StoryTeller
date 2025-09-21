using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Rigidbody))]

public class dice : MonoBehaviour
{
    public Transform[] diceFaces;
    public Rigidbody rb;

    private int _diceIndex = -1;

    private bool _hasStoppedRolling;
    private bool _delayFinished;

    public static UnityAction<int, int> OnDiceResult;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    private void Update()
    {
        if (!_delayFinished) return;

        if (!_hasStoppedRolling && rb.angularVelocity.sqrMagnitude == 0f)
        {
            _hasStoppedRolling = true;
            GetNumberOnTopFace();
        }
    }

    [ContextMenu(itemName:"Get Top Face")]
    private void GetNumberOnTopFace()
    {
        //throw new NotImplementedException();
        if (diceFaces == null) return;

        var topFace = 0;
        var lastYPosition = diceFaces[0].position.y;

        for (int i = 0; i < diceFaces.Length; i++)
        {
            if (diceFaces[i].position.y > lastYPosition)
            {
                lastYPosition = diceFaces[i].position.y;
                topFace = i; 
            }
        }

        Debug.Log(message: $"Dice result {topFace + 1}");

        OnDiceResult?.Invoke(_diceIndex, topFace + 1);
        //return topFace + 1;
    }

    public void RollDice(float throwForce, float rollForce, int i)
    {
        _diceIndex = i;
        float randomVariance = Random.Range(-1f, 1f);
        rb.AddForce(transform.forward * (throwForce + randomVariance), ForceMode.Impulse);

        float randX = Random.Range(0f, 1f);
        float randY = Random.Range(0f, 1f);
        float randZ = Random.Range(0f, 1f);

        rb.AddTorque(new Vector3(randX, randY, randZ) * (rollForce + randomVariance), ForceMode.Impulse);

        DelayResult();
    }

    private async void DelayResult()
    {
        await Task.Delay(1000);
        _delayFinished = true;
    }

}
