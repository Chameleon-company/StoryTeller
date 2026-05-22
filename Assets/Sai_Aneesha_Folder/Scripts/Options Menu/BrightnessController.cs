using UnityEngine;
using UnityEngine.UI;
public class BrightnessController : MonoBehaviour
{
   public Slider brightnessSlider;
   public RawImage brightnessPanel;
   [Range(0f, 1f)]
   public float defaultBrightness = 0f;
   private const string BrightnessKey = "BrightnessValue_Final";
   void Start()
   {
       float savedValue = PlayerPrefs.GetFloat(BrightnessKey, defaultBrightness);
       brightnessSlider.minValue = 0f;
       brightnessSlider.maxValue = 0.4f;
       brightnessSlider.wholeNumbers = false;
       brightnessSlider.value = savedValue;
       brightnessSlider.onValueChanged.RemoveAllListeners();
       brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
       UpdateBrightness(savedValue);
   }
   public void UpdateBrightness(float value)
   {
       float normalized = value / brightnessSlider.maxValue;
       Color c = brightnessPanel.color;
       c.r = 0f;
       c.g = 0f;
       c.b = 0f;
       c.a = (1f - normalized) * 0.7f;
       brightnessPanel.color = c;
       PlayerPrefs.SetFloat(BrightnessKey, value);
       PlayerPrefs.Save();
   }
}
