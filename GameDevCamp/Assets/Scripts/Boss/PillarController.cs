using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarController : MonoBehaviour
{
    public GameObject chargedEffect; // reference to the charged effect prefab to be spawned

    public int numShots; // number of shots taken by the pillar
    private bool charged; // flag indicating whether the pillar is charged or not
    public void TakeShot()
    {
        // increment the number of shots taken by the pillar
        numShots++;

        // check if the pillar is charged
        if (!charged && numShots >= 2)
        {
            charged = true;

            // spawn the charged effect
            if (chargedEffect != null)
            {
                Instantiate(chargedEffect, transform.position, transform.rotation);
            }

            // notify the boss that the pillar has been charged
            BossHealth bossHealth = GameObject.FindObjectOfType<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.PillarCharged();
            }
        }
    }
}