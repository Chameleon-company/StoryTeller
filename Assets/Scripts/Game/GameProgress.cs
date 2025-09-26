using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameProgress : MonoBehaviour
{
    [Header("Saved State")]
    public int unlockedWaterholes = 0;
    public string lastScene = "MainMenu";

    [Header("Autosave")]
    [Tooltip("Enable/disable periodic autosaving.")]
    public bool enableAutosave = true;

    [Tooltip("Seconds between autosaves (e.g., 120–180).")]
    [Range(30f, 600f)]
    public float autosaveIntervalSeconds = 150f;

    [Header("UI Messages (Optional)")]
    public UnityEvent<string> OnInfoMessage;

    private Coroutine _autosaveRoutine;

    private const string KEY_WATERHOLES = "UnlockedWaterholes";
    private const string KEY_LAST_SCENE  = "LastScene";

    // Load ASAP so other scripts see correct state
    private void Awake()
    {
        FastLoad(false);// show “No saved game found...” on first run
    }

    private void Start()
    {
        if (enableAutosave)
            StartCoroutine(StartAutosaveNextFrame());
    }

    private IEnumerator StartAutosaveNextFrame()
    {
        yield return null;
        if (enableAutosave && _autosaveRoutine == null)
            _autosaveRoutine = StartCoroutine(AutosaveLoop());
    }

    private void OnDisable()
    {
        if (_autosaveRoutine != null)
        {
            StopCoroutine(_autosaveRoutine);
            _autosaveRoutine = null;
        }
    }

    private void OnApplicationPause(bool paused)
    {
        if (paused) SafeSaveProgress("Pause");
    }

    private void OnApplicationQuit()
    {
        SafeSaveProgress("Quit");
    }

    // ---- Public API ----
    public void SaveProgress() => SafeSaveProgress("Manual");
    public void LoadProgress() => FastLoad(true);

    public void ResetProgress()
    {
        try
        {
            PlayerPrefs.DeleteKey(KEY_WATERHOLES);
            PlayerPrefs.DeleteKey(KEY_LAST_SCENE);
            PlayerPrefs.Save();
        }
        catch { /* keep silent to stay fast */ }

        unlockedWaterholes = 0;
        lastScene = "MainMenu";
        DeferToast("Game progress reset.");
    }

    public bool HasSave()
    {
        return PlayerPrefs.HasKey(KEY_WATERHOLES) || PlayerPrefs.HasKey(KEY_LAST_SCENE);
    }

    // ---- Internals ----
    private void FastLoad(bool showToastWhenNoSave)
    {
        if (!HasSave())
        {
            if (showToastWhenNoSave) DeferToast("No saved game found. Starting a new game.");
            // keep defaults
            return;
        }

        int uw = PlayerPrefs.GetInt(KEY_WATERHOLES, 0);
        string ls = PlayerPrefs.GetString(KEY_LAST_SCENE, "MainMenu");

        if (uw < 0) uw = 0;
        if (string.IsNullOrWhiteSpace(ls)) ls = "MainMenu";

        unlockedWaterholes = uw;
        lastScene = ls;

        DeferToast("Game progress loaded.");
    }

    private void SafeSaveProgress(string context)
    {
        if (unlockedWaterholes < 0) unlockedWaterholes = 0;
        if (string.IsNullOrWhiteSpace(lastScene)) lastScene = "MainMenu";

        try
        {
            PlayerPrefs.SetInt(KEY_WATERHOLES, unlockedWaterholes);
            PlayerPrefs.SetString(KEY_LAST_SCENE, lastScene);
            PlayerPrefs.Save();
            DeferToast($"Progress saved ({context}).");
        }
        catch
        {
            DeferToast($"Save failed ({context}).");
        }
    }

    private IEnumerator AutosaveLoop()
    {
        float interval = Mathf.Max(30f, autosaveIntervalSeconds);
        yield return new WaitForSeconds(interval);
        while (enabled && gameObject.activeInHierarchy)
        {
            SafeSaveProgress("Autosave");
            yield return new WaitForSeconds(interval);
        }
    }

    private void DeferToast(string msg)
    {
        if (OnInfoMessage == null || OnInfoMessage.GetPersistentEventCount() == 0) return;
        StartCoroutine(_DeferredToast(msg));
    }

    private IEnumerator _DeferredToast(string msg)
    {
        yield return null; // next frame to avoid blocking Awake/Start
        OnInfoMessage?.Invoke(msg);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            ResetProgress();
    }
}
