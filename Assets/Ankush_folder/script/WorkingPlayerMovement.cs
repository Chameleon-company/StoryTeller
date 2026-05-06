using UnityEngine;

public class WorkingPlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Camera playerCamera;  // We'll set this
    
    private Rigidbody rb;
    
    void Start()
    {
        // Get the Rigidbody component (add if missing)
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
            rb.freezeRotation = true; // Prevents falling over
        }
        
        // Find camera if not set
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }
    
    void Update()
    {
        // Get WASD input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        // Create movement direction based on camera facing
        Vector3 forward = playerCamera.transform.forward;
        Vector3 right = playerCamera.transform.right;
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        
        Vector3 movement = (forward * vertical + right * horizontal);
        
        // Apply movement
        rb.linearVelocity = new Vector3(movement.x * moveSpeed, rb.linearVelocity.y, movement.z * moveSpeed);
        
        // Face movement direction
        if (movement.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
        
        // Optional: Show input in Console for debugging
        // Debug.Log($"Horizontal: {horizontal}, Vertical: {vertical}");
    }
}