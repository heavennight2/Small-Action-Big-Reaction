using UnityEngine;
using System.Collections;

public class RockSpawner : MonoBehaviour
{
    public GameObject rockPrefab;
    public Transform player;

    public float spawnInterval = 1.5f;

    // الصخرة تنزل قريب من اللاعب، مو فوق رأسه
    public float minDistance = 1.5f;
    public float maxDistance = 3.5f;

    private bool spawning = false;

    public void StartSpawning()
    {
        if (spawning)
            return;

        spawning = true;
        StartCoroutine(SpawnRocks());
    }

    IEnumerator SpawnRocks()
    {
        while (spawning)
        {
            // نختار يمين أو يسار اللاعب
            float side = Random.value < 0.5f ? -1f : 1f;

            // نختار المسافة
            float distance = Random.Range(minDistance, maxDistance);

            float spawnX = player.position.x + (side * distance);

            Vector3 spawnPosition = new Vector3(
                spawnX,
                transform.position.y,
                0f
            );

            Instantiate(
                rockPrefab,
                spawnPosition,
                Quaternion.identity
            );

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}