using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PickDragDrop : MonoBehaviour
{
    [Header("Cursor Settings")]
    public Texture2D handCursor;      // cursor when hovering
    public Texture2D grabCursor;      // cursor when dragging
    public Vector2 cursorHotspot = new Vector2(8, 4);
    public CursorMode cursorMode = CursorMode.Auto;

    [Header("Lift Effect")]
    public float baseY = 0f;          // ground height
    public float liftHeight = 0.25f;  // how high it lifts when dragging
    public float liftTime = 0.12f;    // how fast the lift happens
    public float scaleWhileHeld = 1.06f; // scale up while dragging
    public GameObject selectRing;     // highlight ring (optional)

    [Header("Movement Settings")]
    public float followSpeed = 25f;   // how fast it follows the mouse
    public bool snapToGrid = false;
    public float gridSize = 1f;

    private Plane groundPlane;        // imaginary ground to drag across
    private Vector3 grabOffset;       // offset when picking up
    private Vector3 originalScale;
    private bool isDragging = false;
    private bool isHovering = false;
    private Coroutine liftRoutine;

    void Awake()
    {
        originalScale = transform.localScale;
        if (Mathf.Approximately(baseY, 0f)) baseY = transform.position.y;
        groundPlane = new Plane(Vector3.up, new Vector3(0f, baseY, 0f));
    }

    void Update()
    {
        HandleHover();

        // --- Start dragging (Left Mouse Button Down) ---
        if (!isDragging && isHovering && LeftMouseDown() && !IsOverUI() &&
            TryGetGroundPoint(GetMousePosition(), out var point))
        {
            grabOffset = new Vector3(transform.position.x - point.x, 0f, transform.position.z - point.z);
            isDragging = true;

            if (selectRing) selectRing.SetActive(true);

            SetCursor(grabCursor ? grabCursor : handCursor);
            StartLift(true);

            Debug.Log("Started dragging: " + gameObject.name);
        }

        if (!isDragging) return;

        // --- Stop dragging (Left Mouse Button Up) ---
        if (LeftMouseUp() || !LeftMouseHeld())
        {
            isDragging = false;
            if (selectRing) selectRing.SetActive(false);
            StartLift(false);

            if (isHovering) SetCursor(handCursor); else ResetCursor();

            Debug.Log("Stopped dragging: " + gameObject.name);
            return;
        }

        // --- Update position while dragging ---
        if (!TryGetGroundPoint(GetMousePosition(), out var hit)) return;

        Vector3 target = new Vector3(hit.x, baseY + liftHeight, hit.z) + grabOffset;

        if (snapToGrid && gridSize > 0f)
        {
            target.x = Mathf.Round(target.x / gridSize) * gridSize;
            target.z = Mathf.Round(target.z / gridSize) * gridSize;
        }

        float t = 1f - Mathf.Exp(-followSpeed * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target, t);
    }

    // --- Hover detection with raycast ---
    void HandleHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (!isHovering)
                {
                    isHovering = true;
                    if (!isDragging) SetCursor(handCursor);
                    Debug.Log("👆 Hovering over: " + gameObject.name);
                }
            }
            else if (isHovering)
            {
                isHovering = false;
                if (!isDragging) ResetCursor();
            }
        }
        else if (isHovering)
        {
            isHovering = false;
            if (!isDragging) ResetCursor();
        }
    }

    // --- Lift effect (animation) ---
    void StartLift(bool up)
    {
        if (liftRoutine != null) StopCoroutine(liftRoutine);
        liftRoutine = StartCoroutine(LiftAnimation(up));
    }

    IEnumerator LiftAnimation(bool up)
    {
        float startY = transform.position.y;
        float endY = baseY + (up ? liftHeight : 0f);

        Vector3 startScale = transform.localScale;
        Vector3 endScale = originalScale * (up ? scaleWhileHeld : 1f);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / Mathf.Max(0.01f, liftTime);
            float smoothT = Mathf.SmoothStep(0, 1, t);

            Vector3 pos = transform.position;
            pos.y = Mathf.Lerp(startY, endY, smoothT);
            transform.position = pos;

            transform.localScale = Vector3.Lerp(startScale, endScale, smoothT);
            yield return null;
        }

        Vector3 finalPos = transform.position;
        finalPos.y = endY;
        transform.position = finalPos;
        transform.localScale = endScale;
    }

    // --- Helpers ---
    bool TryGetGroundPoint(Vector3 screen, out Vector3 world)
    {
        Ray ray = Camera.main.ScreenPointToRay(screen);
        if (groundPlane.Raycast(ray, out float enter))
        {
            world = ray.GetPoint(enter);
            return true;
        }
        world = default;
        return false;
    }

    static bool IsOverUI() => EventSystem.current && EventSystem.current.IsPointerOverGameObject();

    Vector3 GetMousePosition()
    {
        return Input.mousePosition; // using old input system for simplicity
    }

    bool LeftMouseDown() => Input.GetMouseButtonDown(0);
    bool LeftMouseHeld() => Input.GetMouseButton(0);
    bool LeftMouseUp() => Input.GetMouseButtonUp(0);

    void SetCursor(Texture2D tex) => Cursor.SetCursor(tex, cursorHotspot, cursorMode);
    void ResetCursor() => Cursor.SetCursor(null, Vector2.zero, cursorMode);
}
