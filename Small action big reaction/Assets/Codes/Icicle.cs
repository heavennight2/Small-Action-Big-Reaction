using UnityEngine;
using System.Collections;

public class Icicle : MonoBehaviour
{
    public float detectionDistance = 4f;
    public float shakeDuration = 0.5f;
    public float shakeAmount = 0.05f;
    public float fallGravity = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private bool triggered = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (triggered || player == null)
            return;

        // Player is close enough horizontally
        float distanceX = Mathf.Abs(player.position.x - transform.position.x);

        // Player is underneath the icicle
        if (distanceX <= detectionDistance &&
            player.position.y < transform.position.y)
        {
            triggered = true;
            StartCoroutine(FallSequence());
        }
    }

    IEnumerator FallSequence()
    {
        // Shake first
        Vector3 originalPosition = transform.position;
        float timer = 0f;

        while (timer < shakeDuration)
        {
            float x = Random.Range(-shakeAmount, shakeAmount);

            transform.position =
                originalPosition + new Vector3(x, 0f, 0f);

            timer += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;

        // FALL
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = fallGravity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PLAYER HIT BY ICICLE!");
        }
    }
}