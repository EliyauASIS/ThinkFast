using UnityEngine;
using UnityEngine.InputSystem;

public class ArrowMovement : MonoBehaviour
{
    public float lerpSpeed = 18f;        // גבוה יותר = מהיר יותר
    public float minSwipeDistance = 50f;
    public float moveDistance = 600f;

    private RectTransform _rect;
    private Vector2 _target;
    private bool _moving = false;

    private Vector2 _touchStart;
    private bool _pressing = false;

    public bool canMove = true;

    void Start()
    {
        _rect = GetComponent<RectTransform>();
        _target = _rect.anchoredPosition;
    }

    void Update()
    {
        HandleTouchSwipe();

        if (_moving && canMove)
        {
            _rect.anchoredPosition = Vector2.Lerp(
                _rect.anchoredPosition,
                _target,
                lerpSpeed * Time.deltaTime
            );

            if (Vector2.Distance(_rect.anchoredPosition, _target) < 0.5f)
            {
                _rect.anchoredPosition = _target;
                _moving = false;
                canMove = false;
            }
        }
    }

    void HandleTouchSwipe()
    {
        if (Touchscreen.current == null) return;

        var touch = Touchscreen.current.primaryTouch;
        Vector2 currentPos = touch.position.ReadValue();

        if (touch.press.wasPressedThisFrame)
        {
            _touchStart = currentPos;
            _pressing = true;
        }

        if (touch.press.wasReleasedThisFrame && _pressing && !_moving && canMove)
        {
            _pressing = false;
            Vector2 delta = currentPos - _touchStart;

            if (delta.magnitude < minSwipeDistance) return;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                _target = _rect.anchoredPosition + new Vector2(Mathf.Sign(delta.x) * moveDistance, 0f);
            else
                _target = _rect.anchoredPosition + new Vector2(0f, Mathf.Sign(delta.y) * moveDistance);

            _moving = true;
        }
    }
}