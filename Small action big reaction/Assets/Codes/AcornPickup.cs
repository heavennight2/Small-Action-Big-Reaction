using UnityEngine;

public class AcornPickup : MonoBehaviour
{
    public PlatformMovement platform;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ACORN PICKED UP!");

            // شغّل حركة المنصة
            platform.StartMovement();

            // أخفِ الـ Acorn
            gameObject.SetActive(false);
        }
    }
}