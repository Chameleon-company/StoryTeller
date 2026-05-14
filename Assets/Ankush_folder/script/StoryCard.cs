using UnityEngine;

[System.Serializable]
public class StoryCard
{
    public string cardTitle;      // Name of the story
    public string storyText;      // The actual story content
    public Sprite cardImage;      // Optional image for the card
    
    // Constructor to create a new story card
    public StoryCard(string title, string text, Sprite image = null)
    {
        cardTitle = title;
        storyText = text;
        cardImage = image;
    }
}