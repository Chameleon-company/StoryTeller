using UnityEngine;

[CreateAssetMenu(menuName="Storyteller/Hotspot Settings")]
public class HotspotSettings : ScriptableObject {
    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color hoverColor  = Color.yellow;

    [Header("Pulse")]
    public bool enablePulse = true;
    public float pulseSpeed = 5f;   // higher = faster
    public float pulseScale = 0.2f; 

    [Header("Glow")]
    public bool enableGlow = true;
    public float glowIntensity = 1.5f;  // emission multiplier

    [Header("Particles")]
    public bool enableParticles = true;
    public GameObject particlePrefab;   // ClickSparkle.prefab
}
// Description:
// HotspotSettings is a ScriptableObject that centralises all configurable 
// visual and interactive effects for hotspot objects in the Storyteller system. 
// Designers can adjust the hotspot’s appearance (normal/hover colours), 
// enable or disable pulsing scale animation, glow intensity, and particle effects 
// without modifying code. 
//
// Usage:
// - Create an instance via the Unity menu: Assets > Create > Storyteller > Hotspot Settings
// - Assign it to any hotspot-related script (e.g. WaterholeHotspot).
// - Tune colour states, pulse behaviour, glow emission, and optional particle prefab.
// This allows consistent styling and behaviour across multiple hotspots while keeping 
// values easy to tweak directly in the Inspector.