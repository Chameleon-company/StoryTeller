using UnityEngine;

public class MessageStickTile : MonoBehaviour
{
    private StoryCardManager storyManager;
    
    void Start()
    {
        // Find the StoryCardManager in the scene
        storyManager = FindObjectOfType<StoryCardManager>();
        
        if (storyManager == null)
        {
            Debug.LogError("StoryCardManager not found in scene!");
        }
    }
    
    // Call this when a player lands on this tile
    public void OnPlayerLandOnTile()
    {
        if (storyManager != null)
        {
            Debug.Log("Player landed on Message Stick tile!");
            storyManager.DrawRandomStoryCard();
        }
    }
    
    // Optional: Visual feedback when player lands
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerLandOnTile();
        }
    }
}