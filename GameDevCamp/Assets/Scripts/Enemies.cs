using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : ScriptableObject
{
    private float maxHealth = 100;
    private float currentHealth;
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = currentHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }
  
}
