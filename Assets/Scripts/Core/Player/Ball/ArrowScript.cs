using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public int arrowColorID = 0;
    private RectTransform _rt;
    private PlatformScript[] platforms;
    private bool _isHandled = false;

    void Start()
    {
        _rt = GetComponent<RectTransform>();
        platforms = FindObjectsByType<PlatformScript>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (_isHandled) return;

        foreach (PlatformScript platform in platforms)
        {
            RectTransform platformRT = platform.GetComponent<RectTransform>();
            if (platformRT == null) continue;

            if (GetWorldRect(_rt).Overlaps(GetWorldRect(platformRT)))
            {
                if (platform.platformColorID == arrowColorID)
                {
                    _isHandled = true;
                    ArrowSpawner.instance.SpawnBall();
                    PointsManager.instance.AddPoint();
                }
                else
                {
                    GameManager.instance.GameOver();

                }
                gameObject.SetActive(false);
                ArrowSizeReduction reduction = GetComponent<ArrowSizeReduction>();
                if (reduction != null)
                {
                    reduction.StopCoroutine(reduction.ReduceSizeCoroutine);
                }
                CanvasShake.instance.Shake();
                Destroy(gameObject);
            }
        }
    }

    Rect GetWorldRect(RectTransform rt)
    {
        Vector3[] corners = new Vector3[4];
        rt.GetWorldCorners(corners);

        return new Rect(
            corners[0].x, corners[0].y,
            corners[2].x - corners[0].x,
            corners[2].y - corners[0].y
        );
    }
}