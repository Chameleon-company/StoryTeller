using Photon.Pun;
using UnityEngine;

public class PlayerMovement : MonoBehaviourPun
{
    public float moveDistance = 2f;

    private bool isMoving = false;

    // 🔥 Allow other scripts to check movement state
    public bool IsMoving()
    {
        return isMoving;
    }

    public void MoveSteps(int steps)
    {
        if (!photonView.IsMine) return;
        if (isMoving) return;

        Debug.Log("MOVING PLAYER 🚶 Steps: " + steps);

        StartCoroutine(MoveRoutine(steps));
    }

    System.Collections.IEnumerator MoveRoutine(int steps)
    {
        isMoving = true;

        for (int i = 0; i < steps; i++)
        {
            Vector3 start = transform.position;
            Vector3 target = start + Vector3.forward * moveDistance;

            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime * 2f;
                transform.position = Vector3.Lerp(start, target, t);
                yield return null;
            }
        }

        isMoving = false;
    }
}