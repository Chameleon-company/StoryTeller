using UnityEngine;

[CreateAssetMenu(menuName = "Storyteller/Waterhole Hover Data")]
public class WaterholeHoverData : ScriptableObject
{
    public string title;
    [TextArea(2, 6)] public string description;
    public Sprite icon;            
    public Color backgroundColor = Color.white; 
}
