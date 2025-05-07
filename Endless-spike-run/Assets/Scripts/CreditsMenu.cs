using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        Debug.Log("Powrót do menu");
        SceneManager.LoadScene("MainMenu");
    }
}