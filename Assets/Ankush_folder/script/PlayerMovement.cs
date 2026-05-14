using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");
        
        Vector3 movement = new Vector3(moveX, 0, moveZ);
        transform.Translate(movement * speed * Time.deltaTime);
    }
}