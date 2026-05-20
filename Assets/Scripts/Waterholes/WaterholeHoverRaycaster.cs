using UnityEngine;
using UnityEngine.InputSystem;

public class WaterholeHoverRaycaster : MonoBehaviour
{
    [Header("Raycast Settings")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float rayDistance = 1000f;

    [Header("Cursor Settings")]
    [SerializeField] private bool hideCursorOnHover = true;

    private WaterholeHoverTarget currentTarget;
    private bool isHoveringWaterhole;

    private void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        ShowCursor();
    }

    private void Update()
    {
        CheckHoverTarget();
    }

    private void CheckHoverTarget()
    {
        if (mainCamera == null || Mouse.current == null)
        {
            ClearCurrentTarget();
            ShowCursor();
            return;
        }

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = mainCamera.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, rayDistance))
        {
            WaterholeHoverTarget target = hitInfo.collider.GetComponent<WaterholeHoverTarget>();

            if (target != null)
            {
                isHoveringWaterhole = true;

                if (currentTarget != target)
                {
                    ClearCurrentTarget();

                    currentTarget = target;
                    currentTarget.HoverEnter();
                }

                HideCursor();
                return;
            }
        }

        isHoveringWaterhole = false;
        ClearCurrentTarget();
        ShowCursor();
    }

    private void ClearCurrentTarget()
    {
        if (currentTarget != null)
        {
            currentTarget.HoverExit();
            currentTarget = null;
        }

        if (WaterholeTooltipManager.Instance != null)
        {
            WaterholeTooltipManager.Instance.HideTooltip();
        }
    }

    private void HideCursor()
    {
        if (!hideCursorOnHover)
        {
            return;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ShowCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnDisable()
    {
        ShowCursor();
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            ShowCursor();
        }
        else if (isHoveringWaterhole)
        {
            HideCursor();
        }
    }
}
