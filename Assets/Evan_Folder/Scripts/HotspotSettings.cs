using UnityEngine;

[CreateAssetMenu(menuName="Storyteller/Hotspot Settings")]
public class HotspotSettings : ScriptableObject {
    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color hoverColor  = Color.yellow;

    [Header("Pulse")]
    public bool enablePulse = true;
    public float pulseSpeed = 5f;   // higher = faster
    public float pulseScale = 0.2f; // 8% bigger at peak

    [Header("Glow")]
    public bool enableGlow = true;
    public float glowIntensity = 1.5f;  // emission multiplier

    [Header("Particles")]
    public bool enableParticles = true;
    public GameObject particlePrefab;   // ClickSparkle.prefab
}
