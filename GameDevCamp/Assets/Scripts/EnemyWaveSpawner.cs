using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveSpawner : MonoBehaviour
{
    // The prefab for the enemy game object
    public GameObject enemyPrefab;

    // The range in which enemies will be spawned
    public float spawnRange = 10.0f;

    // The minimum distance that enemies must be from the player
    public float minDistance = 8.0f;

    // The number of enemies to spawn in each wave
    public int waveSize = 5;

    // The interval between enemy waves
    public float waveInterval = 10.0f;

    // The player game object
    public GameObject player;

    // Use this for initialization
    void Start()
    {
        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    // A coroutine that spawns enemy waves
    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            // Wait for the wave interval
            yield return new WaitForSeconds(waveInterval);

            // Spawn a wave of enemies
            for (int i = 0; i < waveSize; i++)
            {
                // Calculate a random position within the spawn range
                float angle = Random.Range(0, 360);
                float x = Mathf.Sin(angle) * spawnRange;
                float z = Mathf.Cos(angle) * spawnRange;
                Vector3 spawnPos = player.transform.position + new Vector3(x, 0, z);
                // Check if the spawn position is too close to the player
                float distance = Vector3.Distance(spawnPos, player.transform.position);
                if (distance < minDistance)
                {
                    // If the spawn position is too close, try again
                    continue;
                }

                // Instantiate the enemy prefab at the spawn position
                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            }
        }
    }
}
