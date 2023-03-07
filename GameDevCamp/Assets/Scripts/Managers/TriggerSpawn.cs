using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSpawn : MonoBehaviour
{
    private SpawnManager spawnManager;

    private void Start()
    {
        spawnManager = GetComponent<SpawnManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(spawnManager.SpawnEnemies());
        }
    }
}
