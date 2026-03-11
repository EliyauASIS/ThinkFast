using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class PlayerBall : MonoBehaviour
{
    [Header("Movement Settings")]
    public float followSpeed = 15f;
    public float smoothTime = 0.05f;

    [Header("Boundaries")]
    public float minY = -4.5f;
    public float maxY = 4.5f;
    public float minX = -8f;
    public float maxX = 8f;

    private Vector3 _targetPosition;
    private Vector3 _velocity = Vector3.zero;
    private Camera _cam;

    void OnEnable()
    {
        // הפעל Enhanced Touch לתמיכה במובייל
        EnhancedTouchSupport.Enable();
    }

    void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Start()
    {
        _cam = Camera.main;
        _targetPosition = transform.position;
    }

    void Update()
    {
        HandleInput();
        MoveToTarget();
    }

    void HandleInput()
    {
        Vector3 inputPos = Vector3.zero;
        bool hasInput = false;

        // מובייל - New Input System
        if (Touch.activeTouches.Count > 0)
        {
            Vector2 touchPos = Touch.activeTouches[0].screenPosition;
            inputPos = _cam.ScreenToWorldPoint(touchPos);
            hasInput = true;
        }
        // עכבר - לטסטים בעורך
        else if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            Vector2 mousePos = Mouse.current.position.ReadValue();
            inputPos = _cam.ScreenToWorldPoint(mousePos);
            hasInput = true;
        }

        if (hasInput)
        {
            inputPos.z = 0f;

            inputPos.x = Mathf.Clamp(inputPos.x, minX, maxX);
            inputPos.y = Mathf.Clamp(inputPos.y, minY, maxY);

            _targetPosition = inputPos;
        }
    }

    void MoveToTarget()
    {
        transform.position = Vector3.SmoothDamp(
            transform.position,
            _targetPosition,
            ref _velocity,
            smoothTime,
            followSpeed
        );
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("SpawnedBall"))
        {
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 pushDirection = col.contacts[0].normal * -1f;
                float pushForce = _velocity.magnitude * 2f;
                rb.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}