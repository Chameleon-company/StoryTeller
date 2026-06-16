using UnityEngine;

[CreateAssetMenu(menuName = "Storyteller/Artifact Definition")]
public class ArtifactDefinition : ScriptableObject
{
    public string artifactId;
    public string displayName;
    public GameObject artifactPrefab;
    [TextArea] public string description;
}
