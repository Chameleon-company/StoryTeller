using UnityEngine;
using UnityEngine.UI;

public class SimpleMessageManager : MonoBehaviour
{
        public Canvas messageCanvas;
    public UnityEngine.UI.Text questionText;  // Add UnityEngine.UI. if needed
    public UnityEngine.UI.Text answerText;
    public UnityEngine.UI.Button revealButton;
    public UnityEngine.UI.Button closeButton;

    
    void Start()
    {
        // Hide canvas at start
        if (messageCanvas != null)
            messageCanvas.gameObject.SetActive(false);
    }
    
    public void ShowMessage()
    {
        Debug.Log("ShowMessage called!");
        
        if (messageCanvas != null)
        {
            messageCanvas.gameObject.SetActive(true);
            Time.timeScale = 0f; // Pause game
        }
        
        // Show reveal button, hide close button
        if (revealButton != null)
            revealButton.gameObject.SetActive(true);
            
        if (closeButton != null)
            closeButton.gameObject.SetActive(false);
    }
    
    public void RevealAnswer()
    {
        Debug.Log("RevealAnswer called!");
        
        if (answerText != null)
            answerText.text = "Answer: Karlie Noon\n\nCultural Fact: Gamilaraay astrophysicist studying the Milky Way.";
        
        // Hide reveal, show close
        if (revealButton != null)
            revealButton.gameObject.SetActive(false);
            
        if (closeButton != null)
            closeButton.gameObject.SetActive(true);
    }
    
    public void CloseMessage()
    {
        Debug.Log("CloseMessage called!");
        
        if (messageCanvas != null)
            messageCanvas.gameObject.SetActive(false);
            
        Time.timeScale = 1f; // Resume game
    }
}