using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScript : MonoBehaviour
{ 
    public void BackToMenu()
    {
        Debug.Log("Powr�t do menu");
        SceneManager.LoadScene("MainMenu");
    }
}
