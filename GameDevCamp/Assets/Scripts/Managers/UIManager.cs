using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text enemiesDefeatedText; // Reference to the UI Text component that displays the number of enemies defeated
    public SpawnManager spawnManager; // Reference to the SpawnManager script

    private int enemiesDefeated = 0; // Counter for the number of enemies defeated

    void Start()
    {
        enemiesDefeatedText.gameObject.SetActive(false);
        spawnManager = GetComponent<SpawnManager>();
        UpdateEnemiesDefeatedText(); // Initialize the UI Text component with the current number of enemies defeated
    }

    public void IncrementEnemiesDefeated()
    {
        enemiesDefeated++; // Increment the counter for the number of enemies defeated
        UpdateEnemiesDefeatedText(); // Update the UI Text component with the new count of defeated enemies

        if (enemiesDefeated == spawnManager.numberOfEnemiesToSpawn)
        {
            // All enemies have been defeated
            Debug.Log("Congratulations! You have defeated all enemies.");
            StartCoroutine(spawnManager.LowerPillar());
            // You could add some additional code here to end the level or trigger a victory screen
        }
    }

    void UpdateEnemiesDefeatedText()
    {
        enemiesDefeatedText.text = "Enemies Defeated: " + enemiesDefeated.ToString() + " / " + spawnManager.numberOfEnemiesToSpawn.ToString(); // Update the UI Text component with the current number of enemies defeated
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Enemies started to spawn!");
            enemiesDefeatedText.gameObject.SetActive(true);

        }
    }
}