using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text optionsText;
    public Image controlsDisplay;
    public Text enemiesDefeatedText; // Reference to the UI Text component that displays the number of enemies defeated
    public SpawnManager spawnManager; // Reference to the SpawnManager script

    private int enemiesDefeated = 0; // Counter for the number of enemies defeated

    public Slider healthSlider;

    // Add references to the Game Over Panel and buttons
    public GameObject gameOverPanel;
    public Button retryButton;
    public Button returnToMainMenuButton;

    //private TopDownPlayerController playerController;

    void Start()
    {
        controlsDisplay.gameObject.SetActive(false);
        enemiesDefeatedText.gameObject.SetActive(false);
        spawnManager = GetComponent<SpawnManager>();
        UpdateEnemiesDefeatedText(); // Initialize the UI Text component with the current number of enemies defeated

        // Initialize the Game Over Panel and button click events
        gameOverPanel.SetActive(false);
        retryButton.onClick.AddListener(Retry);
        returnToMainMenuButton.onClick.AddListener(ReturnToMainMenu);

        //playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<TopDownPlayerController>();

    }

    private void Update()
    {
      
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            optionsText.gameObject.SetActive(false);
            controlsDisplay.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            controlsDisplay.gameObject.SetActive(false);
            optionsText.gameObject.SetActive(true);
        }
        ReturnToMainMenu();
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
    public void SetMaxHealth(float health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }
    public void SetHealth(float health)
    {
        healthSlider.value = health;
    }

    private void ReturnToMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void ShowGameOverScreen()
    {
        // Pause the game
        Time.timeScale = 0;

        // Enable the Game Over Panel
        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        // Unpause the game
        Time.timeScale = 1;

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnMainMenu()
    {
        // Unpause the game
        Time.timeScale = 1;

        // Load the main menu scene
        SceneManager.LoadScene("MainMenu");
    }
}