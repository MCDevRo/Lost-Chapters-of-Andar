using UnityEngine;

public class BrazierManager : MonoBehaviour
{
    public static BrazierManager Instance;
    private int braziersLit = 0;
    public GameObject spawnObject;
    public Transform spawnPosition;

    private void Awake()
    {
        Instance = this;
    }

    public void IncrementBraziersLit()
    {
        braziersLit++;
        if (braziersLit >= 3)
        {
            Instantiate(spawnObject, spawnPosition.position, spawnPosition.rotation);
        }
    }
}