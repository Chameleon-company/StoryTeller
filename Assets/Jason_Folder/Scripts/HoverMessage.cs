using System.Collections;
using UnityEngine;

public class HoverMessage : MonoBehaviour
{
    [Header("Tooltip Content")]
    public string messageTitle = "Message Stick";

    [TextArea(2, 5)]
    public string messageBody = "This is a contextual StoryTeller tooltip message.";

    [Header("World Position Offset")]
    public Vector3 worldOffset = new Vector3(0f, 1.5f, 0f);

    [Header("Timing")]
    [Range(0f, 1f)]
    public float hoverDelay = 0.35f;

    [Range(0f, 0.5f)]
    public float hideDelay = 0.10f;

    private Coroutine showCoroutine;
    private Coroutine hideCoroutine;
    private bool isHovered;

    private void OnMouseEnter()
    {
        isHovered = true;

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
            hideCoroutine = null;
        }

        if (showCoroutine != null)
        {
            StopCoroutine(showCoroutine);
        }

        showCoroutine = StartCoroutine(ShowAfterDelay());
    }

    private void OnMouseExit()
    {
        isHovered = false;

        if (showCoroutine != null)
        {
            StopCoroutine(showCoroutine);
            showCoroutine = null;
        }

        if (hideCoroutine != null)
        {
            StopCoroutine(hideCoroutine);
        }

        hideCoroutine = StartCoroutine(HideAfterDelay());
    }

    private IEnumerator ShowAfterDelay()
    {
        yield return new WaitForSeconds(hoverDelay);

        if (isHovered && TooltipUI.I != null)
        {
            TooltipUI.I.Show(messageTitle, messageBody, transform, worldOffset);
        }

        showCoroutine = null;
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);

        if (!isHovered && TooltipUI.I != null)
        {
            TooltipUI.I.Hide(transform);
        }

        hideCoroutine = null;
    }
}