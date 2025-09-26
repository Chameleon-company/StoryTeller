using UnityEngine;

[CreateAssetMenu(menuName = "Storyteller/Waterhole Hover Data")]
public class WaterholeHoverData : ScriptableObject
{
    public string title;
    [TextArea(2, 6)] public string description;
    public Sprite icon;            
    public Color backgroundColor = Color.white; 
}
/// <summary>
/// WaterholeHoverData is a ScriptableObject that stores the lightweight 
/// content shown when a user hovers over a waterhole hotspot in the Storyteller system.  
///
/// Features:
/// - **Title & Description**: Short text content to display in a hover tooltip.  
/// - **Icon**: Optional sprite to visually represent the hotspot in hover UI.  
/// - **Background Color**: Customizable color to theme the hover panel.  
///
/// Usage:
/// - Create new instances via: Assets > Create > Storyteller > Waterhole Hover Data.  
/// - Fill in the text, icon, and background color in the Inspector.  
/// - Reference this asset from hotspot hover scripts to dynamically populate tooltip UI.  
/// </summary>
