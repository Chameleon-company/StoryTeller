using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    
    private Rigidbody rb;
    private Vector3 moveDirection;
    
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
        
        // If no Rigidbody exists, add one
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        
        // Freeze rotation so player doesn't tip over
        rb.freezeRotation = true;
        
        // Use continuous collision detection for better collisions
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        Debug.Log("PlayerController initialized! Use WASD to move.");
    }
    
    void Update()
    {
        // Get input from WASD or Arrow keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // Calculate movement direction (relative to camera)
        moveDirection = new Vector3(horizontal, 0, vertical).normalized;
        
        // Optional: Debug input (uncomment to test if keys work)
        // if (horizontal != 0 || vertical != 0)
        // {
        //     Debug.Log($"Input detected: H={horizontal}, V={vertical}");
        // }
    }
    
    void FixedUpdate()
    {
        // Apply movement in FixedUpdate for physics
        if (moveDirection.magnitude > 0.1f)
        {
            Vector3 move = moveDirection * moveSpeed * Time.fixedDeltaTime;
            rb.MovePosition(transform.position + move);
            
            // Rotate to face movement direction
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.fixedDeltaTime);
        }
    }
    
    // Optional: Visual feedback when moving
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}