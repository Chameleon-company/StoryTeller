using UnityEngine;

public class WaterholeButton : MonoBehaviour
{
    public GameObject window;
    private GameProgress gp;

    void Start()
    {
        if (window != null)
        {
            window.SetActive(false);
        }

        // Cache GameProgress reference (Unity 6 safe)
        gp = FindFirstObjectByType<GameProgress>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    ToggleWindow();

                    // Save progress when waterhole is opened
                    if (gp != null)
                    {
                        gp.unlockedWaterholes += 1;
                        gp.lastScene = "WaterholeScene";
                        gp.SaveProgress();
                    }
                }
            }
        }
    }

    void ToggleWindow()
    {
        if (window != null)
        {
            bool isActive = window.activeSelf;
            window.SetActive(!isActive);
        }
    }
}
