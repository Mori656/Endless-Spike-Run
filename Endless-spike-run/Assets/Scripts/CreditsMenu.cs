using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
    public void BackToMenu()
    {
        Debug.Log("Powr�t do menu");
        SceneManager.LoadScene("MainMenu");
    }
}