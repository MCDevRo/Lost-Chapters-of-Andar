using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarController : MonoBehaviour
{
    public GameObject chargedEffect; // reference to the charged effect prefab to be spawned

    public int numShots; // number of shots taken by the pillar
    private bool charged; // flag indicating whether the pillar is charged or not

    private DiamondGlow[] diamondGlows;
    void Awake()
    {
        diamondGlows = GetComponentsInChildren<DiamondGlow>();
    }
    public void TakeShot()
    {
        // increment the number of shots taken by the pillar
        numShots++;

        // check if the pillar is charged
        if (!charged && numShots >= 1)
        {
            charged = true;

            // spawn the charged effect
            if (chargedEffect != null)
            {
                Instantiate(chargedEffect, transform.position, transform.rotation);
            }
            // enable the diamond glow
            foreach (DiamondGlow diamondGlow in diamondGlows)
            {
                diamondGlow.EnableGlow();
            }


            // notify the boss that the pillar has been charged
            BossHealth bossHealth = GameObject.FindObjectOfType<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.PillarCharged();
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("PillarTrigger OnTriggerEnter");
        if (other.CompareTag("PlayerProjectile"))
        {
            TakeShot();
        }
    }
}