using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] enemyPrefabs;
    public int numberOfEnemiesToSpawn;
    public Transform[] spawnPoints;

    private Collider gameManagerCollider;

    [SerializeField] private GameObject pillar;
    [SerializeField] private float lowerSpeed = 0.5f;
    [SerializeField] private float lowerYPosition = 5f;

    public UIManager uiManager; // Reference to the UIManager script
    public GameObject magicWall;
    public GameObject magicBlocade;

    void Start()
    {
        gameManagerCollider = GetComponent<Collider>();
        LowerPillar();
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            int spawnPointIndex = Random.Range(0, spawnPoints.Length);
            GameObject enemyObject = Instantiate(enemyPrefabs[enemyIndex], spawnPoints[spawnPointIndex].position, Quaternion.identity);
            // Pass a reference to the UIManager script to each spawned enemy
            enemyObject.GetComponent<SecondAreaEnemyHealth>().uiManager = uiManager;
            yield return new WaitForSeconds(1.0f); // wait 1 second before spawning the next enemy
        }
    }
    public IEnumerator LowerPillar()
    {
        // Lower the pillar gradually
        while (pillar.transform.position.y > lowerYPosition)
        {
            pillar.transform.position -= new Vector3(0f, lowerSpeed * Time.deltaTime, 0f);
            yield return null;
        }
        magicBlocade.SetActive(false);
        // Disable this script so that the task is only completed once
        enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Enemies started to spawn!");
            StartCoroutine(SpawnEnemies());
            

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            magicWall.SetActive(true);
            magicBlocade.SetActive(true);
            gameManagerCollider.enabled = !gameManagerCollider.enabled;
        }
    }
}