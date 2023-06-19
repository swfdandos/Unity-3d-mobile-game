using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject Overmenu;
    public bool isdead;
    private SaðlýkDurumu saglik;

    void Start()
    {
        Overmenu.SetActive(false); // oyun baþýnda paneli kapatýr.
        saglik.GetComponent<SaðlýkDurumu>();
    }

    
    void Update()
    {
        if (isdead)
        {
            OverGame();
        }
        else
        {
            TryAgain();
        }
    }
    public void OverGame()
    {
        Overmenu.SetActive(true); // Paneli getirir ve oyun durur.
        Time.timeScale = 0f;
        isdead = true;
    }
    public void BacktoMenu()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // Panel sýralamasýndaki 0. yi getirir yani mainmenü panelini.
    }
    public void TryAgain()
    {
        Overmenu.SetActive(false);
        Time.timeScale = 1f;
        isdead = false;
    }
   
    public void SaglýkCubugu()
    {
        if (saglik.healthBar.value <= 0 )
        {
            OverGame();
        }
        else
        {
            TryAgain();
        }
    }
}
