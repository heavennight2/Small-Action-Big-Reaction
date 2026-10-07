using UnityEngine;
using System.Collections;
using TMPro;

public class AcornPickup : MonoBehaviour
{
    public DisasterManager disasterManager;

    // Text
    public GameObject escapeTextObject;
    public TextMeshProUGUI escapeText;
    public string message = "I NEED TO GET BACK HOME!";
    public float typingSpeed = 0.05f;
    public float messageStayTime = 2f;

    // Sound
    public AudioSource audioSource;
    public AudioClip disasterSound;

    private bool pickedUp = false;

    private void Start()
    {
        if (escapeTextObject != null)
        {
            escapeTextObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || pickedUp)
            return;

        pickedUp = true;

        Debug.Log("ACORN PICKED UP!");

        // Play disaster sound
        if (audioSource != null && disasterSound != null)
        {
            audioSource.PlayOneShot(disasterSound);
        }

        // CAMERA SHAKE
        if (CameraShake.instance != null)
        {
            // Strong shake at first
            CameraShake.instance.StartShake(1.2f, 0.15f);

            // Then keep shaking lightly
            CameraShake.instance.StartContinuousShake(0.04f);
        }

        // Start disaster
        if (disasterManager != null)
        {
            disasterManager.StartDisaster();
        }

        // Typewriter message
        if (escapeTextObject != null && escapeText != null)
        {
            StartCoroutine(TypeMessage());
        }

        // Hide acorn
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            sprite.enabled = false;
        }

        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
        {
            col.enabled = false;
        }
    }

    private IEnumerator TypeMessage()
    {
        escapeTextObject.SetActive(true);
        escapeText.text = "";

        foreach (char letter in message)
        {
            escapeText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(messageStayTime);

        escapeTextObject.SetActive(false);

        // IMPORTANT:
        // Don't disable the whole acorn object here.
        // AudioSource may still be using it.
    }
}