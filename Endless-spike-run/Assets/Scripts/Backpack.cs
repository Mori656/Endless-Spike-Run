using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Backpack : MonoBehaviour
{
    public int score = 0;
    
    public TextMeshProUGUI textUI;
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Coin"))
        {
            score++;
            Destroy(other.gameObject);
        }
        
        if (other.CompareTag("Star"))
        {
            score+=10;
            Destroy(other.gameObject);
        }
    }

    void Update()
    {
    if (textUI != null)
            textUI.text = "Wynik: " + score.ToString();
    else
        Debug.LogWarning("textUI nie jest przypisane w inspektorze!");
    }
}
