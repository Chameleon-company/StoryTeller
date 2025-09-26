using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider), typeof(Renderer))]
public class WaterholeHotspot :
    MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public WaterholeData data;
    public HotspotSettings settings;
    public AudioClip clickSound;
public string title;
public string description;
    Renderer rend;
    AudioSource sfx;
    Vector3 baseScale;
    bool hovering;

    // cache one MPB per renderer
    MaterialPropertyBlock mpb;

    void Awake() {
        rend = GetComponent<Renderer>();
        baseScale = transform.localScale;
        mpb = new MaterialPropertyBlock();

        var cam = Camera.main;
        if (cam) sfx = cam.GetComponent<AudioSource>();

        if (settings != null) {
            // set base color & no emission via MPB
            rend.GetPropertyBlock(mpb);
            mpb.SetColor("_Color", settings.normalColor);
            mpb.SetColor("_EmissionColor", Color.black);
            rend.SetPropertyBlock(mpb);
        }
    }

    public void OnPointerClick(PointerEventData e) {
        if (clickSound && sfx) sfx.PlayOneShot(clickSound);

        if (settings && settings.enableParticles && settings.particlePrefab)
            Instantiate(settings.particlePrefab, transform.position + Vector3.up * 0.1f, Quaternion.identity);

        if (data != null && PopupUI.I != null)
            PopupUI.I.Show(data.title, data.description, data.learnMoreURL, data.audioClip, data.infoImage);
    }

    public void OnPointerEnter(PointerEventData e) {
        hovering = true;
        if (settings == null) return;

        rend.GetPropertyBlock(mpb);
        mpb.SetColor("_Color", settings.hoverColor);
        if (settings.enableGlow)
            mpb.SetColor("_EmissionColor", settings.hoverColor * settings.glowIntensity);
        rend.SetPropertyBlock(mpb);
    }

    public void OnPointerExit(PointerEventData e) {
        hovering = false;
        if (settings == null) return;

        rend.GetPropertyBlock(mpb);
        mpb.SetColor("_Color", settings.normalColor);
        mpb.SetColor("_EmissionColor", Color.black);
        rend.SetPropertyBlock(mpb);

        transform.localScale = baseScale;
    }

    void Update() {
        if (settings != null && settings.enablePulse && hovering) {
            float t = (Mathf.Sin(Time.time * settings.pulseSpeed) + 1f) * 0.5f;
            float s = Mathf.Lerp(1f, 1f + settings.pulseScale, t); // pulseScale = 0.08 → +8%
            transform.localScale = baseScale * s;
        }
    }
}
/// <summary>
/// WaterholeHotspot makes an object in the scene interactive as a "hotspot."  
/// When hovered or clicked, it visually responds and can trigger UI content.  
///
/// Features:
/// - **Click Handling**: Plays an optional click sound, spawns particle effects, 
///   and opens a popup (via PopupUI) showing data from a linked WaterholeData asset.  
/// - **Hover Effects**: Changes the object’s color, applies glow emission, 
///   and can pulse its scale while the cursor is over it.  
/// - **Configurable via HotspotSettings**: All visual effects (colors, pulse, glow, particles) 
///   are controlled by a ScriptableObject so multiple hotspots can share consistent styling.  
/// - **Data-Driven Content**: Pulls title, description, audio, image, and URL from a WaterholeData asset.  
///
/// Usage:
/// - Attach this script to any GameObject with a Collider and Renderer.  
/// - Assign a WaterholeData asset (for popup content) and a HotspotSettings asset (for visuals).  
/// - Optionally assign a click sound.  
/// - On click, the hotspot displays its linked content in the popup UI.  
/// </summary>
