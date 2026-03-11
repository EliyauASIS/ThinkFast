using UnityEngine;
using System.Collections;

public class ArrowSizeReduction : MonoBehaviour
{
    public float duration = 3f;

    private RectTransform _rect;
    private Vector2 _startSize;
    public Coroutine ReduceSizeCoroutine;
    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _startSize = _rect.sizeDelta;

        ReduceSizeCoroutine = StartCoroutine(ReduceSize());
    }

    IEnumerator ReduceSize()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = 1f - (elapsed / duration);

            _rect.sizeDelta = _startSize * t;

            yield return null;
        }
        ArrowSpawner.instance.canSpawn = false;
        GameManager.instance.GameOver();
        gameObject.SetActive(false);
        Destroy(gameObject);
    }
}