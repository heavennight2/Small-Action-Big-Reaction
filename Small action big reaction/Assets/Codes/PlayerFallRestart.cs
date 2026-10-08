
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFallRestart : MonoBehaviour
{
    public float deathY = -10f;
    public GameObject losePanel;

    private bool isDead = false;

    void Start()
    {
        if (losePanel != null)
            losePanel.SetActive(false);
    }

    void Update()
    {
        if (!isDead && transform.position.y < deathY)
        {
            Lose();
        }
    }

    void Lose()
    {
        isDead = true;

        // Stop player movement
        PlayerMovement movement = GetComponent<PlayerMovement>();

        if (movement != null)
            movement.enabled = false;

        // Stop camera shake
        if (CameraShake.instance != null)
            CameraShake.instance.StopShake();

        // Show lose screen
        if (losePanel != null)
            losePanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
