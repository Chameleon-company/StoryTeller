using UnityEngine;

public class MessageStickTrigger : MonoBehaviour
{
    // This will connect to our Message Manager
    public GameObject messageManager;
    
    void Start()
    {
        // Just to confirm script is working
        Debug.Log("Message Stick is ready in scene!");
    }
    
    void OnTriggerEnter(Collider other)
    {
        // Log what touched us
        Debug.Log("Message Stick touched by: " + other.name);
        
        // Check if it's the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("✓ Player collected the Message Stick!");
            
            // Tell the Message Manager to show the message
            if (messageManager != null)
            {
                messageManager.SendMessage("ShowMessage");
            }
            else
            {
                Debug.LogError("⚠ No Message Manager connected!");
            }
            
            // Make the stick disappear
            gameObject.SetActive(false);
            Debug.Log("Message Stick hidden");
        }
    }
}