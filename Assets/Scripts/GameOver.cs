using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    public GameObject Overmenu;
    public bool isdead;
    private Sa�l�kDurumu saglik;

    void Start()
    {
        Overmenu.SetActive(false); // oyun ba��nda paneli kapat�r.
        saglik.GetComponent<Sa�l�kDurumu>();
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
        SceneManager.LoadScene(0); // Panel s�ralamas�ndaki 0. yi getirir yani mainmen� panelini.
    }
    public void TryAgain()
    {
        Overmenu.SetActive(false);
        Time.timeScale = 1f;
        isdead = false;
    }
   
    public void Sagl�kCubugu()
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
