using UnityEngine;
using TMPro;

public class ArtifactPopupUI : MonoBehaviour
{
    [Header("UI")]
    public GameObject panel;
    public TMP_Text titleText;   // TextMeshPro
    public TMP_Text infoText;    // TextMeshPro

    [Header("3D Preview")]
    public Transform spawnPoint;

    private GameObject currentModel;

    public void ShowCollected(ArtifactDefinition artifact, int collectedCount, int requiredCount)
    {
        if (artifact == null) return;

        int remaining = Mathf.Max(0, requiredCount - collectedCount);

        if (titleText != null)
            titleText.text = "Collected: " + artifact.displayName;

        if (infoText != null)
        {
            if (remaining > 0)
                infoText.text = $"Collect {remaining} more artefact(s) to enter the Meeting Place. ({collectedCount}/{requiredCount})";
            else
                infoText.text = $"Meeting Place unlocked! ({collectedCount}/{requiredCount})";
        }

        ShowModel(artifact);
        panel.SetActive(true);
    }

    public void ShowGateLocked(int collectedCount, int requiredCount)
    {
        int remaining = Mathf.Max(0, requiredCount - collectedCount);

        if (titleText != null)
            titleText.text = "Meeting Place Locked";

        if (infoText != null)
            infoText.text = $"You need {remaining} more artefact(s). ({collectedCount}/{requiredCount})";

        ClearModel();
        panel.SetActive(true);
    }

    private void ShowModel(ArtifactDefinition artifact)
    {
        ClearModel();

        if (artifact.artifactPrefab != null && spawnPoint != null)
        {
            currentModel = Instantiate(artifact.artifactPrefab, spawnPoint);
            currentModel.transform.localPosition = Vector3.zero;
            currentModel.transform.localRotation = Quaternion.identity;
            currentModel.transform.localScale = Vector3.one * 0.2f;

            foreach (var c in currentModel.GetComponentsInChildren<Collider>())
                Destroy(c);
        }
    }

    private void ClearModel()
    {
        if (currentModel != null)
            Destroy(currentModel);
    }

    public void Close()
    {
        panel.SetActive(false);
    }
}
