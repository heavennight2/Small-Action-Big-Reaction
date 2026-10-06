using UnityEngine;

public class AcornPickup : MonoBehaviour
{
    public DisasterManager disasterManager;
    public RockSpawner rockSpawner;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("ACORN PICKED UP!");

            // الأرض تصير خطرة
            disasterManager.StartDisaster();

            // تبدأ الصخور
            rockSpawner.StartSpawning();

            // يختفي الـ Acorn
            gameObject.SetActive(false);
        }
    }
}