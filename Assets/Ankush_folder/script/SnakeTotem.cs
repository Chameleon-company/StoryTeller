using UnityEngine;
using System.Collections;

public class SnakeTotem : MonoBehaviour
{
    [Header("Snake Totem Settings")]
    public string totemName = "Snake";
    public string totemMeaning = "Transformation and sacred knowledge";
    
    [Header("Animation Settings")]
    public float wiggleSpeed = 1.5f;
    public float wiggleAmount = 15f;
    
    [Header("Story Association")]
    public string associatedStory = "The Rainbow Serpent";
    
    private Quaternion originalRotation;
    private bool isSelected = false;
    private float floatTimer = 0f;
    
    void Start()
    {
        // Store original rotation for wiggle animation
        originalRotation = transform.rotation;
        
        // Add a collider for interaction
        AddInteractionCollider();
    }
    
    void Update()
    {
        // Idle animation - gentle swaying
        if (!isSelected)
        {
            IdleAnimation();
        }
    }
    
    void IdleAnimation()
    {
        // Gentle floating motion
        floatTimer += Time.deltaTime;
        float floatY = Mathf.Sin(floatTimer * 1.5f) * 0.1f;
        transform.localPosition = new Vector3(0, floatY, 0);
        
        // Gentle rotation wiggle
        float wiggle = Mathf.Sin(Time.time * wiggleSpeed) * wiggleAmount;
        transform.rotation = originalRotation * Quaternion.Euler(0, wiggle, 0);
    }
    
    void AddInteractionCollider()
    {
        // Add a collider if none exists
        if (GetComponent<Collider>() == null)
        {
            BoxCollider collider = gameObject.AddComponent<BoxCollider>();
            collider.size = new Vector3(1, 1, 1);
            collider.isTrigger = true;
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isSelected)
        {
            SelectTotem();
        }
    }
    
    void SelectTotem()
    {
        isSelected = true;
        Debug.Log("Snake Totem Selected: " + totemMeaning);
        
        // You can trigger story card here
        TriggerStoryCard();
        
        // Play selection effect
        StartCoroutine(SelectionEffect());
    }
    
    void TriggerStoryCard()
    {
        Debug.Log("Story: " + associatedStory);
    }
    
    IEnumerator SelectionEffect()
    {
        // Highlight effect
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            Color originalColor = renderer.material.color;
            renderer.material.color = Color.yellow;
            
            yield return new WaitForSeconds(0.5f);
            
            renderer.material.color = originalColor;
        }
        yield return null;
    }
}