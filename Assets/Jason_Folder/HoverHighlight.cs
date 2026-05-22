using UnityEngine;

public class HoverHighlight : MonoBehaviour
{
    public Material glowMaterial;
    private Material defaultMaterial;
    private Renderer rend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<Renderer>();
        defaultMaterial = rend.material;
    }

    void OnMouseEnter()
    {
        rend.material = glowMaterial;
    }

    void OnMouseExit()
    {
        rend.material = defaultMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
