using UnityEngine;
using UnityEngine.UI;

public class TestStoryButton : MonoBehaviour
{
    public StoryCardManager storyCardManager;
    private int cardIndex = 0;
    private string[] cardNames = { "The Creation", "The Rainbow Serpent", "First Sunrise", "Koala and the Kangaroo" };
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ShowNextCard);
    }
    
    void ShowNextCard()
    {
        storyCardManager.ShowStoryCard(cardNames[cardIndex]);
        cardIndex = (cardIndex + 1) % cardNames.Length;
    }
}