using UnityEngine;

[CreateAssetMenu(menuName="Storyteller/Waterhole Data")]
public class WaterholeData : ScriptableObject
{
    public string title;
    [TextArea(3,8)] public string description;
    public string learnMoreURL;
    public AudioClip audioClip;
    public Sprite infoImage; 
}
/// <summary>
/// WaterholeData is a ScriptableObject that stores the content shown 
/// when a hotspot (e.g., a waterhole) is clicked in the Storyteller system.  
///
/// Features:
/// - **Title & Description**: Basic text fields with multi-line support for descriptions.  
/// - **Learn More URL**: An optional external link that can be opened from the UI.  
/// - **Audio Clip**: Optional narration or sound effect that can be played in the popup.  
/// - **Info Image**: Optional sprite to visually represent the waterhole or related content.  
///
/// Usage:
/// - Create new instances via the Unity menu:  
///   `Assets > Create > Storyteller > Waterhole Data`.  
/// - Assign text, image, audio, and URL in the Inspector.  
/// - Reference this ScriptableObject from hotspot scripts (e.g., `WaterholeHotspot`) 
///   to drive popup content dynamically.  
/// </summary>
