using UnityEngine;

public class CollectArtefact : MonoBehaviour
{
    public string artefactName;
    
    void OnMouseDown()
    {
        Debug.Log("Collected: " + artefactName);
        gameObject.SetActive(false);
    }
}