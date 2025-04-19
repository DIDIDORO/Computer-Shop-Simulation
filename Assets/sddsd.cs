using UnityEngine;
using UnityEngine.UI;

public class ScreenDarknessSlider : MonoBehaviour
{
    public Slider brightnessSlider;
    public RawImage overlayImage;

    // Максимальна альфа, яку ми дозволяємо (200 / 255 ≈ 0.78f)
    private float maxAlpha = 200f / 255f;

    void Start()
    {
        brightnessSlider.minValue = 0f;
        brightnessSlider.maxValue = 100f;
        brightnessSlider.value = 100f;

        brightnessSlider.onValueChanged.AddListener(OnSliderChanged);
        OnSliderChanged(brightnessSlider.value);
    }

    void OnSliderChanged(float value)
    {
        // Конвертуємо значення в alpha: 0% = maxAlpha, 100% = 0
        float alpha = Mathf.Lerp(maxAlpha, 0f, value / 100f);

        // Лог для контролю
        Debug.Log($"Slider: {value} → Alpha: {alpha}");

        Color color = overlayImage.color;
        color.a = alpha;
        overlayImage.color = color;
    }
}
