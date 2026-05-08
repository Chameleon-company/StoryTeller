using UnityEngine;
using TMPro;

public class TooltipUI : MonoBehaviour
{
    public static TooltipUI I;

    [Header("UI References")]
    public GameObject panel;
    public TMP_Text titleText;
    public TMP_Text bodyText;
    public CanvasGroup canvasGroup;

    [Header("Layout Mode")]
    public bool useFixedScreenPosition = true;

    [Range(0f, 1f)]
    public float fixedX = 0.5f;

    [Range(0f, 1f)]
    public float fixedY = 0.16f;

    [Header("Follow Target Settings")]
    public Vector3 screenOffset = new Vector3(80f, 100f, 0f);

    [Header("Animation")]
    public float fadeSpeed = 8f;

    private Camera mainCam;
    private Transform currentTarget;
    private Vector3 currentWorldOffset;
    private bool isVisible;

    private void Awake()
    {
        I = this;
        mainCam = Camera.main;

        if (panel != null)
        {
            panel.SetActive(false);
        }

        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0f;
        }
    }

    private void Update()
    {
        if (panel == null || canvasGroup == null) return;

        if (panel.activeSelf)
        {
            UpdatePanelPosition();

            float targetAlpha = isVisible ? 1f : 0f;
            canvasGroup.alpha = Mathf.MoveTowards(
                canvasGroup.alpha,
                targetAlpha,
                fadeSpeed * Time.unscaledDeltaTime
            );

            if (!isVisible && canvasGroup.alpha <= 0.01f)
            {
                panel.SetActive(false);
                currentTarget = null;
            }
        }
    }

    public void Show(string title, string body, Transform target, Vector3 worldOffset)
    {
        if (panel == null || titleText == null || bodyText == null || canvasGroup == null)
        {
            Debug.LogWarning("TooltipUI is missing references.");
            return;
        }

        titleText.text = title;
        bodyText.text = body;

        currentTarget = target;
        currentWorldOffset = worldOffset;

        if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }

        isVisible = true;
        UpdatePanelPosition();
    }

    public void Hide(Transform requester = null)
    {
        if (!useFixedScreenPosition && requester != null && requester != currentTarget)
        {
            return;
        }

        isVisible = false;
    }

    private void UpdatePanelPosition()
    {
        if (panel == null) return;

        if (useFixedScreenPosition)
        {
            panel.transform.position = new Vector3(
                Screen.width * fixedX,
                Screen.height * fixedY,
                0f
            );
            return;
        }

        if (currentTarget == null) return;

        if (mainCam == null)
        {
            mainCam = Camera.main;
            if (mainCam == null) return;
        }

        Vector3 worldPos = currentTarget.position + currentWorldOffset;
        Vector3 screenPos = mainCam.WorldToScreenPoint(worldPos);

        if (screenPos.z > 0f)
        {
            panel.transform.position = screenPos + screenOffset;
        }
        else
        {
            Hide();
        }
    }
}