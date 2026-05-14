using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class StoryCardManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject storyCardPanel;      // The panel that shows the card
   public TMPro.TextMeshProUGUI titleText;  // NEW - for TextMeshPro
public TMPro.TextMeshProUGUI storyText;  // NEW - for TextMeshPro
    public Image cardImage;                // Image for card (optional)
    public Button closeButton;             // Button to close the card
    
    [Header("Story Cards Data")]
    public List<StoryCard> allStoryCards = new List<StoryCard>();
    
    private StoryCard currentCard;
    private bool isCardActive = false;
    
    void Start()
    {
        // Initialize all story cards
        InitializeStoryCards();
        
        // Hide the card panel at start
        if (storyCardPanel != null)
            storyCardPanel.SetActive(false);
        
        // Setup close button
        if (closeButton != null)
            closeButton.onClick.AddListener(CloseStoryCard);
        
        Debug.Log("StoryCardManager initialized with " + allStoryCards.Count + " story cards");
    }
    
    void InitializeStoryCards()
    {
        // Clear existing cards
        allStoryCards.Clear();
        


        Sprite creationImg = Resources.Load<Sprite>("Ankush-folder/Images/img");
        // Card 1: Creation Story
        allStoryCards.Add(new StoryCard(
            "✨ The Creation Story ✨",
            "In the beginning, the land was flat and empty. The Ancestors slept beneath the surface, dreaming of mountains, rivers, and all living things.\n\n" +
            "When they awoke, they began to travel across the land, creating everything we see today. Their footsteps formed the rivers, their bodies became the mountains, and their spirits remain in the land forever.\n\n" +
            "🌿 Reflection: Everything in nature has a spirit and a story. What connections do you feel to the land around you?"
        ));
        
        // Card 2: Rainbow Serpent
        allStoryCards.Add(new StoryCard(
            "🌈 The Rainbow Serpent 🌈",
            "The Rainbow Serpent is a powerful creator spirit. As she traveled across the land, she carved out rivers and waterholes with her massive body.\n\n" +
            "Wherever she rested, she created sacred water sources that give life to all creatures. Her colors in the sky remind us of the connection between water, land, and sky.\n\n" +
            "💧 Reflection: Water gives life to everything. How can you show gratitude for the water in your life?"
        ));
        
        // Card 3: First Sunrise
        allStoryCards.Add(new StoryCard(
            "☀️ The First Sunrise ☀️",
                "Before the first sunrise, the world was dark. The spirits gathered to bring light to the land. They worked together, sharing their knowledge and strength.\n\n" +
                "When the sun finally rose, it brought warmth, life, and the gift of seeing the beauty of Country. The sunrise reminds us that new beginnings come from working together.\n\n" +
                "🤝 Reflection: What can you achieve when you work together with others?"
        ));
        
        // Card 4: Koala and the Kangaroo
        allStoryCards.Add(new StoryCard(
            "🐨 Koala and the Kangaroo 🦘",
            "Koala was sleepy and always rested in the trees. Kangaroo was always moving and hopping across the land.\n\n" +
            "One day, they argued about whose way of life was better. The Creator Spirit taught them that both rest and movement are important.\n\n" +
            "Koala teaches us to rest and reflect, while Kangaroo teaches us to journey and explore. Both are needed for balance in life.\n\n" +
            "⚖️ Reflection: Do you need more rest or more movement in your life right now?"
        ));
        
        // Card 5: BONUS - Sacred Sites (Extra card!)
        allStoryCards.Add(new StoryCard(
            "🏔️ Sacred Sites 🏔️",
            "Sacred Sites are special places on Country where Ancestral spirits rest. These places hold stories, laws, and knowledge passed down for thousands of generations.\n\n" +
            "When we visit Sacred Sites, we must show respect - speak softly, listen deeply, and leave only footprints.\n\n" +
            "🙏 Reflection: What places feel sacred or special to you? How do you show respect when you visit them?"
        ));
        
        // Card 6: BONUS - Songlines
        allStoryCards.Add(new StoryCard(
            "🎵 Songlines 🎵",
            "Songlines are ancient paths across the land, marked by songs and stories. They map waterholes, mountains, and sacred places.\n\n" +
            "By singing the songs, Ancestors could travel hundreds of kilometers, knowing exactly where to find water and food. Songlines connect people, Country, and culture.\n\n" +
            "🗺️ Reflection: How do you find your way? What 'maps' do you use in your life?"
        ));
    }
    
    // Call this method when a player lands on a Message Stick tile
    public void DrawRandomStoryCard()
    {
        if (allStoryCards.Count == 0)
        {
            Debug.LogWarning("No story cards available!");
            return;
        }
        
        // Pick a random card
        int randomIndex = Random.Range(0, allStoryCards.Count);
        currentCard = allStoryCards[randomIndex];
        
        // Display the card
        DisplayStoryCard(currentCard);
        
        Debug.Log("Story Card Drawn: " + currentCard.cardTitle);
    }
    
    // Draw a specific card by index (if you want to test a particular card)
    public void DrawStoryCardByIndex(int index)
    {
        if (index < 0 || index >= allStoryCards.Count)
        {
            Debug.LogWarning("Invalid card index!");
            return;
        }
        
        currentCard = allStoryCards[index];
        DisplayStoryCard(currentCard);
        Debug.Log("Drew specific story card: " + currentCard.cardTitle);
    }
    
    void DisplayStoryCard(StoryCard card)
    {
        if (storyCardPanel == null)
        {
            Debug.LogError("Story Card Panel is not assigned in the Inspector!");
            return;
        }
        
        // Update UI text
        if (titleText != null)
            titleText.text = card.cardTitle;
        else
            Debug.LogWarning("Title Text is not assigned!");
        
        if (storyText != null)
            storyText.text = card.storyText;
        else
            Debug.LogWarning("Story Text is not assigned!");
        
        // Update image if available
         if (cardImage != null)
    {
        if (card.cardImage != null)
        {
            cardImage.sprite = card.cardImage;
            cardImage.gameObject.SetActive(true);  // Show image
        }
        else
        {
            cardImage.gameObject.SetActive(false); // Hide if no image
        }
    }
        
        // Show the panel
        storyCardPanel.SetActive(true);
        isCardActive = true;
        
        // Pause the game while card is showing
        Time.timeScale = 0f;
        
        Debug.Log($"Displaying story card: {card.cardTitle}");
    }
    
    public void CloseStoryCard()
    {
        if (storyCardPanel != null)
            storyCardPanel.SetActive(false);
        
        isCardActive = false;
        
        // Resume game
        Time.timeScale = 1f;
        
        Debug.Log("Story card closed - game resumed");
    }
    
    public bool IsCardActive()
    {
        return isCardActive;
    }
    
    // Optional: Get a random story text without showing the card
    public string GetRandomStoryText()
    {
        if (allStoryCards.Count > 0)
        {
            int randomIndex = Random.Range(0, allStoryCards.Count);
            return allStoryCards[randomIndex].storyText;
        }
        return "No stories available.";
    }
    
    // Optional: Get total number of cards
    public int GetTotalCardCount()
    {
        return allStoryCards.Count;
    }
}