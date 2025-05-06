using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameView");
    }

    public void ShowStory()
    {
        SceneManager.LoadScene("StoryScene");
    }

    public void ShowCredits()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        // Wyjœcie z gry
        Debug.Log("Wyjœcie z gry");
        Application.Quit();
    }
}
