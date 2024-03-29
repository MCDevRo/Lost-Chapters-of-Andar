using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    public GameObject magicBaricad;
    public GameObject magicBaricade;
    public GameObject bossUi;
    public GameObject boss; // Add this line to reference the GameObject with the BossHealth component
    private BossHealth bossHealth;

    private void Start()
    {
        bossHealth = boss.GetComponent<BossHealth>(); // Modify this line to get the BossHealth component from the boss GameObject
    }
    private void Update()
    {
        if (bossHealth.currentHealth <= 0)
        {
            magicBaricad.SetActive(false);
            magicBaricade.SetActive(false);
            bossUi.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            magicBaricad.SetActive(true);
            magicBaricade.SetActive(true);
            bossUi.SetActive(true);
            FindObjectOfType<AudioManager>().Play("BossIntro");
        }
    }
}
