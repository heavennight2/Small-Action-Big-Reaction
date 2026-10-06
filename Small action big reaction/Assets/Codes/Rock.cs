using UnityEngine;

public class Rock : MonoBehaviour
{
    public float destroyDelay = 0.25f;

    private bool hitSomething = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hitSomething)
            return;

        if (collision.gameObject.CompareTag("Ground") ||
            collision.gameObject.CompareTag("Player"))
        {
            hitSomething = true;

            Destroy(gameObject, destroyDelay);
        }
    }
}