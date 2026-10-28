
using UnityEngine;
using UnityEngine.UI;

public class BlinkUI : MonoBehaviour
{
    [SerializeField] private Image targetImage;
    [SerializeField] private float blinkSpeed = 2f;

    private Color originalColor;

    private void Start()
    {
        if (targetImage == null)
            targetImage = GetComponent<Image>();

        if (targetImage != null)
            originalColor = targetImage.color;
    }

    private void Update()
    {
        if (targetImage == null) return;

        float alpha = (Mathf.Sin(Time.time * blinkSpeed) + 1f) / 2f;

        Color color = originalColor;
        color.a = Mathf.Lerp(0.1f, 1f, alpha);

        targetImage.color = color;
    }
}
