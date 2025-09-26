using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Put this on a board tile (cube or sprite with a Collider/Collider2D).
/// Requires: EventSystem in the scene + PhysicsRaycaster (3D) OR Physics2DRaycaster (2D) on the Camera.
/// On click, draws a card from MessageStickDeck and shows it in CardPopup.
/// </summary>
public class MessageStickTile : MonoBehaviour, IPointerClickHandler
{
    [Header("References")]
    [SerializeField] private MessageStickDeck deck;   // assign the scene's MessageStickDeck
    [SerializeField] private CardPopup popup;         // assign the CardPopup in the scene

    [Header("Behaviour")]
    [Tooltip("If true, only the first click after landing will draw a card until you reset via OnPlayerLanded().")]
    [SerializeField] private bool oneShotPerLanding = true;

    [Tooltip("Optional: block rapid double-clicks while popup is animating.")]
    [SerializeField] private bool debounceClicks = true;

    [Tooltip("Optional SFX on click (before popup).")]
    [SerializeField] private AudioSource clickSfx;

    private bool used;
    private bool busy;

    /// <summary>
    /// Unity UI click handler fired when this object is clicked in Game view.
    /// </summary>
    public void OnPointerClick(PointerEventData eventData)
    {
        // Must click with primary button and not through UI masking (EventSystem handles that).
        if (eventData.button != PointerEventData.InputButton.Left) return;

        if (debounceClicks && busy) return;
        if (oneShotPerLanding && used) return;

        if (deck == null || popup == null)
        {
            Debug.LogWarning($"[MessageStickTile] Missing reference(s). Deck:{deck != null}, Popup:{popup != null}", this);
            return;
        }

        var card = deck.Draw();
        if (card == null)
        {
            Debug.LogWarning("[MessageStickTile] No card drawn (empty deck?).", this);
            return;
        }

        if (clickSfx) clickSfx.Play();

        if (debounceClicks) busy = true;
        used = true;

        // Show the popup; CardPopup handles dimmer, flip, etc.
        popup.ShowCard(card);

        // Small delay to avoid spam (optional safeguard). You can remove if not needed.
        if (debounceClicks) StartCoroutine(ClearBusySoon(0.25f));
    }

    /// <summary>
    /// Call this from your turn/board manager when a player newly lands on this tile.
    /// Resets the one-shot gate for the next allowed draw.
    /// </summary>
    public void OnPlayerLanded()
    {
        used = false;
    }

    /// <summary>
    /// Convenience setters if you spawn tiles at runtime.
    /// </summary>
    public void SetDeck(MessageStickDeck d) => deck = d;
    public void SetPopup(CardPopup p) => popup = p;

    private System.Collections.IEnumerator ClearBusySoon(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        busy = false;
    }

#if UNITY_EDITOR
    // Nice editor gizmo to see clickable tiles
    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.7f, 0.2f, 0.35f);
        var col3D = GetComponent<Collider>();
        var col2D = GetComponent<Collider2D>();
        if (col3D) Gizmos.DrawCube(col3D.bounds.center, col3D.bounds.size);
        else if (col2D) Gizmos.DrawCube(col2D.bounds.center, col2D.bounds.size);
    }
#endif
}