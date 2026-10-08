
using UnityEngine;
using System.Collections;

public class HomeTarget : MonoBehaviour
{
    public Transform entrancePoint;
    public GameObject endPanel;

    public float enterDuration = 1f;

    // Audio
    public AudioSource backgroundMusic;
    public AudioSource audioSource;
    public AudioClip winSound;

    private bool finished = false;

    private void Start()
    {
        if (endPanel != null)
            endPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || finished)
            return;

        finished = true;
        StartCoroutine(EnterHome(other.gameObject));
    }

    private IEnumerator EnterHome(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();

        if (movement != null)
            movement.enabled = false;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.simulated = false;
        }

        // Stop camera shake
        if (CameraShake.instance != null)
            CameraShake.instance.StopShake();

        Vector3 startPosition = player.transform.position;
        Vector3 startScale = player.transform.localScale;

        Vector3 targetPosition = entrancePoint != null
            ? entrancePoint.position
            : transform.position;

        float timer = 0f;

        while (timer < enterDuration)
        {
            timer += Time.deltaTime;

            float t = Mathf.Clamp01(timer / enterDuration);

            player.transform.position = Vector3.Lerp(
                startPosition,
                targetPosition,
                t
            );

            player.transform.localScale = Vector3.Lerp(
                startScale,
                Vector3.zero,
                t
            );

            yield return null;
        }

        player.SetActive(false);

        // Stop background music
        if (backgroundMusic != null)
            backgroundMusic.Stop();

        // Play victory sound
        if (audioSource != null && winSound != null)
            audioSource.PlayOneShot(winSound);

        yield return new WaitForSeconds(0.5f);

        if (endPanel != null)
            endPanel.SetActive(true);
    }
}
