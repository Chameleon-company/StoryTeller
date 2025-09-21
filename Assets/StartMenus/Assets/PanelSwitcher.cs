using System.Collections.Generic;
using UnityEngine;

public class PanelSwitcher : MonoBehaviour
{
    [System.Serializable] public class NamedPanel { public string name; public GameObject panel; }
    public List<NamedPanel> panels;

    Dictionary<string, GameObject> map;

    void Awake()
    {
        map = new Dictionary<string, GameObject>();
        foreach (var p in panels)
        {
            if (p != null && p.panel != null) map[p.name] = p.panel;
        }
        ShowPanel("Main"); // default screen
    }

    public void ShowPanel(string panelName)
    {
        foreach (var kv in map)
            kv.Value.SetActive(kv.Key == panelName);
    }
}
