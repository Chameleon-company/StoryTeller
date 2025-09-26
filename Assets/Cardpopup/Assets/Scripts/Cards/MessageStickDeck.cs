using System.Collections.Generic;
using UnityEngine;

public class MessageStickDeck : MonoBehaviour
{
    [Header("Load from Resources/Cards at runtime (recommended)")]
    [SerializeField] private bool loadFromResources = true;

    [Tooltip("If not loading from Resources, drag cards in here in the Inspector.")]
    [SerializeField] private List<MessageStickCard> deckList = new List<MessageStickCard>();

    private Queue<MessageStickCard> drawPile;
    private List<MessageStickCard> discard = new List<MessageStickCard>();

    void Awake()
    {
        var source = new List<MessageStickCard>();
        if (loadFromResources)
            source.AddRange(Resources.LoadAll<MessageStickCard>("Cards"));
        else
            source.AddRange(deckList);

        Shuffle(source);
        drawPile = new Queue<MessageStickCard>(source);
    }

    public MessageStickCard Draw()
    {
        if (drawPile.Count == 0)
            ReshuffleFromDiscard();
        return drawPile.Count > 0 ? drawPile.Dequeue() : null;
    }

    public void Discard(MessageStickCard card)
    {
        if (card != null) discard.Add(card);
    }

    private void ReshuffleFromDiscard()
    {
        if (discard.Count == 0) return;
        Shuffle(discard);
        drawPile = new Queue<MessageStickCard>(discard);
        discard = new List<MessageStickCard>();
    }

    private void Shuffle(List<MessageStickCard> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}