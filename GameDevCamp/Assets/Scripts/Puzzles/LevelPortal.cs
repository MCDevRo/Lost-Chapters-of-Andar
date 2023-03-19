using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    public ScrollCounter scrollCounter;
    public string nextLevelName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && scrollCounter.collectedScrolls >= scrollCounter.totalScrolls)
        {
            SceneManager.LoadScene(nextLevelName);
        }
    }
}