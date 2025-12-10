using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public void MovePlayer(int steps)
    {
        // Add your existing movement coroutine here
        Debug.Log("Moving " + steps + " steps...");
    }

    public void EnterFinalPath()
    {
        Debug.Log("Entering final kangaroo track path!");
    }
}
