using UnityEngine;

[CreateAssetMenu(fileName = "NewMessageStickCard", menuName = "Storyteller/Message Stick Card")]
public class MessageStickCard : ScriptableObject
{
    [SerializeField] private string cardId;      // optional unique id
    [SerializeField] private string title;
    [TextArea(3, 12)][SerializeField] private string body;
    [SerializeField] private Sprite illustration; // optional image
    [SerializeField] private AudioClip narration; // optional voiceover

    public string CardId => cardId;
    public string Title => title;
    public string Body => body;
    public Sprite Illustration => illustration;
    public AudioClip Narration => narration;
}