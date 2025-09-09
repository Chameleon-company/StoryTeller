using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Collider))]
public class WaterholeHotspot :
    MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public WaterholeData data;           // ScriptableObject with title/desc/url/audio/image
    public HotspotSettings settings;     // ScriptableObject with color/pulse/glow/particles
    public AudioClip clickSound;         // optional click sound

    private Renderer rend;
    private AudioSource sfx;             // AudioSource on Camera/Manager
    private Vector3 baseScale;
    private bool hovering;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        baseScale = transform.localScale;

        // find an AudioSource on Main Camera
        var cam = Camera.main;
        if (cam) sfx = cam.GetComponent<AudioSource>();

        // set starting color
        if (rend && settings != null)
        {
            var m = rend.material;
            m.color = settings.normalColor;
            m.EnableKeyword("_EMISSION");
            m.SetColor("_EmissionColor", Color.black);
        }
    }

    public void OnPointerClick(PointerEventData e)
    {
        // play click sound
        if (clickSound && sfx) sfx.PlayOneShot(clickSound);

        // particle effect
        if (settings && settings.enableParticles && settings.particlePrefab)
        {
            Instantiate(settings.particlePrefab, transform.position + Vector3.up * 0.1f, Quaternion.identity);
        }

        // show popup
        if (data != null && PopupUI.I != null)
        {
            PopupUI.I.Show(
                data.title,
                data.description,
                data.learnMoreURL,
                data.audioClip,
                data.infoImage
            );
        }
    }

public void OnPointerEnter(PointerEventData e) {
    hovering = true;
    if (settings != null) {
        rend.material.color = settings.hoverColor;
        if (settings.enableGlow) {
            rend.material.SetColor("_EmissionColor", settings.hoverColor * settings.glowIntensity);
        }
    }

    // show tooltip
    if (TooltipUI.I != null && data != null) {
        TooltipUI.I.Show(data.title, transform.position + Vector3.up * 0.5f);
    }
}

public void OnPointerExit(PointerEventData e) {
    hovering = false;
    rend.material.color = settings.normalColor;
    rend.material.SetColor("_EmissionColor", Color.black);
    transform.localScale = baseScale;

    // hide tooltip
    if (TooltipUI.I != null) TooltipUI.I.Hide();
}


    void Update()
    {
        // pulse effect while hovering
        if (settings != null && settings.enablePulse && hovering)
        {
            float t = (Mathf.Sin(Time.time * settings.pulseSpeed) + 1f) * 0.5f; // 0..1
            float s = Mathf.Lerp(1f, settings.pulseScale, t);
            transform.localScale = baseScale * s;
        }
    }
    
    
    
}
