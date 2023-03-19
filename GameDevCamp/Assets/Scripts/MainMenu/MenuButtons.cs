using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("stage 1");
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadGame()
    {
        // Load saved game data here
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}