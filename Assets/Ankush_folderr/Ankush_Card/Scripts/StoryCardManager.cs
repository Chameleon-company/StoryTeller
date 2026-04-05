using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class StoryCardManager : MonoBehaviour
{
    [Header("Story Cards List")]
    public List<StoryCardData> allStoryCards = new List<StoryCardData>();
    
    [Header("UI References")]
    public GameObject storyCardPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI storyText;
    public Image cardImage;
    public Button closeButton;
    public Button readAloudButton;
    
    [Header("Audio")]
    public AudioSource audioSource;
    
    private StoryCardData currentCard;
    
    [System.Serializable]
    public class StoryCardData
    {
        public string cardName;
        [TextArea(5, 10)]
        public string storyContent;
        public Sprite cardSprite;
        public AudioClip narrationClip;
    }
    
    void Start()
    {
        if (storyCardPanel != null)
            storyCardPanel.SetActive(false);
            
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseStoryCard);
            
        if (readAloudButton != null)
            readAloudButton.onClick.AddListener(ReadStoryAloud);
    }
    
    public void ShowStoryCard(string cardName)
    {
        StoryCardData foundCard = allStoryCards.Find(c => c.cardName == cardName);
        
        if (foundCard != null)
        {
            currentCard = foundCard;
            titleText.text = foundCard.cardName;
            storyText.text = foundCard.storyContent;
            if (cardImage != null && foundCard.cardSprite != null)
                cardImage.sprite = foundCard.cardSprite;
            storyCardPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Story card not found: " + cardName);
        }
    }
    
    public void ReadStoryAloud()
    {
        if (currentCard != null && currentCard.narrationClip != null && audioSource != null)
        {
            audioSource.PlayOneShot(currentCard.narrationClip);
        }
    }
    
    public void CloseStoryCard()
    {
        storyCardPanel.SetActive(false);
        currentCard = null;
    }
}