using UnityEngine;
using UnityEngine.UI;

public class ScrollCounter : MonoBehaviour
{
    public Text scrollCounterText;
    public int totalScrolls = 2;
    public int collectedScrolls = 0;

    private void OnEnable()
    {
        ScrollPickup.OnScrollCollected += IncrementScrollCounter;
    }

    private void OnDisable()
    {
        ScrollPickup.OnScrollCollected -= IncrementScrollCounter;
    }

    private void IncrementScrollCounter()
    {
        collectedScrolls++;
        UpdateScrollCounterText();
    }

    private void UpdateScrollCounterText()
    {
        scrollCounterText.text = $"Scrolls: {collectedScrolls}/{totalScrolls}";
    }
}