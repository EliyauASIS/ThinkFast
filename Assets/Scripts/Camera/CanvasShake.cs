using UnityEngine;
using System.Collections;

public class CanvasShake : MonoBehaviour
{
    public static CanvasShake instance;

    private RectTransform rect;
    private Coroutine shakeCoroutine;

    void Awake()
    {
        instance = this;
        rect = GetComponent<RectTransform>();
    }

    public void Shake(float duration = 0.3f, float magnitude = 20f)
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);
            rect.anchoredPosition = Vector2.zero;
        }

        shakeCoroutine = StartCoroutine(ShakeRoutine(duration, magnitude));
    }

    IEnumerator ShakeRoutine(float duration, float magnitude)
    {
        Vector2 originalPos = rect.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            rect.anchoredPosition = originalPos + new Vector2(x, y);

            elapsed += Time.deltaTime;
            yield return null;
        }

        rect.anchoredPosition = originalPos;
        shakeCoroutine = null;
    }
}