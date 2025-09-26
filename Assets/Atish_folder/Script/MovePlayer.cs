using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class DragAnywhereXZ : MonoBehaviour
{
    [Header("Keeps the piece at this Y height")]
    public float yOffset = 0f;

    [Header("Optional")]
    public bool snapToGrid = false;
    public float gridSize = 1f;
    public float followLerp = 25f; // higher = snappier

    Plane _plane;            // infinite XZ plane at yOffset
    Vector3 _grabOffsetXZ;   // keeps cursor-relative offset
    bool _dragging;

    void Awake()
    {
        if (Mathf.Approximately(yOffset, 0f))
            yOffset = transform.position.y;

        _plane = new Plane(Vector3.up, new Vector3(0f, yOffset, 0f));
    }

    void OnValidate()
    {
        _plane = new Plane(Vector3.up, new Vector3(0f, yOffset, 0f));
    }

    void OnMouseDown()
    {
        if (TryGetPointOnPlane(GetMousePos(), out var p))
        {
            _grabOffsetXZ = new Vector3(transform.position.x - p.x, 0f, transform.position.z - p.z);
            _dragging = true;
        }
    }

    void OnMouseDrag()
    {
        if (!_dragging) return;
        if (!TryGetPointOnPlane(GetMousePos(), out var p)) return;

        Vector3 target = new Vector3(p.x, yOffset, p.z) + _grabOffsetXZ;

        if (snapToGrid && gridSize > 0f)
        {
            target.x = Mathf.Round(target.x / gridSize) * gridSize;
            target.z = Mathf.Round(target.z / gridSize) * gridSize;
        }

        float t = 1f - Mathf.Exp(-followLerp * Time.deltaTime);
        transform.position = Vector3.Lerp(transform.position, target, t);
    }

    void OnMouseUp() => _dragging = false;

    bool TryGetPointOnPlane(Vector3 screenPos, out Vector3 world)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        if (_plane.Raycast(ray, out float enter))
        { world = ray.GetPoint(enter); return true; }
        world = default; return false;
    }

    Vector3 GetMousePos()
    {
    #if ENABLE_INPUT_SYSTEM
        return Mouse.current != null ? (Vector3)Mouse.current.position.ReadValue() : Input.mousePosition;
    #else
        return Input.mousePosition;
    #endif
    }
}
