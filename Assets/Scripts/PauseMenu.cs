using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool ispause;
    void Start()
    {
        pauseMenu.SetActive(false); // oyun baþýnda paneli kapatýr.
    }

    
    void Update()
    {
        if (ispause)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }
    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Paneli getirir ve oyun durur.
        Time.timeScale = 0f;
        ispause = true;
    }
    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        ispause = false;
    }
    public void BacktoMenu()
    {
        
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Panel sýralamasýndaki 0. yi getirir yani mainmenü panelini.
    }
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyundan Çýkýldý");
    }
}
