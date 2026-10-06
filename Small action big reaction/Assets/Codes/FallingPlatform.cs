using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float shakeDuration = 0.15f;
    public float shakeAmount = 0.08f;
    public float gravityScale = 8f;

    private bool falling = false;

    public void Fall()
    {
        if (falling)
            return;

        falling = true;
        StartCoroutine(ShakeAndFall());
    }

    IEnumerator ShakeAndFall()
    {
        Vector3 originalPosition = transform.position;

        float timer = 0f;

        // اهتزاز سريع قبل السقوط
        while (timer < shakeDuration)
        {
            float x = Random.Range(-shakeAmount, shakeAmount);
            float y = Random.Range(-shakeAmount, shakeAmount);

            transform.position =
                originalPosition + new Vector3(x, y, 0f);

            timer += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPosition;

        // نفصل القطعة عن PlatformHolder
        transform.SetParent(null);

        // نخليها تسقط بسرعة
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = gravityScale;
        rb.collisionDetectionMode =
            CollisionDetectionMode2D.Continuous;
    }
}