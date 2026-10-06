using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public Transform player;

    public float maxTiltAngle = 38f;
    public float tiltSpeed = 90f;

    // كل ما كان أصغر، المنصة تصير أكثر حساسية لحركة اللاعب
    public float sensitivity = 5f;

    private Rigidbody2D rb;
    private bool movementStarted = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!movementStarted)
            return;

        // بعد اللاعب عن منتصف المنصة
        float playerOffset = player.position.x - transform.position.x;

        // حساسية عالية:
        // ما يحتاج اللاعب يوصل للطرف عشان المنصة تتأثر
        float balance = Mathf.Clamp(
            playerOffset / sensitivity,
            -1f,
            1f
        );

        // نخلي التأثير أقوى كل ما ابتعد عن المنتصف
        float aggressiveBalance =
            Mathf.Sign(balance) * balance * balance;

        float targetAngle =
            -aggressiveBalance * maxTiltAngle;

        // المنصة تلحق الوزن بسرعة
        float newAngle = Mathf.MoveTowardsAngle(
            rb.rotation,
            targetAngle,
            tiltSpeed * Time.fixedDeltaTime
        );

        rb.MoveRotation(newAngle);
    }

    public void StartMovement()
    {
        movementStarted = true;
    }
}