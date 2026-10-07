using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float fallDelay = 0.1f;
    public float shakeDuration = 0.15f;
    public float shakeAmount = 0.08f;
    public float gravityScale = 8f;

    private bool playerTouched = false;
    private bool falling = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // قبل أخذ الـ Acorn المنصات عادية
        if (!DisasterManager.disasterStarted)
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        // نتأكد إن اللاعب نزل فوق المنصة
        foreach (ContactPoint2D contact in collision.contacts)
        {
            if (contact.normal.y < -0.5f)
            {
                playerTouched = true;
                break;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!DisasterManager.disasterStarted)
            return;

        // ما لمس المنصة؟ لا تسوي شيء
        if (!playerTouched)
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        Fall();
    }

    public void Fall()
    {
        if (falling)
            return;

        falling = true;
        StartCoroutine(ShakeAndFall());
    }

    IEnumerator ShakeAndFall()
    {
        // مهلة صغيرة بعد ما اللاعب ينط منها
        yield return new WaitForSeconds(fallDelay);

        Vector3 originalPosition = transform.position;

        float timer = 0f;

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

        // نفصلها عن PlatformHolder
        transform.SetParent(null);

        // الآن تبدأ تسقط
        Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();

        rb.gravityScale = gravityScale;
        rb.collisionDetectionMode =
            CollisionDetectionMode2D.Continuous;
    }
}