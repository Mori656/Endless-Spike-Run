using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        Debug.Log("Powr�t do menu");
        SceneManager.LoadScene("MainMenu");
    }
}