using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameObject pillar;
    [SerializeField] private GameObject artefact;
    [SerializeField] private float lowerSpeed = 0.5f;
    [SerializeField] private float lowerYPosition = 5f;
    [SerializeField] public Text enemyCountText;

    private int enemyCount;
    private bool isLowering;
    private int enemiesDefeated;

    private void Start()
    {
        
        
        // Get the total number of enemies that will be spawned
        foreach (int count in spawnManager.enemyCounts)
        {
            enemyCount += count;
        }

        // Set the initial enemy count text
        UpdateEnemyCountText();
    }

    private void Update()
    {
        // Check if all enemies have been defeated
        if (spawnManager.spawnedEnemies.Count == enemyCount && !isLowering)
        {
            // Lower the pillar and activate the artefact
            isLowering = true;
            StartCoroutine(LowerPillar());
            //artefact.SetActive(true);
        }

        // Update the enemy count text
        UpdateEnemyCountText();
    }

    private IEnumerator LowerPillar()
    {
        // Lower the pillar gradually
        while (pillar.transform.position.y > lowerYPosition)
        {
            pillar.transform.position -= new Vector3(0f, lowerSpeed * Time.deltaTime, 0f);
            yield return null;
        }

        // Disable this script so that the task is only completed once
        enabled = false;
    }

    private void UpdateEnemyCountText()
    {
        
        // Update the enemy count text
        enemyCountText.text = "Defeat Enemies: " + enemiesDefeated + " / " + enemyCount;
    }

    public void IncrementEnemiesDefeated()
    {
        // Increment the number of enemies defeated
        enemiesDefeated++;

        // Update the enemy count text
        UpdateEnemyCountText();
        Debug.Log("Enemies defeated: " + enemiesDefeated + " / " + enemyCount);
    }
}
