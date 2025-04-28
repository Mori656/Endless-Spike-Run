using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // �aduj pierwsz� scen� gry (pami�taj by doda� j� do Build Settings!)
        SceneManager.LoadScene("GameView");
    }

    public void ShowStory()
    {
        // Tutaj mo�esz wczyta� osobn� scen� z fabu�� lub wy�wietli� okno
        SceneManager.LoadScene("StoryScene");
    }

    public void ShowCredits()
    {
        // Tutaj osobna scena z autorami lub panel
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        // Wyj�cie z gry
        Debug.Log("Wyj�cie z gry");
        Application.Quit();
    }
}
