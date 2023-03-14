using UnityEngine;

public class Braziers : MonoBehaviour
{
    public GameObject fireEffect;
    private bool isLit = false;
    public Transform offSetSpawnFire;

    public void LightBrazier()
    {
        if (!isLit)
        {
            Instantiate(fireEffect, offSetSpawnFire.position,offSetSpawnFire.rotation);
            isLit = true;
        }
    }
}
