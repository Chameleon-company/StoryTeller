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
