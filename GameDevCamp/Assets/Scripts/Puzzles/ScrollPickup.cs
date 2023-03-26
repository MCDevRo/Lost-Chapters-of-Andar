using UnityEngine;

public class ScrollPickup : MonoBehaviour
{
    public delegate void ScrollCollectedHandler();
    public static event ScrollCollectedHandler OnScrollCollected;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnScrollCollected?.Invoke();
            Destroy(gameObject);
        }
    }
}