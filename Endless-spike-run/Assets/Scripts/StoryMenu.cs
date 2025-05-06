using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        Debug.Log("Powrót do menu");
        SceneManager.LoadScene("MainMenu");
    }
}