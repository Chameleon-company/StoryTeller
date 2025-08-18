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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

}
