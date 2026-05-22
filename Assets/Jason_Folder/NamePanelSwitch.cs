using UnityEngine;

public class NamePanelSwitch : MonoBehaviour
{
    public GameObject NamePanel;
    public GameObject LobbyPanel;

    public void SwitchToNamePanel()
    {
        LobbyPanel.SetActive(false);
        NamePanel.SetActive(true);
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
