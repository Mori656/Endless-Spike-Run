using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpikeHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")){
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("DeathScene");
        }
    }
}
