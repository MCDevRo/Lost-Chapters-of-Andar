using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] public List<int> enemyCounts;

    public List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Start()
    {
        //StartCoroutine(SpawnEnemies());
    }
    public IEnumerator SpawnEnemies()
    {
        // Loop through each enemy type and spawn the desired number of enemies
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            for (int j = 0; j < enemyCounts[i]; j++)
            {
                // Randomly select a spawn point
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

                // Instantiate the enemy prefab at the selected spawn point
                GameObject spawnedEnemy = Instantiate(enemyPrefabs[i], spawnPoint.position, spawnPoint.rotation);

                // Add the spawned enemy to the list of spawned enemies
                spawnedEnemies.Add(spawnedEnemy);

                // Wait for 3 seconds before spawning the next enemy
                yield return new WaitForSeconds(3f);
            }
        }
    }

    public void ResetSpawnedEnemies()
    {
        // Destroy all spawned enemies and clear the list
        foreach (GameObject spawnedEnemy in spawnedEnemies)
        {
            Destroy(spawnedEnemy);
        }
        spawnedEnemies.Clear();

        // Spawn new enemies with the current settings
        StartCoroutine(SpawnEnemies());
    }

    // The following methods can be used to modify the spawn settings at runtime

    public void AddSpawnPoint(Transform spawnPoint)
    {
        spawnPoints.Add(spawnPoint);
    }

    public void RemoveSpawnPoint(Transform spawnPoint)
    {
        spawnPoints.Remove(spawnPoint);
    }

    public void AddEnemyPrefab(GameObject enemyPrefab, int count)
    {
        enemyPrefabs.Add(enemyPrefab);
        enemyCounts.Add(count);
    }

    public void RemoveEnemyPrefab(GameObject enemyPrefab)
    {
        int index = enemyPrefabs.IndexOf(enemyPrefab);
        enemyPrefabs.RemoveAt(index);
        enemyCounts.RemoveAt(index);
    }

    public void SetEnemyCount(GameObject enemyPrefab, int count)
    {
        int index = enemyPrefabs.IndexOf(enemyPrefab);
        enemyCounts[index] = count;
    }

    private void OnTriggerEnter(Collider other)
    {
        TaskManager taskManager = GetComponent<TaskManager>();

        if (other.gameObject.tag == "Player")
        {
            taskManager.enabled = true;
            Debug.Log("Enemies are starting to spawn!");
            //taskManager.enemyCountText.gameObject.SetActive(true);
            StartCoroutine(SpawnEnemies());
            gameObject.GetComponent<Collider>().enabled = false;
        }
    }
}
