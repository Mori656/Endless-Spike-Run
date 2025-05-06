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
        // Wyj�cie z gry
        Debug.Log("Wyj�cie z gry");
        Application.Quit();
    }
}
