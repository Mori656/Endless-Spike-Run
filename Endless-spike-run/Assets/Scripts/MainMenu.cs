using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // £aduj pierwsz¹ scenê gry (pamiêtaj by dodaæ j¹ do Build Settings!)
        SceneManager.LoadScene("GameView");
    }

    public void ShowStory()
    {
        // Tutaj mo¿esz wczytaæ osobn¹ scenê z fabu³¹ lub wyœwietliæ okno
        SceneManager.LoadScene("StoryScene");
    }

    public void ShowCredits()
    {
        // Tutaj osobna scena z autorami lub panel
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        // Wyjœcie z gry
        Debug.Log("Wyjœcie z gry");
        Application.Quit();
    }
}
