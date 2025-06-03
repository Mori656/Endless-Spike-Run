using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    void Start()
    {
        if (PlayerPrefs.HasKey("hiScore"))
        {
            scoreUI.text = ""+ PlayerPrefs.GetInt("hiScore");
        }
        
    }
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
