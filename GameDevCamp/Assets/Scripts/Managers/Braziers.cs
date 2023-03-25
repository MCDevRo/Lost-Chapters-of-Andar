using UnityEngine;

public class Braziers : MonoBehaviour
{
    public GameObject fireEffect;
    private bool isLit = false;
    public Transform offSetSpawnFire;

    private Collider brazierColider;

    private void Start()
    {
        brazierColider = GetComponent<Collider>();
    }

    public void LightBrazier()
    {
        if (!isLit)
        {
            Instantiate(fireEffect, offSetSpawnFire.position,offSetSpawnFire.rotation);
            isLit = true;
            brazierColider.enabled = !brazierColider.enabled;
        }
    }
}
