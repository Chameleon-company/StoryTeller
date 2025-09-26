using UnityEngine;

public class Gotolink : MonoBehaviour
{
    public string url = "https://www.thestoryteller.store/";

    public void OpenURL()
    {
        Application.OpenURL(url);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
