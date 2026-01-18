using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform[] tiles;
    public int currentTileIndex = 0;
    private bool isMoving = false;

    public void MovePlayer(int steps)
    {
        if (isMoving) return;
        StartCoroutine(MoveSteps(steps));
    }

    private IEnumerator MoveSteps(int steps)
    {
        isMoving = true;

        for (int i = 0; i < steps; i++)
        {
            currentTileIndex++;

            if (currentTileIndex >= tiles.Length)
            {
                currentTileIndex = tiles.Length - 1;
                break;
            }

            transform.position = tiles[currentTileIndex].position;
            yield return new WaitForSeconds(0.3f);
        }

        isMoving = false;
    }
}
