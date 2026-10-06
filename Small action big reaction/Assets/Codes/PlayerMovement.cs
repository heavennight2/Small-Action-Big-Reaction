using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform platform;

    public float moveSpeed = 6f;
    public float acceleration = 20f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float input = Input.GetAxisRaw("Horizontal");

        // اتجاه سطح المنصة
        Vector2 platformDirection = platform.right;

        // السرعة الحالية على اتجاه المنصة
        float currentSpeed =
            Vector2.Dot(rb.linearVelocity, platformDirection);

        // السرعة اللي اللاعب يبغاها
        float targetSpeed = input * moveSpeed;

        float newSpeed = Mathf.MoveTowards(
            currentSpeed,
            targetSpeed,
            acceleration * Time.fixedDeltaTime
        );

        // الفرق المطلوب إضافته
        float speedDifference = newSpeed - currentSpeed;

        rb.linearVelocity += platformDirection * speedDifference;
    }
}