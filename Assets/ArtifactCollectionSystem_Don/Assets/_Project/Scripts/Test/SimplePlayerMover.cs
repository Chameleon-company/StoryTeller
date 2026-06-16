using UnityEngine;

public class SimplePlayerMover : MonoBehaviour
{
    public float speed = 4f;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(h, 0, v) * speed * Time.deltaTime;
        transform.position += move;
    }
}
