using UnityEngine;

public class BrazierManager : MonoBehaviour
{
    public static BrazierManager Instance;
    private int braziersLit = 0;
    public GameObject spawnObject;
    public Transform spawnPosition;
    public GameObject magicWall;

    private void Awake()
    {
        Instance = this;
    }

    public void IncrementBraziersLit()
    {
        braziersLit++;
        if (braziersLit >= 3)
        {
            magicWall.SetActive(false);
            Instantiate(spawnObject, spawnPosition.position, spawnPosition.rotation);
        }
    }
}